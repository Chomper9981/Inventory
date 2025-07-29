using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventorySystem.Models;
using InventorySystem.Managers;
using UnityEngine.Events;


namespace InventorySystem.Views
{
    public class PlayerShopSlot : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;

        [SerializeField] private TMP_Text _amount;

        [SerializeField] private Button _button;

        public Item itemSlot { get; set; }
        public UnityEvent<Item> OnSellItem;
        private void Start()
        {
            _button.onClick.AddListener(OnPlayerShoppingSlotClick);
        }

        public void OnPlayerShoppingSlotClick()
        {
            OnSellItem?.Invoke(itemSlot);
        }

        public void SetItem(Item item)
        {
            itemSlot = item;
            
            Sprite[] sprites = Resources.LoadAll<Sprite>("");
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == item.SpriteName)
                {
                    _itemImage.sprite = sprite;
                    break;
                }
            }
            _amount.text = item.Amount.ToString();
        }
    }
}