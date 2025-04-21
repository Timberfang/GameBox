using GameBox.Services;

namespace GameBox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var TestGame = InputService.NewGame();
            Console.Clear();
            Console.WriteLine(TestGame.ToString());
        }
    }
}
