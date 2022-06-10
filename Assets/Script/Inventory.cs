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

    }

    public void addItem(Item item)
        { itemList.Add(item); }
    public void removeItem(Item item)
    {
        itemList.Remove(item);
    }

    public bool hasItem(Item item)
    {
        return itemList.Contains(item);
    }

    public List<Item> getItemList()
    { return itemList; }
}
