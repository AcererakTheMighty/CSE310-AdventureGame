using System;
using System.Collections.Generic;

namespace TextAdventureGame
{
    class Game
    {
        private Player _player;
        private CommandHandler _commandHandler;
        private bool _isRunning;

        public Game()
        {
            InitializeGame();
            _commandHandler = new CommandHandler(_player);
            _isRunning = true;
        }

        private void InitializeGame()
        {
            // Create locations
            Location townSquare = new Location("Town Square", "You are standing in the bustling town square. People are moving around, and vendors are selling their goods.");
            Location forest = new Location("Enchanted Forest", "Tall trees surround you, and the sounds of wildlife fill the air.");
            Location cave = new Location("Dark Cave", "It's dark and damp inside the cave. You can hear dripping water echoing around you.");
            Location castle = new Location("Ancient Castle", "The remnants of an ancient castle stand before you, its walls covered in ivy.");

            // Connect locations
            townSquare.AddExit("north", forest);
            forest.AddExit("south", townSquare);
            forest.AddExit("east", cave);
            cave.AddExit("west", forest);
            cave.AddExit("north", castle);
            castle.AddExit("south", cave);

            // Add items
            townSquare.AddItem(new Item("sword", "A sharp-looking sword lies here. On the sword are markings in a language not of this world."));
            forest.AddItem(new Item("shield", "A sturdy shield is propped against a tree next to an armored corpse."));
            cave.AddItem(new Item("torch", "An unlit torch is mounted on the wall."));
            castle.AddItem(new Item("key", "A small rusty key is glinting on the ground."));

            // Initialize player
            _player = new Player(townSquare);

            Console.WriteLine("Welcome to the World of Liurnia!");
            Console.WriteLine("Type 'help' to see a list of commands.\n");
            DescribeCurrentLocation();
        }

        public void Run()
        {
            while (_isRunning)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                _isRunning = _commandHandler.HandleCommand(input);

                // Trigger events after each command
                TriggerEvents();
            }
        }

        private void TriggerEvents()
        {
            if (_player.CurrentLocation.Name == "Ancient Castle" && _player.HasItem("key"))
            {
                Console.WriteLine("\nYou use the key to unlock a hidden chamber in the Ancient Castle.");
                Console.WriteLine("Inside, you find a treasure chest filled with gold and precious gems.");
                Console.WriteLine("Congratulations! You've found the treasure and won the game!");
                _isRunning = false;
            }

       
        }

        public void DescribeCurrentLocation()
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
    }
}