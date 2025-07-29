using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventorySystem.Models;
using InventorySystem.Managers;
using UnityEngine.Events;


namespace InventorySystem.Views
{
    public class PlayerInventorySlot : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;

        [SerializeField] private TMP_Text _amount;

        [SerializeField] private Button _button;

        public Item itemSlot { get; set; }
        public UnityEvent<Item> OnUseItem;
        private void Start()
        {
            _button.onClick.AddListener(OnPlayerInventorySlotClick);
        }

        public void OnPlayerInventorySlotClick()
        {
            OnUseItem?.Invoke(itemSlot);
        }

        public void SetItem(Item item)
        {
            itemSlot = item;
            
            _itemImage.sprite = SpriteManager.Instance.GetSprite(item.SpriteName);

            _amount.text = item.Amount.ToString();
        }
    }
}