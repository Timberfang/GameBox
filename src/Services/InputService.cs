using Spectre.Console;
using Humanizer;
using GameBox.Media;

namespace GameBox.Services;

public static class InputService
{
    // TODO: Update dictionary as types added
    private static readonly Dictionary<Type, MediaType> TypeToEnum = new()
    {
        { typeof(Game), MediaType.Game }
    };

    public static Game NewGame()
    {
        Game output = NewMedia<Game>();
        output.Platform = GetGamePlatform();
        return output;
    }

    public static T ChooseMedia<T>() where T : IMedia
    {
        MediaType type = TypeToEnum[typeof(T)];

        string ChosenMedia = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"Choose a [blue]{nameof(T)}[/]:")
                .AddChoices(FileService.GetMedia(type))
                .UseConverter(Path.GetFileNameWithoutExtension)
        );

        return FileService.LoadMedia<T>(ChosenMedia, type);
    }

    private static GamePlatform GetGamePlatform()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<GamePlatform>()
                .Title("Choose a [blue]platform[/] for this game:")
                .AddChoices(Enum.GetValues<GamePlatform>())
                .UseConverter(x => x.Humanize())
        );
    }

    private static T NewMedia<T>() where T : IMedia, new()
    {
        AnsiConsole.WriteLine($"Creating a new {nameof(T)}...");
        AnsiConsole.WriteLine();

        T output = new()
        {
            Name = AnsiConsole.Ask<string>("Name:"),
            Description = AnsiConsole.Ask<string>("Description:"),
            Year = AnsiConsole.Ask<int>("Release Year:"),
            Creator = AnsiConsole.Ask<string>("Creator:")
        };

        return output;
    }
}
