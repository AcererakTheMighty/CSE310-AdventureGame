using System.Collections.Generic;

namespace TextAdventureGame
{
    class Location
    {
        public string Name { get; }
        public string Description { get; }
        public Dictionary<string, Location> Exits { get; }
        public List<Item> Items { get; }

        public Location(string name, string description)
        {
            Name = name;
            Description = description;
            Exits = new Dictionary<string, Location>();
            Items = new List<Item>();
        }

        public void AddExit(string direction, Location location)
        {
            Exits[direction.ToLower()] = location;
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }
}