namespace Battleship
{
    class Program
    {
        public static void Main(string[] args)
        {
            var player = new Player();
            var computer = new Computer();
            Game.Start(player, computer);
        }
    }
}