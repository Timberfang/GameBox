using System.Text.Json;

namespace GameVault;

public static class FileService
{
    private static readonly string ConfigPath = Path.Join(Environment.CurrentDirectory, "config");
    private static readonly string GamePath = Path.Combine(ConfigPath, "games");
    private static readonly string EmulatorPath = Path.Combine(ConfigPath, "emulators");
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        IgnoreReadOnlyProperties = true,
        WriteIndented = true
    };

    public static IEnumerable<string> GetGames()
    {
        Initialize();
        return Directory.GetFiles(GamePath, "*.json");
    }

    public static void SaveGame(Game game)
    {
        Initialize();
        string TargetPath = Path.Combine(GamePath, game.Name + ".json");
        Serialize(game, TargetPath);
    }

    public static Game LoadGame(string name)
    {
        Initialize();
        string TargetPath = Path.Combine(GamePath, name);
        if (!TargetPath.EndsWith(".json")) { TargetPath += ".json"; }
        return Deserialize<Game>(TargetPath);
    }

    public static IEnumerable<string> GetEmulators()
    {
        Initialize();
        return Directory.GetFiles(EmulatorPath, "*.json");
    }

    public static IEnumerable<Emulator> GetEmulatorConfigs()
    {
        Initialize();
        List<Emulator> emulators = [];
        foreach (string EmulatorName in GetEmulators()) { emulators.Add(LoadEmulator(EmulatorName)); }
        return emulators;
    }

    public static Emulator LoadEmulator(string name)
    {
        Initialize();
        string TargetPath = Path.Combine(EmulatorPath, name);
        if (!TargetPath.EndsWith(".json")) { TargetPath += ".json"; }
        return Deserialize<Emulator>(TargetPath);
    }

    private static void Initialize()
    {
        string[] Directories = [ConfigPath, GamePath, EmulatorPath];

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
}
