using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSlot : MonoBehaviour
{
    public Image itemIconImage;
    public Text itemName;

    private InvetoryManagement InventoryManagementScript;
    private PlayerMovement PlayerMovementScript;

    public void SetSlot(Item data)
    {
        itemIconImage.sprite = data.itemImage;
        itemName.text = data.itemName;
        //gameObject.name = data.itemName;
    }

    public void UseButtonItem()
    {
        UseItem(gameObject.name);
    }
    private void UseItem(string itemName)
    {
        InventoryManagementScript = GameObject.Find("Inventory").GetComponent<InvetoryManagement>();
        PlayerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();

        if (InventoryManagementScript.HasItem(itemName))        // �� � � �������� ����� �������
        {
            Sprite itemIcon = Resources.Load<Sprite>(itemName);
            Item itemToUse = new Item(itemIcon, itemName, 0);

            // ������������ �������� � ��������� ���� � ���������
            InventoryManagementScript.RemoveItem(itemToUse);

            // 䳿 ������� �� ���� ��������
            switch (itemName)
            {
                case "SUSHI":
                    // ������ ������
                    Debug.Log("SUSHI EATEN");
                    PlayerStats.Instance.EatFoodItem(1);    // restore 1 slide
                    break;
                case "ONIGIRI":
                    // ������'�
                    Debug.Log("ONIGIRI EATEN");
                    PlayerStats.Instance.EatFoodItem(2);    // restore 2 slide
                    break;
                case "COIN":
                    // ������ ������
                    GameManagerScript.instance.AddMoney(50);
                    break;
                case "WIRESBOX":
                    // ������'�
                    GameManagerScript.instance.AddMoney(25);
                    break;
                case "CPU":
                    // ������ ������
                    GameManagerScript.instance.AddMoney(100);
                    break;
                case "MOTHERBOARD":
                    // ������'�
                    GameManagerScript.instance.AddMoney(30);
                    break;
                case "DETAIL":
                    // ������'�
                    GameManagerScript.instance.AddMoney(10);
                    break;
                case "VIDEOCARD":
                    // ������'�
                    GameManagerScript.instance.AddMoney(150);
                    break;
                case "JUMP":
                    // ������'�
                    PlayerMovementScript.jumpForce += 1f;
                    break;
                case "SPEED":
                    // ������'�
                    PlayerMovementScript.moveSpeed += 2f;
                    break;

                // ����� �� ������ ����� ��������
                default:
                    Debug.Log("�������� �������: " + itemName);
                    //soundManagerScript.PlayItemUseSound();
                    break;
            }
        }
    }
}
