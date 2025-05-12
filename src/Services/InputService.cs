using System.CommandLine;
using System.Text.RegularExpressions;
using GameBox.Media;

namespace GameBox.Services;

public static partial class InputService
{
	public static async Task<int> StartCommandLine(string[] args)
	{
		Option<string> nameOption = new("--name") { IsRequired = true };
		Option<string> descriptionOption = new("--description", () => string.Empty);
		Option<int> yearOption = new("--year", () => 1970);
		Option<string> creatorOption = new("--creator", () => string.Empty);
		Option<string> typeOption = new("--type") { IsRequired = true };
		typeOption.FromAmong("game", "movie", "show");
		nameOption.AddAlias("-n");
		typeOption.AddAlias("-t");

		RootCommand rootCommand = new("Media manager.");
		Command createCommand = new("create")
		{
			nameOption,
			descriptionOption,
			yearOption,
			creatorOption
		};
		Command readCommand = new("read")
		{
			nameOption,
			typeOption
		};
		Command listCommand = new("list")
		{
			typeOption
		};
		rootCommand.AddCommand(createCommand);
		rootCommand.AddCommand(readCommand);
		rootCommand.AddCommand(listCommand);

		createCommand.SetHandler((name, description, year, creator) =>
			{
				Game output = new(name, description, GamePlatform.PC, year, creator);
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
						"movie" => FileService.LoadMedia<Movie>(name),
						"show" => FileService.LoadMedia<Show>(name),
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
		listCommand.SetHandler((type) =>
		{
			IEnumerable<string> media = type switch
			{
				"game" => FileService.ListMedia<Game>(),
				"movie" => FileService.ListMedia<Movie>(),
				"show" => FileService.ListMedia<Show>(),
				_ => throw new ArgumentOutOfRangeException(nameof(type))
			};

			Console.WriteLine(string.Join(Environment.NewLine, media));
		}, typeOption);

		return await rootCommand.InvokeAsync(args);
	}

	[GeneratedRegex(@"(?<=').+(?=')")]
	private static partial Regex FileExceptionRegex();
}