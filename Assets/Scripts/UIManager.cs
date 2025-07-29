using InventorySystem.Models;
using InventorySystem.Views;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerInventoryPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private InventoryView _playerInventoryView;
    [SerializeField] private ShopView _shopInventoryView;

    public void OpenPlayerInventory()
    {
        _playerInventoryPanel.SetActive(true);
        _playerInventoryView.Open();
    }

    public void OpenShop()
    {
        _shopPanel.SetActive(true);
        _shopInventoryView.Open();
    }

    public void OpenMenu()
    {
        _menuPanel.SetActive(true);
    }

    public void CloseAllPanels()
    {
        _playerInventoryPanel.SetActive(false);
        _shopPanel.SetActive(false);    
        _menuPanel.SetActive(false);
        
    }

    // public void GenerateInventorySlot()
    // {
    //     _playerInventoryView.GenerateInventorySlot();
    // }

    public void BackFormInventory()
    {
        _playerInventoryView.Close();
    }

    public void BackFormShop()
    {
        _shopInventoryView.Close();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
