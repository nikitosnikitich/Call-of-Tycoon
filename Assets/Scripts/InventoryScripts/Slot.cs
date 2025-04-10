using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image itemIconImage;
    public Text itemName;

    public void SetSlot(Item data)
    {
        itemIconImage.sprite = data.itemImage;
        itemName.text = data.itemName;
    }
}
