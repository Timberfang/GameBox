using System.Text.Json;
using GameBox.Media;

namespace GameBox.Services;

public static class FileService
{
    private static readonly string ConfigPath = Path.Join(Environment.CurrentDirectory, "config");
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        IgnoreReadOnlyProperties = true,
        WriteIndented = true
    };
    private static readonly Dictionary<Type, string> LibraryName = new()
    {
        { typeof(Game), "games" },
        // { typeof(Media), "movies" },
        // { typeof(Show), "shows" }
    };

    public static IEnumerable<string> GetMedia<T>() where T : IMedia
    {
        Initialize<T>();
        return Directory.GetFiles(GetLibraryPath<T>(), "*.json");
    }

    public static void SaveMedia<T>(T media) where T : IMedia
    {
        Initialize<T>();
        string TargetPath = Path.Combine(GetLibraryPath<T>(), GetSafePath(media.Name) + ".json");
        Serialize(media, TargetPath);
    }

    public static T LoadMedia<T>(string name) where T : IMedia
    {
        Initialize<T>();
        string TargetPath = Path.Combine(GetLibraryPath<T>(), name);
        if (!TargetPath.EndsWith(".json")) { TargetPath += ".json"; }
        return Deserialize<T>(TargetPath);
    }

    public static IEnumerable<T> LoadAllMedia<T>() where T : IMedia
    {
        Initialize<T>();
        List<T> output = [];
        foreach (string file in GetMedia<T>()) { output.Add(LoadMedia<T>(file)); }
        return output;
    }

    private static void Initialize<T>() where T : IMedia
    {
        string[] Directories = [ConfigPath, GetLibraryPath<T>()];

        foreach (string DirectoryPath in Directories) { Directory.CreateDirectory(DirectoryPath); }
    }

    private static void Serialize<T>(T obj, string target)
    {
        if (File.Exists(target)) { throw new InvalidOperationException(""); }
        File.WriteAllText(target, JsonSerializer.Serialize(obj, JsonOptions));
    }

    private static T Deserialize<T>(string target)
    {
        if (!File.Exists(target)) { throw new FileNotFoundException(""); }
        return JsonSerializer.Deserialize<T>(File.ReadAllText(target)) ?? throw new Exception();
    }

    private static string GetLibraryPath<T>() where T : IMedia
    {
        return Path.Join(ConfigPath, LibraryName[typeof(T)]);
    }

    private static string GetSafePath(string path)
    {
        // Colons get special handling, see https://wiki.no-intro.org/index.php?title=Naming_Convention#Characters
        path = path.Replace(": ", " - ");
        foreach (char c in Path.GetInvalidFileNameChars()) { path = path.Replace(c, '_'); }
        return path;
    }
}
