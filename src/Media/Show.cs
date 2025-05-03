namespace GameBox.Media;

public class Show : IMedia
{
	public Show(string name, string? description = null, int year = 1970, string? creator = null)
	{
		Name = name;
		Description = description ?? string.Empty;
		Year = year;
		Creator = creator ?? string.Empty;
		Cover = "cover.jpg";
	}

	public string Name { get; set; }
	public string Description { get; set; }
	public int Year { get; set; }
	public string? Creator { get; set; }
	public string? Cover { get; set; }

	public override string ToString()
	{
		return $"{Name} ({Year})" + Environment.NewLine + Environment.NewLine + Description + Environment.NewLine;
	}
}