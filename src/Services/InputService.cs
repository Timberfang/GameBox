using Spectre.Console;
using Humanizer;
using GameBox.Media;

namespace GameBox.Services;

public static class Input
{
    public static Game NewGame()
    {
        AnsiConsole.WriteLine("Adding a new game...");
        AnsiConsole.WriteLine();
        Game output = new(
            AnsiConsole.Ask<string>("Name:"),
            AnsiConsole.Ask<string>("Description:"),
            GetGamePlatform(),
            AnsiConsole.Ask<int>("Release Year:"),
            AnsiConsole.Ask<string>("Creator:")
        );
        return output;
    }

    public static Game ChooseGame()
    {
        string ChosenGame = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a [blue]game[/]:")
                .AddChoices(FileService.GetMedia(MediaType.Game))
                .UseConverter(Path.GetFileNameWithoutExtension)
        );

        return FileService.LoadMedia<Game>(ChosenGame, MediaType.Game);
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
}
