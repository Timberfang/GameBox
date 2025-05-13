using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ConsoleAppFramework;
using GameBox.Media;

namespace GameBox.Services;

public static partial class InputService
{
	public static void StartCommandline(string[] args)
	{
		ConsoleApp.ConsoleAppBuilder app = ConsoleApp.Create();
		app.Add("create", Commands.Create);
		app.Add("read", Commands.Read);
		app.Add("list", Commands.List);
		app.Run(args);
	}

	[GeneratedRegex(@"(?<=').+(?=')")]
	private static partial Regex FileExceptionRegex();

	private static class Commands
	{
		/// <summary>
		///     Create new media
		/// </summary>
		/// <param name="name">-n, Title.</param>
		/// <param name="description">-d, Long description, up to one paragraph.</param>
		/// <param name="year">-y, Year of release.</param>
		/// <param name="creator">-c, Creator(s).</param>
		public static void Create(string name, string description, int year, string creator)
		{
			Game output = new(name, description, GamePlatform.PC, year, creator);
			FileService.SaveMedia(output);
		}

		/// <summary>
		///     Read media
		/// </summary>
		/// <param name="name">-n, Title.</param>
		/// <param name="type">-t, Media Type.</param>
		public static void Read(string name,
			[AllowedValues("game", "movie", "show",
				ErrorMessage = "Type must be one of the following: 'game', 'movie', 'show'.")]
			string type)
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
		}

		/// <summary>
		///     List media
		/// </summary>
		/// <param name="type">-t, Media Type.</param>
		public static void List(
			[AllowedValues("game", "movie", "show",
				ErrorMessage = "Type must be one of the following: 'game', 'movie', 'show'.")]
			string type)
		{
			IEnumerable<string> media = type switch
			{
				"game" => FileService.ListMedia<Game>(),
				"movie" => FileService.ListMedia<Movie>(),
				"show" => FileService.ListMedia<Show>(),
				_ => throw new ArgumentOutOfRangeException(nameof(type))
			};

			Console.WriteLine(string.Join(Environment.NewLine, media));
		}
	}
}