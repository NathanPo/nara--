using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemMoney;

    private void Awake()
    {
        itemMoney = transform.Find("itemMoney");
    }

    public void setInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems() {
        bool money = inventory.getItemList().Contains(Inventory.Item.Money);
        itemMoney.gameObject.SetActive(money);
    }
}
