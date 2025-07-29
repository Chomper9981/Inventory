using UnityEngine;
using InventorySystem.Models;
using InventorySystem.Managers;
using UnityEngine.UI;
using InventorySystem.Database;
using TMPro;
using System.Linq;

namespace InventorySystem.Views
{
    public class InventoryView : BaseView
    {
        [SerializeField] private PlayerInventorySlot _playerInventorySlotPrefabs;
        [SerializeField] private Transform _playerInventorySlotContainer;
        [SerializeField] private int _inventorySize = 36;
        [SerializeField] private Image _CoinImage;
        [SerializeField] private TMP_Text _CoinText;
        private PlayerInventorySlot[] playerInventorySlots;
        public void GenerateInventorySlot()
        {
            playerInventorySlots = new PlayerInventorySlot[_inventorySize];
            for(int i = 0; i < _inventorySize; i++)
            {
                playerInventorySlots[i] = Instantiate(_playerInventorySlotPrefabs, _playerInventorySlotContainer);
                playerInventorySlots[i].OnUseItem.AddListener(OnPlayerInventorySlotClick);
            }
        }
        public void UpdateInventory()
        {
            Item[] items = InventoryManagers.Instance.playerInventory.Items.Values.ToArray();
            foreach(var slot in playerInventorySlots)
            {
                slot.gameObject.SetActive(false);
            }

            for (int i = 0; i < items.Length; i++)
            {
                if(items[i].Id == 999)
                {
                    _CoinImage.sprite = SpriteManager.Instance.GetSprite(items[i].SpriteName);
                    _CoinText.text = items[i].Amount.ToString();
                }
                else
                {
                    playerInventorySlots[i].gameObject.SetActive(true);
                    playerInventorySlots[i].SetItem(items[i]);       
                }
            }
        }
        public void ClearInventory()
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
        }
        public void OnPlayerInventorySlotClick(Item item)
        {
            InventoryManagers.Instance.UseItem(item.Id);
            UpdateInventory();
        }
        public void ResetInventory()
        {
            InventoryManagers.Instance.ResetInventory();
            UpdateInventory();
        }

        public void Start()
        {
            // ResetInventory();
        }
        public override void Open()
        {
            base.Open();
            GenerateInventorySlot();
            UpdateInventory();
        }

        public override void Close()
        {
            ClearInventory();
            base.Close();
        }
    }
}