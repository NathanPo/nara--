using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemMoney;
    private Transform itemPoliceCard;

    private void Awake()
    {
        itemMoney = transform.Find("itemMoney");
        itemPoliceCard = transform.Find("itemPoliceCard");
    }

    public void setInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        bool money = inventory.getItemList().Contains(Inventory.Item.Money);
        bool policeCard = inventory.getItemList().Contains(Inventory.Item.PoliceContact);

        itemMoney.gameObject.SetActive(money); 
        itemPoliceCard.gameObject.SetActive(policeCard);

            
    }
}
