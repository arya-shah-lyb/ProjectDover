﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDover
{
    public class RoomManager
    {
        private List<Room> Rooms { get; set; }
        private long CurrentRoomId { get; set; }
        private Room CurrentRoom { get { return Rooms.First(p => p.Id == CurrentRoomId); } }

        public RoomManager()
        {
            CurrentRoomId = 0;

            Rooms = new List<Room>();
            Rooms.Add(new Room()
            {
                Id = 0,
                Name = "Outside Brady's House",
                Description = "Brady has an extremely loved home.  There are even trees and flowers.  On your left, there is a truck up on some blocks.",
                Exits = new List<Exit>() { new Exit() { Direction = Direction.North, TargetRoomId = 1 } }
            });
            Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Inside Brady's House",
                Description = "The inside is even nicer than the outside.  $4000 of electronic equipment sit on a table.",
                Exits = new List<Exit>() {  new Exit() { Direction = Direction.South, TargetRoomId = 0 }, 
                                            new Exit() { Direction = Direction.East, TargetRoomId = 2 },
                                            new Exit() { Direction = Direction.West, TargetRoomId = 3 } }
            });
            Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Living Room",
                Description = "Nice cozy living room. On the North wall there is a mirror. And a flashlight on the table in the middle of the room.",
                Exits = new List<Exit>() { new Exit() { Direction = Direction.West, TargetRoomId = 1 } },
                Inventory = new Inventory("Living Room") {   
                    Items = new List<Item>(){
                            new Item() { Name = "Mirror", Description = "Regular mirror, where you can see yourself."},
                            new Item() { Name = "flashlight", 
                                         Description = "Regular basic flashlight.", 
                                         Triggers = new Dictionary<string,string>() {{"take","noFlashlight"}}
                                        } 
                    }
                },
                PotentialDescription = new Dictionary<string,string>() {
                    {"noFlashlight", "Nice cozy living room. On the North wall there is a mirror. And a the table in the middle of the room."}
                }
                //TODO: trigger character creation event!
            });
            Rooms.Add(new Room()
            {
                Id = 3,
                Name = "Master Bedroom",
                Description = "Nice luxurious bedroom. The perfect place for Brady to rest and retreive his voice.",
                Exits = new List<Exit>() { new Exit() { Direction = Direction.East, TargetRoomId = 1 } }
            });
        }

        public string CurrentRoomName => CurrentRoom.Name;
        public bool CurrentRoomHasSeenDescription => CurrentRoom.HasSeenDescription;

        public string CurrentRoomDescription
        {
            get
            {
                CurrentRoom.HasSeenDescription = true;
                return CurrentRoom.Description;
            }
        }

        public string CurrentRoomExits()
        {
            var exitList = new List<string>();
            foreach (var currentRoomExit in CurrentRoom.Exits)
            {
                exitList.Add(Exit.GetDirection(currentRoomExit.Direction));
            }

            return $"Exits: {string.Join(", ", exitList)}";
        }

        public void Go(Command command)
        {
            // north
            var indexOfExit = -1;
            switch (command)
            {
                case Command.COMMAND_NORTH:
                    indexOfExit = GetExitFromDirection(Direction.North);
                    break;
                case Command.COMMAND_SOUTH:
                    indexOfExit = GetExitFromDirection(Direction.South);
                    break;
                case Command.COMMAND_EAST:
                    indexOfExit = GetExitFromDirection(Direction.East);
                    break;
                case Command.COMMAND_WEST:
                    indexOfExit = GetExitFromDirection(Direction.West);
                    break;
            }

            if (indexOfExit == -1)
            {
                Console.WriteLine("You cannot go that way.");
                return;
            }

            CurrentRoomId = CurrentRoom.Exits[indexOfExit].TargetRoomId;
        }

        public void Do(Command command) {
            
            switch (command)
            {
                case Command.COMMAND_LOOK:
                    Console.WriteLine(CurrentRoomDescription);
                    break;
            }

        }

        private int GetExitFromDirection(Direction direction)
        {
            return CurrentRoom.Exits.FindIndex(p=> p.Direction == direction);
        }

        public Inventory CurrentRoomInventory(){
            return CurrentRoom.Inventory;
        }

        public void ProcessTrigger(string trigger){
            CurrentRoom.Description = CurrentRoom.PotentialDescription[trigger];
        }
    }
}