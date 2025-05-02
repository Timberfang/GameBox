using GameBox.Media;
using System.CommandLine;
using System.Text.RegularExpressions;

namespace GameBox.Services;

public static partial class InputService
{
	public static async Task<int> StartCommandLine(string[] args)
	{
		Option<string> nameOption = new Option<string>("--name");
		Option<string> descriptionOption = new Option<string>("--description", getDefaultValue: () => string.Empty);
		Option<int> yearOption = new Option<int>("--year", getDefaultValue: () => 1970);
		Option<string> creatorOption = new Option<string>("--creator", getDefaultValue: () => string.Empty);
		Option<string> typeOption = new Option<string>("--type");
		nameOption.IsRequired = true;
		typeOption.IsRequired = true;
		
		RootCommand rootCommand = new RootCommand("Media manager.");
		Command createCommand = new Command("create")
		{
			nameOption,
			descriptionOption,
			yearOption,
			creatorOption
		};
		Command readCommand = new Command("read")
		{
			nameOption,
			typeOption
		};
		rootCommand.AddCommand(createCommand);
		rootCommand.AddCommand(readCommand);
		
		createCommand.SetHandler((name, description, year, creator) =>
			{
				Game output = new Game(name, description, GamePlatform.PC, year, creator);
				FileService.SaveMedia(output);
			},
			nameOption, descriptionOption, yearOption, creatorOption);
		readCommand.SetHandler((name, type) =>
			{
				try
				{
					IMedia output = type switch
					{
						"game" => FileService.LoadMedia<Game>(name),
						// "movie" => FileService.LoadMedia<Movie>(name),
						_ => throw new ArgumentOutOfRangeException(nameof(type))
					};

					Console.WriteLine(output.ToString());
				}
				catch (FileNotFoundException e)
				{
					string filePath = FileExceptionRegex().Match(e.Message).Value;
					string fileName = Path.GetFileNameWithoutExtension(filePath);
					Console.WriteLine($"No media of type '{type}' named '{fileName}' was found.");
				}
			},
			nameOption, typeOption);
		
		return await rootCommand.InvokeAsync(args);
	}

    [GeneratedRegex(@"(?<=').+(?=')")]
    private static partial Regex FileExceptionRegex();
}