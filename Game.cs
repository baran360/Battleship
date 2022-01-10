namespace Battleship
{
    class Game
    {
        public static Player playerTurn;
        public static Player playerTurnRound;
        public static void Start(Player player, Computer computer)
        {
            int[] coor = new int[2];
            Setup.UserSetup(player);
            //Setup.UserDefaultSetup(player);
            Setup.ComputerSetup(computer);
            playerTurn = WhoStarts(player, computer);
            Console.ReadKey();
            Console.Clear();
            while (player.ships > 0 && computer.ships > 0)
            {
                playerTurnRound = playerTurn;
                player.PrintGrid();
                if (playerTurn == player)
                {
                    Console.WriteLine("It's your turn.");
                    coor = Shoot(player, computer);
                }
                else
                {
                    Console.Write("It is Computer's turn...");
                    Console.ReadKey();
                    coor = ComputerAI.Shoot(computer, player);
                }
                Console.Clear();
                player.PrintGrid();

                if (playerTurnRound == player && playerTurn == player)
                { Console.WriteLine("Bullseye!\nYou shot the target at [{0}{1}]", "ABCDEFGHIJ"[coor[1]], coor[0] + 1); }
                else if (playerTurnRound == player && playerTurn == computer)
                { Console.WriteLine("Miss.\nYou couldn't shoot any targets at [{0}{1}]", "ABCDEFGHIJ"[coor[1]], coor[0] + 1); }
                else if (playerTurnRound == computer && playerTurn == computer)
                { Console.WriteLine("Hit!\nComputer shot one of your ships at [{0}{1}]", "ABCDEFGHIJ"[coor[0]], coor[1] + 1); }
                else if (playerTurnRound == computer && playerTurn == player)
                { Console.WriteLine("Miss.\nComputer couldn't shoot any of your ships at [{0}{1}]", "ABCDEFGHIJ"[coor[0]], coor[1] + 1); }

                Console.Write("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            
            GetWinner(player, computer);
        }
        private static void GetWinner(Player player, Computer computer)
        {
            if (player.ships == 0)
            {
                Console.WriteLine("You lost the game. Computer won.");
            }
            else
            {
                Console.WriteLine("You won the game!");
            }
        }
        private static Player WhoStarts(Player player, Computer computer)
        {
            var random = new Random();
            int randomNum = random.Next(0,2);
            if (randomNum == 0) { Console.Write("You will start the game..."); return player; }
            Console.Write("Computer will start the game...");
            return computer;
        }
        public static int[] GetShootingCoor(Player player)
        {
            int[] coor = new int[2];

            while (true)
            {
                Console.WriteLine("Please specify the point you want to shoot at.");
                coor[0] = Setup.GetIndex(typeof(int), "Input the X coordinate [1-10]: ");
                coor[1] = Setup.GetIndex(typeof(rowindex), "Input the Y coordinate [A-J]: ");
                if (!IsBlank(player, coor)) { Console.WriteLine("You previously shot at that location"); continue; }
                break;
            }
            return coor;
        }
        public static int[] Shoot(Player player, Computer computer)
        {
            int[] coor = GetShootingCoor(player);
            player.available.Remove(coor);
            bool isSuccess = computer.selfGrid.grid[coor[1]][coor[0]] == state.ship;
            if (isSuccess)
            {
                computer.selfGrid.grid[coor[1]][coor[0]] = state.sunk;
                player.enemyGrid.grid[coor[1]][coor[0]] = state.sunk;
                computer.ships--;
            }
            else
            {
                computer.selfGrid.grid[coor[1]][coor[0]] = state.shot;
                player.enemyGrid.grid[coor[1]][coor[0]] = state.shot;
                playerTurn = computer;
            }
            return coor;
        }
        private static Boolean IsBlank(Player player, int[] coor)
        {
            return player.enemyGrid.grid[coor[1]][coor[0]] == state.blank;
        }
    }
}
