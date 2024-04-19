using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DGD203
{
    internal class ClueLocation : Location
    {
        public string Clue { get; private set; }
        public ClueLocation(string clue, string locationName, LocationType type, Vector2 coordinates, List<Item> itemsOnLocation) : base(locationName, type, coordinates, itemsOnLocation)
        {
            Clue = clue;
        }

        public ClueLocation(string clue, string locationName, LocationType type, Vector2 coordinates) : base(locationName, type, coordinates)
        {
            Clue = clue;
        }
    }
}
