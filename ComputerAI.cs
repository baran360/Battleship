namespace Battleship
{
    //available list will be updated after every hit.
    class ComputerAI
    {
        private static Boolean HitSuccess(Player player, int[] coor)
        {
            if (player.selfGrid.grid[coor[0]][coor[1]] == state.ship) { return true; }
            return false;
        }
        public static int[] Shoot(Computer computer, Player player)
        {
            int[] coor = GetShootingCoor(computer);
            //assign a method for this code.
            foreach (int[] point in computer.available)
            {
                if (point[0] == coor[0] && point[1] == coor[1])
                {
                    computer.available.Remove(point);
                    break;
                }
            }

            if (HitSuccess(player, coor))
            {
                Console.WriteLine("You hit a target successfully.");
                computer.pattern.Add(coor);
                computer.enemyGrid.grid[coor[0]][coor[1]] = state.sunk;
                player.selfGrid.grid[coor[0]][coor[1]] = state.sunk;
                Game.playerTurn = computer;
                Random random = new Random();

                switch (computer.currentMod)
                {
                    case mod.free:
                        int[] top = { coor[0], coor[1] + 1 };
                        int[] bottom = { coor[0], coor[1] - 1 };
                        int[] right = { coor[0] + 1, coor[1] };
                        int[] left = { coor[0] - 1, coor[1] };
                        computer.possibleCoor.Add(top); computer.possibleCoor.Add(bottom); computer.possibleCoor.Add(right); computer.possibleCoor.Add(left);
                        computer.currentMod = mod.searching;
                        break;
                    case mod.searching:
                        computer.possibleCoor.Clear();
                        searchingDir[] searchingDirs = new searchingDir[2] { searchingDir.pozitive, searchingDir.negative };
                        computer.currentDir = searchingDirs[random.Next(0, 2)];
                        if (computer.pattern[0][0] == computer.pattern[1][0]) { computer.shipDirection = direction.vertical; }
                        else { computer.shipDirection = direction.horizontal; }
                        computer.currentMod = mod.found;
                        break;
                    case mod.found:
                        if (computer.pattern.Count == 5)
                        {
                            computer.pattern.Clear();
                            computer.directionsSearched.Clear();
                            computer.currentMod = mod.free;
                        }
                        break;
                }
                player.ships--;
            }
            else
            //If the hit misses.
            {
                Console.WriteLine("You couldn't shoot any ships.");
                computer.enemyGrid.grid[coor[0]][coor[1]] = state.shot;
                player.selfGrid.grid[coor[0]][coor[1]] = state.shot;
                Game.playerTurn = player;

                switch (computer.currentMod)
                {
                    case mod.free:
                        break;
                    case mod.searching:
                        computer.possibleCoor.Remove(coor);
                        break;
                    case mod.found:
                        computer.directionsSearched.Add(computer.currentDir);
                        if (computer.directionsSearched.Count == 2)
                        {
                            computer.pattern.Clear();
                            computer.directionsSearched.Clear();
                            computer.currentMod = mod.free;
                            break;
                        }
                        if (computer.currentDir == searchingDir.pozitive) { computer.currentDir = searchingDir.negative; }
                        else { computer.currentDir = searchingDir.pozitive; }
                        break;
                }
            }
            return coor;
        }
        public static int[] GetShootingCoor(Computer computer)
        {
            int[] coor = new int[2];
            var random = new Random();
            int index = 0;
            
            switch (computer.currentMod)
            {
                case mod.free:
                    index = random.Next(0, computer.available.Count);
                    coor = computer.available[index];
                    break;

                case mod.searching:
                    bool loop = true;
                    while (loop)
                    {
                        //error here
                        index = random.Next(0, computer.possibleCoor.Count);
                        coor = computer.possibleCoor[index];
                        foreach (int[] point in computer.available)
                        {
                            if (point[0] == coor[0] && point[1] == coor[1])
                            {
                                computer.available.Remove(point);
                                loop = false;
                                break;
                            }
                        }
                        computer.possibleCoor.RemoveAt(index);
                    }
                    break;
                case mod.found:
                    int[] coorX = new int[computer.pattern.Count];
                    int[] coorY = new int[computer.pattern.Count];
                    for (int i = 0; i < computer.pattern.Count; i++)
                    {
                        coorX[i] = computer.pattern[i][0];
                        coorY[i] = computer.pattern[i][1];
                    }
                    int minX = coorX.Min(); int maxX = coorX.Max();
                    int minY = coorY.Min(); int maxY = coorY.Max();
                    switch (computer.currentDir, computer.shipDirection)
                    {
                        case (searchingDir.pozitive, direction.vertical):
                            coor[0] = minX; coor[1] = maxY + 1; break;
                        case (searchingDir.negative, direction.vertical):
                            coor[0] = minX; coor[1] = minY -1; break;
                        case (searchingDir.pozitive, direction.horizontal):
                            coor[0] = maxX + 1; coor[1] = minY; break;
                        case (searchingDir.negative, direction.horizontal):
                            coor[0] = minX - 1; coor[1] = minY; break;
                    }
                    break;
            }
            return coor;
        }
    }
}
