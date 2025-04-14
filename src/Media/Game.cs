namespace GameBox.Media;

public class Game : IMedia
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public string? Creator { get; set; }
    public string? Cover { get; set; }
    public GamePlatform Platform { get; set; }
    public static readonly Dictionary<GamePlatform, string> GamePlatformName = new()
    {
        { GamePlatform.PC, "PC (Windows)" },
        { GamePlatform.N3DS, "Nintendo 3DS" },
        { GamePlatform.N64, "Nintendo 64" },
        { GamePlatform.NSwitch, "Nintendo Switch" },
        { GamePlatform.NWiiU, "Nintendo Wii U" }
    };

    public Game()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public Game(string name, string? description = null, GamePlatform? platform = null, int year = 1970, string? creator = null)
    {
        Name = name;
        Description = description ?? string.Empty;
        Year = year;
        Creator = creator;
        Platform = platform ?? GamePlatform.PC;
    }

    public override string ToString()
    {
        return $"{Name} ({Year}) - {GamePlatformName[Platform]}" + Environment.NewLine + Environment.NewLine + Description + Environment.NewLine;
    }
}
