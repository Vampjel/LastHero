using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory
{
    private List<item> itemList;

    public inventory()
    {
        itemList = new List<item>();
        AddItem(new item { itemType = item.ItemType.sword, amount = 5 });
        Debug.Log(itemList.Count + " Items");
    }

    public void AddItem(item item)
    {
        itemList.Add(item);
    }

}
