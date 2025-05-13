namespace MediaBox.Config.Path;

internal class FilteredPath(string Path)
{
	internal FilteredPath(string Path, string Filter) : this(Path)
	{
		this.Filter = Filter;
	}

	internal string Path { get; set; } = Path;
	internal string Filter { get; set; } = "";

	internal IEnumerable<string> GetFiles()
	{
		if (!Directory.Exists(Path)) return [];

		return Directory.GetFiles(Path, Filter);
	}
}