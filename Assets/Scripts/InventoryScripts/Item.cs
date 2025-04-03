using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour
{
    public Sprite itemImage;
    public string itemName;
    public int price;

    public Item(Sprite itemImage, string itemName, int price)
    {
        this.itemImage = itemImage;
        this.itemName = itemName;
        this.price = price;
    }
}
