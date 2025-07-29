namespace InventorySystem.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SpriteName { get; set; }
        public int PricePurchase { get; set; }
        public int PriceSell { get; set; }
        public int Amount { get; set; }

        
    }
}