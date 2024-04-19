using System.Numerics;

namespace DGD203
{
    public enum LocationType
    {
        Riddle,
        Furniture,
        Viewpoint,
        Environment
    }

    public class Location
    {
        #region VARIABLES


        public string Description { get; private set; }
        public Vector2 Coordinates { get; private set; }
        public LocationType Type { get; private set; }
        public List<Item> ItemsOnLocation { get; private set; }

        #endregion

        #region CONSTRUCTOR

        public Location(string locationName, LocationType type, Vector2 coordinates, List<Item> itemsOnLocation)
        {
            Description = locationName;
            Type = type;
            Coordinates = coordinates;
            ItemsOnLocation = itemsOnLocation; // The list is given by the constructor arguments
        }

        public Location(string locationName, LocationType type, Vector2 coordinates)
        {
            Description = locationName;
            Type = type;
            Coordinates = coordinates;
            ItemsOnLocation = new List<Item>(); // The list is created, but it has ZERO elements
        }

        #endregion

        #region METHODS

        public void RemoveItem(Item item)
        {
            ItemsOnLocation.Remove(item);
        }
        #endregion
    }
}