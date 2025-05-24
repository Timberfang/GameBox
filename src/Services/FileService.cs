using System.Text.Json;
using System.Text.Json.Serialization;
using MediaBox.Media;

namespace MediaBox.Services;

[JsonSourceGenerationOptions(UseStringEnumConverter = true, WriteIndented = true)]
[JsonSerializable(typeof(IMedia))]
[JsonSerializable(typeof(Game))]
[JsonSerializable(typeof(Movie))]
[JsonSerializable(typeof(Show))]
public partial class MediaContext : JsonSerializerContext;

public static class FileService
{
	private static readonly string ConfigPath = Path.Join(Environment.CurrentDirectory, "config");

	private static readonly Dictionary<Type, string> LibraryName = new()
	{
		{ typeof(Game), "games" },
		{ typeof(Movie), "movies" },
		{ typeof(Show), "shows" }
	};

	private static string[] GetMedia<T>() where T : IMedia
	{
		Initialize<T>();
		return Directory.GetFiles(GetLibraryPath<T>(), "*.json");
	}

	public static void SaveMedia<T>(T media) where T : class, IMedia
	{
		Initialize<T>();
		string targetPath = Path.Combine(GetLibraryPath<T>(), GetSafePath(media.Name) + ".json");
		Serialize(media, targetPath);
	}

	public static T LoadMedia<T>(string name) where T : class, IMedia
	{
		Initialize<T>();
		string targetPath = Path.Combine(GetLibraryPath<T>(), name);
		if (!targetPath.EndsWith(".json")) targetPath += ".json";
		return Deserialize<T>(targetPath);
	}

	public static IEnumerable<T> LoadAllMedia<T>() where T : class, IMedia
	{
		Initialize<T>();
		List<T> output = [];
		output.AddRange(GetMedia<T>().Select(LoadMedia<T>));
		return output;
	}

	public static IEnumerable<string> ListMedia<T>() where T : class, IMedia
	{
		// OfType removes null reference warning
		return GetMedia<T>().Select(Path.GetFileNameWithoutExtension).OfType<string>();
	}

	private static void Initialize<T>() where T : IMedia
	{
		string[] directories = [ConfigPath, GetLibraryPath<T>()];

		foreach (string directoryPath in directories) Directory.CreateDirectory(directoryPath);
	}

	private static void Serialize<T>(T obj, string target) where T : class, IMedia
	{
		if (File.Exists(target)) throw new InvalidOperationException("");
		File.WriteAllText(target, JsonSerializer.Serialize(obj, typeof(T), MediaContext.Default));
	}

	private static T Deserialize<T>(string target) where T : class, IMedia
	{
		if (!File.Exists(target)) throw new FileNotFoundException($"File at '{target}' not found.");
		return JsonSerializer.Deserialize(File.ReadAllText(target), typeof(T), MediaContext.Default) as T ??
		       throw new Exception();
	}

	private static string GetLibraryPath<T>() where T : IMedia
	{
		return Path.Join(ConfigPath, LibraryName[typeof(T)]);
	}

	private static string GetSafePath(string path)
	{
		// Colons get special handling, see https://wiki.no-intro.org/index.php?title=Naming_Convention#Characters
		path = path.Replace(": ", " - ");
		return Path.GetInvalidFileNameChars().Aggregate(path, (current, c) => current.Replace(c, '_'));
	}
}