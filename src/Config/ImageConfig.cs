using GameBox.Config.Path;

namespace GameBox.Config;

internal record class ImageConfig(string Name)
{
    internal string Name { get; set; } = Name;
    internal IList<FilteredPath> Paths { get; set; } = [];
}
