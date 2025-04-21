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
    private static readonly Dictionary<MediaType, string> LibraryName = new()
    {
        { MediaType.Game, "games" },
        { MediaType.Movie, "movies" },
        { MediaType.Show, "shows" }
    };

    public static IEnumerable<string> GetMedia(MediaType type)
    {
        Initialize(type);
        return Directory.GetFiles(GetLibraryPath(type), "*.json");
    }

    public static void SaveMedia<T>(T media) where T : IMedia
    {
        Initialize(media.Type);
        string TargetPath = Path.Combine(GetLibraryPath(media.Type), media.Name + ".json");
        Serialize(media, TargetPath);
    }

    public static T LoadMedia<T>(string name, MediaType type)
    {
        Initialize(type);
        string TargetPath = Path.Combine(GetLibraryPath(type), name);
        if (!TargetPath.EndsWith(".json")) { TargetPath += ".json"; }
        return Deserialize<T>(TargetPath);
    }

    private static void Initialize(MediaType type)
    {
        string[] Directories = [ConfigPath, GetLibraryPath(type)];

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

    private static string GetLibraryPath(MediaType type) => Path.Join(ConfigPath, LibraryName[type]);
}
