namespace GameBox.Config.Path;

internal class FilteredPath(string Path)
{
    internal string Path { get; set; } = Path;
    internal string Filter { get; set; } = "";

    internal FilteredPath(string Path, string Filter) : this(Path)
    {
        this.Filter = Filter;
    }

    internal IEnumerable<string> GetFiles()
    {
        if (!Directory.Exists(Path)) { return []; }
        else { return Directory.GetFiles(Path, Filter); }
    }
}
