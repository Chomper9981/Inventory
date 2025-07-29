using UnityEngine;
using UnityEngine.UI;
using InventorySystem.Database;
using TMPro;
using InventorySystem.Models;
using InventorySystem.Managers;



public class GameController : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    [SerializeField] private Button _playerInventoryButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _backFromShopButton;
    [SerializeField] private Button _backFromPlayerInventoryButton;

    private InventoryManagers _inventoryManager;
    private SpriteManager _spriteManager;
    public void OnClickPlayerInventoryButton()
    {
        _ui.CloseAllPanels();
        _ui.OpenPlayerInventory();
    }

    public void OnClickShopButton()
    {
        _ui.CloseAllPanels();
        _ui.OpenShop();
    }

    public void OnClickBackFromShopButton()
    {
        _ui.CloseAllPanels();
        _ui.BackFormShop();
        _ui.OpenMenu();
    }

    public void OnClickBackFromPlayerInventoryButton()
    {
        _ui.CloseAllPanels();
        _ui.BackFormInventory();
        _ui.OpenMenu();
    }


    void Start()
    {
        _ui.CloseAllPanels();
        _ui.OpenMenu();
        SpriteManager.Instance.LoadSpriteSheet();
        InventoryManagers.Instance.PlayerInitialize();
        ShopManagers.Instance.ShopInitialize();
        ShopManagers.Instance.ClassifyItems();
    }

    
    void Update()
    {
        
    }
}
