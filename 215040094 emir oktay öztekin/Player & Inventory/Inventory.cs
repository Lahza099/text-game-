namespace DGD203
{
    public enum Item
    {
        Charm,
        Rune,
        Coin,
        Clue,
    }

    public class Inventory
	{
		public List<Item> Items { get; private set; }

		public Inventory()
		{
			Items = new List<Item>();
		}

		public void AddItem(Item item)
		{
			if (Items.Contains(item))
			{
				return;
			}

			Items.Add(item);
		}

		public void RemoveItem(Item item)
		{
			if (!Items.Contains(item))
			{
				return;
			}

			Items.Remove(item);
		}

		public bool HasItem(Item item)
		{
			return Items.Contains(item);
		}
	}
}
