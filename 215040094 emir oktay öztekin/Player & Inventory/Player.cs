namespace DGD203
{
	public class Player
	{
		private const int playerMaxHealth = 2;
		public string Name { get; private set; }
		public int Health { get; private set; }
		public Inventory Inventory { get; private set; }

		public Player(string name, List<Item> inventoryItems)
		{
			Name = name;
			Health = playerMaxHealth;
			Inventory = new Inventory();

			for (int i = 0; i < inventoryItems.Count; i++)
			{
				Inventory.AddItem(inventoryItems[i]);
			}
		}

		public void TakeItem(Item item)
		{
			Inventory.AddItem(item);
		}

		public void CheckInventory()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
			if (Inventory.Items.Count <= 1)
				Console.WriteLine($"You have 1 item");
			else
				Console.WriteLine($"You have {Inventory.Items.Count} items");

            for (int i = 0; i < Inventory.Items.Count; i++)
			{
				Console.WriteLine($"[{i + 1}] {Inventory.Items[i]}");
            }
            Console.ForegroundColor = ConsoleColor.Gray;

        }

		public void TakeDamage(int amount)
		{
			Health -= amount;
			if (Health < 0) Health = 0;

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"You take {amount} damage. You have {Health} health left");

			if (Health <= 0)
			{
				Console.WriteLine("YOU DIED");
			}

			Console.ForegroundColor = ConsoleColor.Gray;
		}
	}
}