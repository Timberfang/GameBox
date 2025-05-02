using GameBox.Config.Path;

namespace GameBox.Config;

internal record class SaveConfig(string Name)
{
	public string Name { get; set; } = Name;
	public IList<LinkedPath> Paths { get; set; } = [];
}