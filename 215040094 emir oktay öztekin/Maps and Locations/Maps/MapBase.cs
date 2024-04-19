using System.Numerics;

namespace DGD203
{
	public abstract class MapBase
	{
		protected Game _game;
		protected Vector2 _coordinates;
		protected int[] _widthBoundaries;
		protected int[] _heightBoundaries;
		protected Location[] _locations;
		public bool MapCompleted { get; protected set; }

		public MapBase(Game game, int width, int height)
		{
			_game = game;
            // Setting the width boundaries
            int widthBoundary = (width - 1) / 2;

			_widthBoundaries = new int[2];
			_widthBoundaries[0] = -widthBoundary;
			_widthBoundaries[1] = widthBoundary;

			// Setting the height boundaries
			int heightBoundary = (height - 1) / 2;

			_heightBoundaries = new int[2];
			_heightBoundaries[0] = -heightBoundary;
			_heightBoundaries[1] = heightBoundary;

			// Setting starting coordinates
			_coordinates = new Vector2(0, 0);

			GenerateLocations();

			// Display result message
			Console.WriteLine($"Created map with size {width}x{height}");
		}

		#region Coordinates

		public Vector2 GetCoordinates()
		{
			return _coordinates;
		}

		public void SetCoordinates(Vector2 newCoordinates)
		{
			_coordinates = newCoordinates;
		}

		#endregion

		#region Movement

		public void MovePlayer(int x, int y)
		{
			int newXCoordinate = (int)_coordinates[0] + x;
			int newYCoordinate = (int)_coordinates[1] + y;

			if (!CanMoveTo(newXCoordinate, newYCoordinate))
			{
				Console.WriteLine("You can't go that way");
				return;
			}

			_coordinates[0] = newXCoordinate;
			_coordinates[1] = newYCoordinate;

			CheckForLocation(_coordinates);
		}

		private bool CanMoveTo(int x, int y)
		{
			return !(x < _widthBoundaries[0] || x > _widthBoundaries[1] || y < _heightBoundaries[0] || y > _heightBoundaries[1]);
		}

		#endregion

		#region Locations

		protected abstract void GenerateLocations();

		public abstract void CheckForLocation(Vector2 coordinates);

		protected bool IsOnLocation(Vector2 coords, out Location foundLocation)
		{
			for (int i = 0; i < _locations.Length; i++)
			{
				if (_locations[i].Coordinates == coords)
				{
					foundLocation = _locations[i];
					return true;
				}
			}

			foundLocation = null;
			return false;
		}

		protected bool HasItem(Location location)
		{
			return location.ItemsOnLocation.Count != 0;
		}

		protected bool HasItem(Location location, Item itemType)
		{
			return location.ItemsOnLocation.Count != 0 && location.ItemsOnLocation.Contains(itemType);
		}

		public void TakeItem(Player player, Vector2 coordinates)
		{
			if (IsOnLocation(coordinates, out Location location))
			{
				if (HasItem(location))
				{
					Item itemOnLocation = location.ItemsOnLocation[0];

					player.TakeItem(itemOnLocation);
					location.RemoveItem(itemOnLocation);

					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine($"You took the {itemOnLocation}");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    return;
				}
			}

			Console.WriteLine("There is nothing to take here!");
		}

		public void RemoveItemFromLocation(Item item)
		{
			for (int i = 0; i < _locations.Length; i++)
			{
				if (_locations[i].ItemsOnLocation.Contains(item))
				{
					_locations[i].RemoveItem(item);
				}
			}
		}

		#endregion
	}
}