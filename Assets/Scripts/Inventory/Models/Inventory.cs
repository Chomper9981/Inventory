using System.Collections.Generic;

namespace InventorySystem.Models
{
    public class Inventory
    {
        public Dictionary<int, Item> Items { get; set; }

        public Inventory()
        {
            Items = new Dictionary<int, Item>();
        }
        
    }
}