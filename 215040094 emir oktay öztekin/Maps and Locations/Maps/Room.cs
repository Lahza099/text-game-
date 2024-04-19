using System.Numerics;

namespace DGD203
{
    internal class Room : MapBase
    {
        public Room(Game game, int width, int height) : base(game, width, height)
        {

        }

        protected override void GenerateLocations()
        {
            _locations = new Location[8];

            #region Location 1
            Vector2 centerLightLocation = new Vector2(0, 0);
            _locations[0] = new Location("You stand in the center of the room", LocationType.Furniture, centerLightLocation);
            #endregion

            #region Location 2
            Vector2 birckLocation = new Vector2(-2, 2);
            List<Item> brickItems = new List<Item>()
            {
                Item.Charm
            };
            _locations[1] = new Location("Cracked brickwork is left exposed under the wallpaper", LocationType.Environment, birckLocation, brickItems);
            #endregion

            #region Location 3
            Vector2 chairLocation = new Vector2(1, -2);
            _locations[2] = new Location("A chair sits broken and forgotten", LocationType.Furniture, chairLocation);
            #endregion

            #region Location 4
            Vector2 lampLocation = new Vector2(1, 1);
            List<Item> lampItems = new List<Item>()
            {
                Item.Coin
            };
            _locations[3] = new Location("A lamp stands in the corner", LocationType.Furniture, lampLocation, lampItems);
            #endregion

            #region Location 5
            Vector2 floorboardLocation = new Vector2(2, 2);
            List<Item> floorboardItems = new List<Item>()
            { 
                Item.Rune
            };
            _locations[4] = new ClueLocation("I bounce back when you call, repeating your words in an empty hall.", "You find a broken floor board", LocationType.Environment, floorboardLocation, floorboardItems);
            #endregion

            #region Location 6
            Vector2 windowLocation = new Vector2(2, 0);
            _locations[5] = new ClueLocation("In mountains high or valleys low, I linger where the breezes blow.", "You find a window with a note stuck to it", LocationType.Viewpoint, windowLocation);
            #endregion

            #region Location 7
            Vector2 npcLocation = new Vector2(-2, -2);
            _locations[6] = new Location("NPC", LocationType.Riddle, npcLocation);
            #endregion

            #region Location 8
            Vector2 writingOnWallLocation = new Vector2(0, -1);
            _locations[7] = new ClueLocation("I bounce off canyon walls and through dense forest halls, but in a vacuum, I won't be found at all.", "You find some writing on the wall", LocationType.Viewpoint, writingOnWallLocation);
            #endregion
        }

        public override void CheckForLocation(Vector2 coordinates)
        {
            Console.WriteLine($"You are now standing on {_coordinates[0]},{_coordinates[1]}");

            Location location; 
            if (IsOnLocation(coordinates, out location))
            {
                switch(location.Type)
                {
                    case LocationType.Riddle:
                        Riddle riddle = new Riddle(_game, location);
                        riddle.StartCombat();

                        if (riddle.RiddleCompleted)
                        {
                            MapCompleted = true;
                        }
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"{location.Description}");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        if (location.GetType() == typeof(ClueLocation))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"You find a clue");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{((ClueLocation)location).Clue}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        if (HasItem(location))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"There is a {location.ItemsOnLocation[0]} here");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"There is nothing else of interest here");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        break;
                }
            }
        }
    }
}
