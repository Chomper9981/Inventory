using InventorySystem.Models;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace InventorySystem.Database
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;
        
        
        private void Awake()
        {
            if(Instance != null) Destroy(Instance);
            Instance = this;
        }
        

        [System.Serializable] 
        private class SerializableItem
        {
            public int Id;
            public string Name;
            public string SpriteName;
            public int PricePurchase;
            public int PriceSell;
            public int Amount;

            public SerializableItem (int Itemid, Item item)
            {
                
                Id = Itemid;
                Name = item.Name;
                SpriteName = item.SpriteName;
                PricePurchase = item.PricePurchase;
                PriceSell = item.PriceSell;
                Amount = item.Amount;
            }


            public Item ToItem(SerializableItem item)
            {
                return new Item()
                {
                    Id = item.Id,
                    Name = item.Name,
                    SpriteName = item.SpriteName,
                    PricePurchase = item.PricePurchase,
                    PriceSell = item.PriceSell,
                    Amount = item.Amount,
                };
            }
        }

        [System.Serializable] 
        private class SerializableInventory
        {
            public List<SerializableItem> Items = new List<SerializableItem>();

            public SerializableInventory(Inventory inventory)
            {   
                foreach (var item in inventory.Items)
                {
                    Items.Add(new SerializableItem(item.Key, item.Value));
                }
            }

            public Inventory ToInventory()
            {
                Inventory inventory = new Inventory();
                    foreach (SerializableItem item in Items)
                {
                    inventory.Items.Add(item.Id, item.ToItem(item));
                }
                return inventory;
            }
        }

        

        public void Save(string name, Inventory inventory)
        {
            string json = JsonUtility.ToJson(new SerializableInventory(inventory));
            PlayerPrefs.SetString(name, json);
            // Debug.Log("DataManager Save " + name + ": " + json);
        }

        public Inventory Load(string name)
        {
            string json = PlayerPrefs.GetString(name);
            Inventory inventory = JsonUtility.FromJson<SerializableInventory>(json).ToInventory();
            // Debug.Log("DataManager Load " + name + ": " + inventory.Items.Count);
            return inventory;
        }
    }
}