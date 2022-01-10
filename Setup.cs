namespace Battleship
{
    enum direction { horizontal, vertical }
    enum rowindex { A, B, C, D, E, F, G, H, I, J }
    class Setup
    {
        public static void UserSetup(Player player)
        {
            UserVariables(player,2);
            UserVariables(player,3);
            UserVariables(player,4);
            UserVariables(player,5);
        }
        public static void UserDefaultSetup(Player player)
        {
            int[][] coor2 = new int[5][] { new int[2], new int[2], new int[2], new int[2], new int[2] };
            coor2[0][0] = 1; coor2[0][1] = 1; coor2[1][0] = 1; coor2[1][1] = 2;
            int[][] coor3 = new int[5][] { new int[2], new int[2], new int[2], new int[2], new int[2] };
            coor3[0][0] = 3; coor3[0][1] = 5; coor3[1][0] = 4; coor3[1][1] = 5; coor3[2][0] = 5; coor3[2][1] = 5;
            int[][] coor4 = new int[5][] { new int[2], new int[2], new int[2], new int[2], new int[2] };
            coor4[0][0] = 5; coor4[0][1] = 8; coor4[1][0] = 6; coor4[1][1] = 8; coor4[2][0] = 7; coor4[2][1] = 8; coor4[3][0] = 8; coor4[3][1] = 8;
            int[][] coor5 = new int[5][] { new int[2], new int[2], new int[2], new int[2], new int[2] };
            coor5[0][0] = 9; coor5[0][1] = 2; coor5[1][0] = 9; coor5[1][1] = 3; coor5[2][0] = 9; coor5[2][1] = 4; coor5[3][0] = 9; coor5[3][1] = 5; coor5[4][0] = 9; coor5[4][1] = 6;

            PlaceShip(player, 2, coor2);
            PlaceShip(player, 3, coor3);
            PlaceShip(player, 4, coor4);
            PlaceShip(player, 5, coor5);
            Console.Clear();
            player.PrintGrid();

        }
        public static void ComputerSetup(Computer computer)
        {
            ComputerVariables(computer, 2);
            ComputerVariables(computer, 3);
            ComputerVariables(computer, 4);
            ComputerVariables(computer, 5);
        }
        private static void UserVariables(Player player, int length)
        {
            Console.Clear();
            int[][] coordinates;
            //Getting user input, checking their accuracy and if there aren't any issues getting the coordinate values.
            while (true)
            {
                player.PrintGrid();
                Console.WriteLine("Please place your {0}x1 ship", length);

                //Getting user input and assigning them to variables.
                int shipFrontX = GetIndex(typeof(int), "The X coordinate of the front of the ship [1-10]: ");
                int shipFrontY = GetIndex(typeof(rowindex), "The Y Coordinate of the front of the ship [A-J]: ");
                int shipBackX = GetIndex(typeof(int), "The X coordinate of the back of the ship [1-10]: ");
                int shipBackY = GetIndex(typeof(rowindex), "The Y Coordinate of the back of the ship [A-J]: ");

                //Checking the accuracy of user input.
                if (!IsCorrectDimension(shipFrontX, shipFrontY, shipBackX, shipBackY)) { Console.Clear(); Console.WriteLine("Wrong dimensions."); continue; }
                direction dir = GetDirection(shipFrontX, shipFrontY, shipBackX, shipBackY);
                if (!IsCorrectLength(length, dir, shipFrontX, shipFrontY, shipBackX, shipBackY)) { Console.Clear(); Console.WriteLine("The length is incorrect."); continue; }
                coordinates = GetCoordinates(length, dir, shipFrontX, shipFrontY, shipBackX, shipBackY);
                if (!NoCollision(player, length, coordinates)) { Console.Clear(); Console.WriteLine("There is already a ship exist there."); continue; }

                //If there is no mistake at inputs exit from the loop.
                break;
            }
            PlaceShip(player, length, coordinates);
            Console.Clear();
            player.PrintGrid();
        }
        private static void ComputerVariables(Computer computer, int length)
        {
            var random = new Random();
            direction[] directions = new direction[2] { direction.vertical, direction.horizontal };
            direction dir = (direction)directions.GetValue(random.Next(0, 2));
            int[][] coordinates = new int[5][];
            bool noCollision = false;

            while (!noCollision)
            {
                int mainDir = random.Next(0, 10);
                int sideDir = random.Next(0, 10 - (length - 1));
                
                switch (dir)
                {
                    case direction.vertical:
                        coordinates = GetCoordinates(length, dir, mainDir, sideDir, mainDir, sideDir + (length - 1));
                        break;

                    case direction.horizontal:
                        coordinates = GetCoordinates(length, dir, sideDir, mainDir, sideDir + (length - 1), mainDir);
                        break;
                }
                noCollision = NoCollision(computer, length, coordinates);
            }
            PlaceShip(computer, length, coordinates);
        }
        public static int GetIndex(Type type, string message)
        {
            int index = 0;
            string[] columns = {"1","2","3","4","5","6","7","8","9","10"};
            while (true)
            {
                Console.Write(message);
                var value = Console.ReadLine().ToUpper();
                if (typeof(rowindex) == type && Enum.IsDefined(typeof(rowindex),value))
                {
                    rowindex enumValue = (rowindex)Enum.Parse(typeof(rowindex), value);
                    index = (int)enumValue;
                    break;
                }
                if (typeof(int) == type && columns.Contains(value))
                {
                    index = Array.IndexOf(columns, value);
                    break;
                }
                Console.WriteLine("Your value is out of range. Please input a valid value.");
            }
            return index;
        }
        private static Boolean IsCorrectDimension(int shipFrontX, int shipFrontY, int shipBackX, int shipBackY)
        {
            if (shipFrontX != shipBackX && shipFrontY != shipBackY) { return false; }
            if (shipFrontX == shipBackX && shipFrontY == shipBackY) { return false; }
            return true;
        }
        private static direction GetDirection(int shipFrontX, int shipFrontY, int shipBackX, int shipBackY)
        {
            if (shipFrontX == shipBackX) { return direction.vertical; }
            return direction.horizontal;
        }
        private static Boolean IsCorrectLength(int length, direction dir, int shipFrontX, int shipFrontY, int shipBackX, int shipBackY)
        {
            switch (dir)
            {
                case direction.vertical:
                    return length == (Math.Abs(shipFrontY - shipBackY)+1);

                case direction.horizontal:
                    return length == (Math.Abs(shipFrontX - shipBackX)+1);
            }
            return false;
        }
        private static int[][] GetCoordinates(int length, direction dir, int shipFrontX, int shipFrontY, int shipBackX, int shipBackY)
        {
            int[][] coordinates = { new int[2], new int[2], new int[2], new int[2], new int[2] };
            switch (dir)
            {
                case direction.vertical:
                    int coorX = shipFrontX;
                    int coorY1 = Math.Min(shipFrontY,shipBackY);
                    for (int i = 0; i < length; i++)
                    {
                        int[] coor = new int[2] { coorX, coorY1 };
                        coordinates[i] = coor;
                        coorY1++;
                    }
                    break;

                case direction.horizontal:
                    int coorY = shipFrontY;
                    int coorX1 = Math.Min(shipFrontX,shipBackX);
                    for (int i = 0; i < length; i++)
                    {
                        int[] coor = new int[2] { coorX1, coorY };
                        coordinates[i] = coor;
                        coorX1++;
                    }
                    break;
            }
            return coordinates;
        }
        private static Boolean NoCollision(Player player, int length, int[][] coordinates)
        {
            for (int i = 0; i < length; i++)
            {
                if (player.selfGrid.grid[coordinates[i][1]][coordinates[i][0]] != state.blank)
                {
                    return false;
                }
            }
            return true;
        }
        private static void PlaceShip(Player player, int length, int[][] coordinates)
        {
            for (int i = 0; i < length; ++i)
            {
                player.selfGrid.grid[coordinates[i][1]][coordinates[i][0]] = state.ship;
            }
        }
    }
}
