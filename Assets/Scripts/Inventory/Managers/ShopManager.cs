using System.Collections.Generic;
using System.Linq;
using InventorySystem.Database;
using InventorySystem.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InventorySystem.Managers
{
    public class ShopManagers : MonoBehaviour
    {
        public static ShopManagers Instance;
        public Inventory shopInventory;
        // public UnityEvent OnShopChange;
        private Dictionary<string, List<Item>> _categorizedItems = new Dictionary<string, List<Item>>();
        private List<Item> _foods = new List<Item>();
        private List<Item> _potions = new List<Item>();    
        private List<Item> _weapons = new List<Item>();
        private List<Item> _equipments = new List<Item>();
        private List<Item> _ores = new List<Item>();
        private List<Item> _questItems = new List<Item>();
        private List<Item> _ammo = new List<Item>();
        private void Awake()
        {
            if(Instance != null) Destroy(Instance);
            Instance = this;
        }
        public void ShopInitialize()
        {
            shopInventory = DataManager.Instance.Load("shop");
        }
        public bool PlayerSellItem(int itemId)
        {
            Inventory playerTemp = InventoryManagers.Instance.playerInventory;
            Item playerItem = playerTemp.Items[itemId];
            Item item = new Item()
            {
                Id = itemId,
                Name = playerItem.Name,
                SpriteName = playerItem.SpriteName,
                PricePurchase = playerItem.PricePurchase,
                PriceSell = playerItem.PriceSell,
                Amount = 1
            };
            Item coin = playerTemp.Items[999];
            int priceSell = playerItem.PriceSell;
            if (itemId == 999) return false;
            if (playerItem.Amount > 1)
            {
                playerItem.Amount--;
            }
            else
            {
                playerTemp.Items.Remove(itemId);
            }
            coin.Amount += priceSell;
            if (!shopInventory.Items.ContainsKey(itemId))
            {
                shopInventory.Items.Add(itemId, item);
            }
            else
            {
                shopInventory.Items[itemId].Amount++;
                
            }
            DataManager.Instance.Save("inventory", playerTemp);
            DataManager.Instance.Save("shop", shopInventory);
            return true;
        }

        public bool PlayerBuyItem(int itemId)
        {
            Inventory playerTemp = InventoryManagers.Instance.playerInventory;
            Item shopItem = shopInventory.Items[itemId];
            Item coin = playerTemp.Items[999];
            int pricePurchase = shopItem.PricePurchase;
            Item item = new Item()
            {
                Id = itemId,
                Name = shopItem.Name,
                SpriteName = shopItem.SpriteName,
                PricePurchase = shopItem.PricePurchase,
                PriceSell = shopItem.PriceSell,
                Amount = 1
            };
            if (itemId == 999)
            {
                return false;
            }
            if (coin.Amount >= shopItem.PricePurchase)
            {
                if (shopItem.Amount > 1)
                {
                    shopItem.Amount--;
                }
                else
                {
                    shopInventory.Items.Remove(itemId);
                }
                if (!playerTemp.Items.ContainsKey(itemId))
                {
                    playerTemp.Items.Add(itemId, item);
                }
                else
                {
                    playerTemp.Items[itemId].Amount++;
                    
                }
                coin.Amount -= pricePurchase;
            }
            else
            {
                Debug.Log("You are broke, bro!");
                return false;
            }
            DataManager.Instance.Save("inventory", playerTemp);
            DataManager.Instance.Save("shop", shopInventory);
            return true;
        }
        public bool ClassifyItems()
        {
            string csv = Resources.Load<TextAsset>("Items_data").text;
            string[] lines = csv.Split('\n');
            
            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');
                
                Item item = new Item()
                {
                    Id = int.Parse(fields[0]),
                    Name = fields[1],
                    SpriteName = fields[2],
                    PricePurchase = int.Parse(fields[3]),
                    PriceSell = int.Parse(fields[4]),
                    Amount = 10
                };

                if(int.Parse(fields[0])>=100 && int.Parse(fields[0])<200)
                {
                    _foods.Add(item);
                }
                else if(int.Parse(fields[0])>=200 && int.Parse(fields[0])<300)
                {
                    _potions.Add(item);
                }
                else if(int.Parse(fields[0])>=300 && int.Parse(fields[0])<400)
                {
                    _ores.Add(item);
                }
                else if(int.Parse(fields[0])>=400 && int.Parse(fields[0])<500)
                {
                    item.Amount = 1;
                    _weapons.Add(item);
                }
                else if(int.Parse(fields[0])>=500 && int.Parse(fields[0])<600)
                {
                    item.Amount = 30;
                    _ammo.Add(item);
                }
                else if(int.Parse(fields[0])>=700 && int.Parse(fields[0])<800)
                {
                    item.Amount = 1;
                    _equipments.Add(item);
                }
                else if(int.Parse(fields[0])>=800 && int.Parse(fields[0])<900)
                {
                    item.Amount = 1;
                    _questItems.Add(item);
                }
                else
                {
                    continue;
                }
            }
            // Debug.Log("ClassifyItems: " + _ammo[0].Id);
            _categorizedItems.Add("Ammo", _ammo);
            _categorizedItems.Add("Equipments",_equipments);
            _categorizedItems.Add("Foods",_foods);
            _categorizedItems.Add("Ores",_ores);
            _categorizedItems.Add("Potions",_potions);
            _categorizedItems.Add("Weapons",_weapons);
            _categorizedItems.Add("QuestItems",_questItems);
            return true;
        }
        private void GetRandomItemFormCategory(string category, Inventory inventory)
        {
            Item item = new Item();
            List<Item> list = _categorizedItems[category];
            item = list[Random.Range(0, list.Count)];
            inventory.Items.Add(item.Id, item);
        }

        public bool ResetShop()
        {
            Inventory inventory = new Inventory();
            ICollection<string> Keys = _categorizedItems.Keys;
            
            foreach(string key in Keys)
            {
                GetRandomItemFormCategory(key,inventory);
            }
            DataManager.Instance.Save("shop", inventory);
            ShopInitialize();
            return true;
        }
    }
}