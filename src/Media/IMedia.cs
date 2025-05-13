namespace MediaBox.Media;

public interface IMedia
{
	public string Name { get; set; }
	public string Description { get; set; }
	public int Year { get; set; }
	public string? Creator { get; set; }
	public string? Cover { get; set; }
}