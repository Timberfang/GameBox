namespace GameBox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var TestGame = Input.NewGame();
            Console.Clear();
            Console.WriteLine(TestGame.ToString());
        }
    }
}
