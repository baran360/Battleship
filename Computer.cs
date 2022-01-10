namespace Battleship
{
    enum mod { free, searching, found }
    enum searchingDir { pozitive, negative }
    class Computer : Player
    {
        //Determines computer's current mod.
        //Free: Shooting at random coordinates until hitting a target.
        //Searching: Recently hit a target, looking for the rest of the ship.
        //Found: Hit two consecutive points. Keep shooting near points until being sure the ship has been sunk.
        public mod currentMod = mod.free;
        //After two consecutive hits, figuring out the direction of the ship (horizontal or vertical),
        //determining which side to shoot at next [Only used at "found" mod].
        public searchingDir currentDir;
        //After two consecutive hits, computer keeps shooting through one direction until missing a shot.
        //After missing, "currentDir" added to "directionsSearched" list and changed to other direction.
        //When two directions added to the list successfully, computer turn the mod to "free" again.
        public List<searchingDir> directionsSearched = new List<searchingDir>();
        //Consecutive successful hits added to the "pattern" list.
        //This list used to determine which point to shoot at next.
        public List<int[]> pattern = new List<int[]>();
        //After a successful hit, all four nearby points added to "possibleCoor" list.
        //Computer will try those point until getting another successful hit.
        public List<int[]> possibleCoor = new List<int[]>();
        //The direction of the last located ship [horizontal or vertical].
        public direction shipDirection;
    }
}
