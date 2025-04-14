namespace GameBox.Media;

public record Emulator
{
    public readonly string Name;
    public readonly IEnumerable<GamePlatform> Platform;
    public readonly string URL;

    public Emulator()
    {
        Name = string.Empty;
        Platform = [];
        URL = string.Empty;
    }

    public Emulator(string name, GamePlatform platform, string url)
    {
        Name = name;
        Platform = [platform];
        URL = url;
    }

    public Emulator(string name, IEnumerable<GamePlatform> platform, string url)
    {
        Name = name;
        Platform = platform;
        URL = url;
    }
}
