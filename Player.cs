namespace Battleship
{
    class Player
    {
        //The grid that player places their ships. Shown on the left side of the screen and updated each round.
        public Grid selfGrid;
        //The enemy's grid but ships are not visible. Only to show the player which points did they shoot before.
        public Grid enemyGrid;
        //The point that can be shooted at. Only the Computer object uses this list.
        public List<int[]> available;
        //Has every possible coordinates inside (100 elements). Used to create "available" list.
        public static List<int[]> blankAvailable;
        //The sum of lengths of the ships. Game finishes when this value reaches to 0.
        public int ships = 14;
        public Player()
        {
            selfGrid = new Grid();
            enemyGrid = new Grid();
            CreateAvailable();
        }
        public void CreateAvailable()
        {
            List<int[]> blankAvailable = new List<int[]>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int[] var = { i, j };
                    blankAvailable.Add(var);
                }
            }
            available = blankAvailable;
        }
        private void PrintRow(Grid grid, int row)
        {
            for (int column = 0; column < 10; column++)
            {
                switch (grid.grid[row][column])
                {
                    case state.blank:
                        Console.Write("[ ] ");
                        break;
                    case state.shot:
                        Console.Write("[x] ");
                        break;
                    case state.ship:
                        Console.Write("[O] ");
                        break;
                    case state.sunk:
                        Console.Write("[*] ");
                        break;
                }
            }
        }
        public void PrintGrid()
        {
            string letters = "ABCDEFGHIJ";
            Console.WriteLine("\t           YOUR AREA \t\t\t\t          ENEMY AREA\n");
            Console.WriteLine("     1   2   3   4   5   6   7   8   9   10 \t     1   2   3   4   5   6   7   8   9   10\n");
            for (int row = 0; row < 10; row++)
            {
                Console.Write(letters[row] + "   ");
                PrintRow(selfGrid, row);
                Console.Write("\t");
                Console.Write(letters[row] + "   ");
                PrintRow(enemyGrid, row);
                Console.WriteLine("\n");
            }
        }
    }
}
