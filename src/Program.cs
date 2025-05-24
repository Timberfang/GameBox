using Avalonia;
using MediaBox.Services;

namespace MediaBox;

internal static class Program
{
	private static void Main(string[] args)
	{
		if (args.Length > 0)
			InputService.StartCommandline(args);
		else
			InputService.StartGui(args);
	}

	// Avalonia configuration, don't remove; also used by visual designer.
	// ReSharper disable once UnusedMember.Local
	private static AppBuilder BuildAvaloniaApp()
	{
		return InputService.BuildGui();
	}
}