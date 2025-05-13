namespace MediaBox.Config.Path;

internal class LinkedPath(string Source, string Destination)
{
	public string Source { get; set; } = Source;
	public string Destination { get; set; } = Destination;

	internal void Link()
	{
		Directory.CreateDirectory(Destination);
		Directory.Move(Source, Destination);
		Directory.Delete(Source);
		File.CreateSymbolicLink(Source, Destination);
	}

	internal void Unlink()
	{
		File.Delete(Source);
		Directory.Move(Destination, Source);
		Directory.Delete(Destination);
	}
}