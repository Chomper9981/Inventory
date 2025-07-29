using UnityEngine;
using InventorySystem.Models;
using InventorySystem.Managers;
using UnityEngine.UI;
using InventorySystem.Database;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

namespace InventorySystem.Views
{
    public class ShopView : BaseView
    {
        [SerializeField] private PlayerShopSlot _playerShopSlotPrefabs;
        [SerializeField] private Transform _playerShopSlotContainer;
        [SerializeField] private int _playerShopSize = 36;
        [SerializeField] private Image _CoinImage;
        [SerializeField] private TMP_Text _CoinText;
        [SerializeField] private ShopSlot _shopSlotPrefabs;
        [SerializeField] private Transform _shopSlotContainer;
        [SerializeField] private int _shopSize = 36;
        private PlayerShopSlot[] playerInventorySlots;
        private ShopSlot[] shopSlots;
        public void GenerateShopSlot()
        {
            playerInventorySlots = new PlayerShopSlot[_playerShopSize];
            for (int i = 0; i < _playerShopSize; i++)
            {
                playerInventorySlots[i] = Instantiate(_playerShopSlotPrefabs, _playerShopSlotContainer);
                playerInventorySlots[i].OnSellItem.AddListener(OnPlayerShoppingSlotClick);
            }
            shopSlots = new ShopSlot[_shopSize];
            for (int i = 0; i < _shopSize; i++)
            {
                shopSlots[i] = Instantiate(_shopSlotPrefabs, _shopSlotContainer);
                shopSlots[i].OnBuyItem.AddListener(OnShopSlotClick);
            }
        }
        public void UpdateShop()
        {
            Item[] playerItems = InventoryManagers.Instance.playerInventory.Items.Values.ToArray();
            
            foreach (var slot in playerInventorySlots)
            {
                slot.gameObject.SetActive(false);
            }
            for (int i = 0; i < playerItems.Length; i++)
            {
                if (playerItems[i].Id == 999)
                {
                    _CoinImage.sprite = SpriteManager.Instance.GetSprite(playerItems[i].SpriteName);
                    _CoinText.text = playerItems[i].Amount.ToString();
                }
                else
                {
                    playerInventorySlots[i].gameObject.SetActive(true);
                    playerInventorySlots[i].SetItem(playerItems[i]);
                }
            }
            Item[] shopItems = ShopManagers.Instance.shopInventory.Items.Values.ToArray();
            foreach (var slot in shopSlots)
            {
                slot.gameObject.SetActive(false);
            }
            for (int i = 0; i < shopItems.Length; i++)
            {
                shopSlots[i].gameObject.SetActive(true);
                shopSlots[i].SetItem(shopItems[i]);
            }
        }
        public void ClearShop()
        {
            if (playerInventorySlots != null)
            {
                foreach (var slot in playerInventorySlots)
                {
                    if (slot != null)
                    {
                        Destroy(slot.gameObject);
                    }
                }
            }

            if (shopSlots != null)
            {
                foreach (var slot in shopSlots)
                {
                    if (slot != null)
                    {
                        Destroy(slot.gameObject);
                    }
                }
            }
            playerInventorySlots = null;
            shopSlots = null;
        }
        public void OnPlayerShoppingSlotClick(Item item)
        {
            ShopManagers.Instance.PlayerSellItem(item.Id);
            UpdateShop();
        }
        public void OnShopSlotClick(Item item)
        {
            ShopManagers.Instance.PlayerBuyItem(item.Id);
            UpdateShop();
        }
        public void ResetShop()
        {
            ShopManagers.Instance.ResetShop();
            UpdateShop();
        }
        public void Start()
        {
            base.Open();
            
        }

        public override void Open()
        {
            base.Open();
            GenerateShopSlot();
            UpdateShop();
            ResetShop();
        }

        public override void Close()
        {
            ClearShop();
            base.Close();
            
        }

    }
}