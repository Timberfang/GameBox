using GameBox.Services;

namespace GameBox;

internal static class Program
{
	private static async Task Main(string[] args)
	{
		await InputService.StartCommandLine(args);
	}
}