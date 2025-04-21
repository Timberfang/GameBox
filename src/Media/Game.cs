using Humanizer;

namespace GameBox.Media;

public class Game : IMedia
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public string? Creator { get; set; }
    public string? Cover { get; set; }
    public GamePlatform Platform { get; set; }
    public MediaType Type => MediaType.Game;

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
        Cover = "cover.jpg";
        Platform = platform ?? GamePlatform.PC;
    }

    public override string ToString()
    {
        return $"{Name} ({Year}) - {Platform.Humanize()}" + Environment.NewLine + Environment.NewLine + Description + Environment.NewLine;
    }
}
