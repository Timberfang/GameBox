using MediaBox.Config.Path;

namespace MediaBox.Config;

internal record class SaveConfig(string Name)
{
	public string Name { get; set; } = Name;
	public IList<LinkedPath> Paths { get; set; } = [];
}