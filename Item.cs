namespace TextAdventureGame
{
    class Item
    {
        public string Name { get; }
        public string Description { get; }

        public Item(string name, string description)
        {
            Name = name.ToLower();
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}