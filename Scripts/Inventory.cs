using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public string[] names = new string[15];
    public string[] sprites = new string[15];

    public void AddItem(Item.Type type)
    {
        switch (type)
        {
            case Item.Type.Potion:
                Potion.quantity++;
                Debug.Log("Potion Added");
                Debug.Log(Potion.quantity);
                break;

            case Item.Type.SpiritPotion:
                Potion.spiritQuantity++;
                break;
        }
    }
}
