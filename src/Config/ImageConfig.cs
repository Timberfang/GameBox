using MediaBox.Config.Path;

namespace MediaBox.Config;

internal record class ImageConfig(string Name)
{
	internal string Name { get; set; } = Name;
	internal IList<FilteredPath> Paths { get; set; } = [];
}