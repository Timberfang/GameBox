using Spectre.Console;
using GameBox.Media;

namespace GameBox.Services;

public static class InputService
{
    public static Game NewGame()
    {
        Game output = NewMedia<Game>();
        output.Platform = GetGamePlatform();
        return output;
    }

    public static T ChooseMedia<T>() where T : IMedia
    {
        string ChosenMedia = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"Choose a {nameof(T)}:")
                .AddChoices(FileService.GetMedia<T>())
                .UseConverter(Path.GetFileNameWithoutExtension)
        );

        return FileService.LoadMedia<T>(ChosenMedia);
    }

    private static GamePlatform GetGamePlatform()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<GamePlatform>()
                .Title("Choose a platform for this game:")
                .AddChoices(Enum.GetValues<GamePlatform>())
                .UseConverter(x => Game.PlatformFriendlyName[x])
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
