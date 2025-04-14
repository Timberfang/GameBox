using Spectre.Console;
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
            AnsiConsole.Ask<int>("Release Year:")
        );
        return output;
    }

    private static GamePlatform GetGamePlatform()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<GamePlatform>()
                .Title("What [blue]platform[/] is this game for?")
                .AddChoices(Game.GamePlatformName.Keys)
                .UseConverter(x => Game.GamePlatformName[x])
        );
    }
}
