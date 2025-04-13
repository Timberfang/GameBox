namespace GameBox;

public class Game
{
    public string Name { get; set; }
    public string Description { get; set; }
    public GamePlatform Platform { get; set; }
    public int Year { get; set; }
    public string? Developer { get; set; }
    public string? Publisher { get; set; }
    // public bool HasCover { get; } TODO
    public bool HasDeveloper { get { return null != Developer; } }
    public bool HasPublisher { get { return null != Publisher; } }
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

    public Game(string name, string? description = null, GamePlatform? platform = null, int year = 1970, string? developer = null, string? publisher = null)
    {
        Name = name;
        Description = description ?? string.Empty;
        Platform = platform ?? GamePlatform.PC;
        Year = year;
        Developer = developer;
        Publisher = publisher;
    }

    public override string ToString()
    {
        return $"{Name} ({Year}) - {GamePlatformName[Platform]}" + Environment.NewLine + Environment.NewLine + Description + Environment.NewLine;
    }
}
