using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public enum Item
    {
        Money,
        PoliceContact
    }

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        itemList
            .Add(Item.Money);
        itemList.Add(Item.PoliceContact);

    }

    public void addItem(Item item)
        { itemList.Add(item); }

    public List<Item> getItemList()
    { return itemList; }
}
