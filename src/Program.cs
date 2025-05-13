using GameBox.Services;

namespace GameBox;

internal static class Program
{
	private static void Main(string[] args)
	{
		InputService.StartCommandline(args);
	}
}