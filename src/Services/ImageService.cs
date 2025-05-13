using System.Text.RegularExpressions;
using GameBox.Config;
using GameBox.Config.Path;

namespace GameBox.Services;

internal static class ImageService
{
	internal static void Sort(ImageConfig config, string destination)
	{
		if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);
		string targetPath = Path.Combine(destination, config.Name);
		if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
		Regex nameRegex = new(@$"{config.Name} \((\d\d\d\d)\)");

		foreach (FilteredPath source in config.Paths)
		{
			if (!Directory.Exists(source.Path)) continue;

			IEnumerable<string> sourceFiles = source.GetFiles();
			foreach (string file in sourceFiles) File.Move(file, Path.Join(targetPath, file));
		}

		string[] targetFiles = Directory.EnumerateFiles(targetPath).ToArray();
		string[] matchingFiles = targetFiles.Where(x => nameRegex.Match(x).Success).ToArray();

		if (matchingFiles.Length == 0)
		{
			int i = 1;
			foreach (string file in targetFiles)
			{
				string suffix = i.ToString("D4");
				string fileName = config.Name + $"({suffix})" + Path.GetExtension(file);
				File.Move(file, Path.Join(targetPath, fileName));
				i++;
			}
		}
		else
		{
			// Find any gaps (e.g. files Test (0001).jpg and Test (0004).jpg) should return 2 and 3 as missing
			IList<int> existingNumbers = [];
			foreach (string file in matchingFiles)
				existingNumbers.Add(int.Parse(nameRegex.Match(file).Groups[1].Value));
			int maxNumber = existingNumbers.Max();
			int minNumber = existingNumbers.Min();

			// TODO: Close gaps, if any, maintaining the original order for the files
		}
	}
}