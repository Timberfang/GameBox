namespace GameBox.Media;

public static class NameConverter
{
    public static string GetTypeName(MediaType type)
    {
        return type switch
        {
            MediaType.Game => "Game",
            MediaType.Movie => "Movie",
            MediaType.Show => "Show",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
    }

    public static string GetTypeName(IMedia media) => GetTypeName(media.Type);

    public static string GetFolderName(MediaType type)
    {
        return type switch
        {
            MediaType.Game => "games",
            MediaType.Movie => "movies",
            MediaType.Show => "shows",
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
    }

    public static string GetFolderName(IMedia media) => GetFolderName(media.Type);
}
