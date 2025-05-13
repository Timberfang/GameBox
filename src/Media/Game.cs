namespace MediaBox.Media;

public class Game : IMedia
{
	public static readonly Dictionary<GamePlatform, string> PlatformFriendlyName = new()
	{
		{ GamePlatform.PC, "PC (Windows)" },
		{ GamePlatform.N64, "Nintendo 64" },
		{ GamePlatform.NWiiU, "Nintendo Wii U" },
		{ GamePlatform.N3DS, "Nintendo 3DS" },
		{ GamePlatform.NSwitch, "Nintendo Switch" }
	};

	public Game()
	{
		Name = string.Empty;
		Description = string.Empty;
	}

	public Game(string name, string? description = null, GamePlatform? platform = null, int year = 1970,
		string? creator = null)
	{
		Name = name;
		Description = description ?? string.Empty;
		Year = year;
		Creator = creator ?? string.Empty;
		Cover = "cover.jpg";
		Platform = platform ?? GamePlatform.PC;
	}

	public GamePlatform Platform { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public int Year { get; set; }
	public string? Creator { get; set; }
	public string? Cover { get; set; }

	public override string ToString()
	{
		return $"{Name} ({Year}) - {PlatformFriendlyName[Platform]}" + Environment.NewLine + Environment.NewLine +
		       Description + Environment.NewLine;
	}
}