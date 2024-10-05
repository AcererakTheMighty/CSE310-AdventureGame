using System.Collections.Generic;

namespace TextAdventureGame
{
    class Player
    {
        public Location CurrentLocation { get; set; }
        public List<Item> Inventory { get; }

        public Player(Location startingLocation)
        {
            CurrentLocation = startingLocation;
            Inventory = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }

        public bool HasItem(string itemName)
        {
            return Inventory.Exists(i => i.Name == itemName.ToLower());
        }

        public void ShowInventory()
        {
            if (Inventory.Count == 0)
            {
                System.Console.WriteLine("Your inventory is empty.");
                return;
            }

            System.Console.WriteLine("You are carrying:");
            foreach (var item in Inventory)
            {
                System.Console.WriteLine($"- {item.Name}");
            }
        }
    }
}