namespace Battleship
{
    enum state { blank, shot, ship, sunk }
    class Grid
    {
        public static state[][] blankGrid;
        public state[][] grid;
        public Grid()
        {
            state[][] blank = { new state[10],new state[10],new state[10],new state[10],new state[10],new state[10],new state[10],new state[10],new state[10],new state[10] };
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    blank[i][j] = state.blank;
                }
            }
            blankGrid = blank;
            grid = blankGrid;
        }
        public void PrintGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(grid[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
