using System;

namespace TextAdventureGame
{
    class CommandHandler
    {
        private Player _player;
        private Game _game;

        public CommandHandler(Player player)
        {
            _player = player;
            // To allow access to game methods like DescribeCurrentLocation, we might need a reference to Game
            // However, to avoid circular dependencies, consider passing necessary methods or using events
            // For simplicity, we'll assume triggering events is handled within the Game class
        }

        public bool HandleCommand(string input)
        {
            string[] parts = input.Trim().ToLower().Split(' ', 2);
            string command = parts[0];
            string argument = parts.Length > 1 ? parts[1] : "";

            switch (command)
            {
                case "go":
                    Go(argument);
                    break;
                case "look":
                    Look();
                    break;
                case "inventory":
                    ShowInventory();
                    break;
                case "take":
                case "pick":
                    if (command == "pick" && argument.StartsWith("up "))
                        argument = argument.Substring(3);
                    Take(argument);
                    break;
                case "help":
                    ShowHelp();
                    break;
                case "exit":
                case "quit":
                    Console.WriteLine("Thank you for playing!");
                    return false;
                default:
                    Console.WriteLine("I don't understand that command.");
                    break;
            }
            return true;
        }

        private void Go(string direction)
        {
            if (string.IsNullOrEmpty(direction))
            {
                Console.WriteLine("Go where?");
                return;
            }

            if (_player.CurrentLocation.Exits.TryGetValue(direction, out Location newLocation))
            {
                _player.CurrentLocation = newLocation;
                Console.WriteLine($"\nYou move {direction} to the {newLocation.Name}.");
                Console.WriteLine(newLocation.Description);
                if (newLocation.Items.Count > 0)
                {
                    Console.WriteLine("You see the following items:");
                    foreach (var item in newLocation.Items)
                    {
                        Console.WriteLine($"- {item.Name}");
                    }
                }
                Console.WriteLine("Exits:");
                foreach (var exit in newLocation.Exits.Keys)
                {
                    Console.WriteLine($"- {exit}");
                }
            }
            else
            {
                Console.WriteLine("You can't go that way.");
            }
        }

        private void Look()
        {
            var location = _player.CurrentLocation;
            Console.WriteLine($"\n{location.Name}");
            Console.WriteLine(location.Description);
            if (location.Items.Count > 0)
            {
                Console.WriteLine("You see the following items:");
                foreach (var item in location.Items)
                {
                    Console.WriteLine($"- {item.Name}");
                }
            }
            Console.WriteLine("Exits:");
            foreach (var exit in location.Exits.Keys)
            {
                Console.WriteLine($"- {exit}");
            }
        }

        private void ShowInventory()
        {
            _player.ShowInventory();
        }

        private void Take(string itemName)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                Console.WriteLine("Take what?");
                return;
            }

            var location = _player.CurrentLocation;
            var item = location.Items.Find(i => i.Name == itemName.ToLower());
            if (item != null)
            {
                _player.AddItem(item);
                location.RemoveItem(item);
                Console.WriteLine($"You picked up the {item.Name}.");
            }
            else
            {
                Console.WriteLine($"There is no {itemName} here.");
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine("\nAvailable commands:");
            Console.WriteLine("- go [direction] (e.g., go north)");
            Console.WriteLine("- look");
            Console.WriteLine("- take [item] or pick up [item]");
            Console.WriteLine("- inventory");
            Console.WriteLine("- help");
            Console.WriteLine("- exit or quit");
        }
    }
}