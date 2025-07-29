using System.Collections.Generic;
using System.Linq;
using InventorySystem.Database;
using InventorySystem.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InventorySystem.Managers
{

    public class InventoryManagers : MonoBehaviour
    {
        public Inventory playerInventory;  
        // public UnityEvent OnInventoryChange;
        public static InventoryManagers Instance;
        private void Awake()
        {
            if(Instance != null) Destroy(Instance);
            Instance = this;
        }
        public void PlayerInitialize()
        {
            playerInventory = DataManager.Instance.Load("inventory");
        }
        public bool UseItem(int itemId)
        {
            Item item = playerInventory.Items[itemId];
            if(itemId == 999)
            {
                return false;
            }
            if(item.Amount > 1)
            {
                item.Amount--;
            }
            else
            {
                playerInventory.Items.Remove(item.Id);
            }
            DataManager.Instance.Save("inventory", playerInventory);
            // OnInventoryChange?.Invoke();
            return true;
        }
        public bool ResetInventory()
        {
            Inventory inventory = new Inventory();

            string csv = Resources.Load<TextAsset>("Items_data").text;
            string[] lines = csv.Split('\n');
            string[] fields = lines[lines.Length - 1].Split(',');
            Item item = new Item();
            item.Id = int.Parse(fields[0]);
            item.Name = fields[1];
            item.SpriteName = fields[2];
            item.PricePurchase = int.Parse(fields[3]);
            item.PriceSell = int.Parse(fields[4]);
            item.Amount = 50000;
            inventory.Items.Add(item.Id, item);
            DataManager.Instance.Save("inventory", inventory);
            PlayerInitialize();
            return true;
        }
    }
}