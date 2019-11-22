﻿using System.Collections.Generic;

namespace ProjectDover
{
    public class Room
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string,string> PotentialDescription { get; set; }
        public List<Exit> Exits { get; set; }

        public Inventory Inventory { get; set; }

        public bool HasSeenDescription { get; set; }
    }
}