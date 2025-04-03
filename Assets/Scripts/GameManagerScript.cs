using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public int money = 200;
    [SerializeField] private Text moneyText;
    bool isActiveNow = false;
    //[SerializeField] private GameObject ShopPanel;
    public Item[] items;
    public bool HaveBusiness;

    private InvetoryManagement InventoryManagementScript;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InventoryManagementScript = GameObject.Find("Inventory").GetComponent<InvetoryManagement>();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void AddMoney(int value)
    {
        money += value;
    }

    public void RemoveMoney(int value)
    {
        money -= value;
    }

    private void UpdateUI()
    {
        moneyText.text = "Money: ¥" + money.ToString();
    }

    public void BuyItem(int itemIndex)
    {
        Item item = items[itemIndex];
        if (money >= items[itemIndex].price)
        {
            money -= item.price;
            Debug.Log("Buy item: " + item.itemName);
            //
            string ItemName = item.itemName.ToUpper();
            Sprite ItemIcon = Resources.Load<Sprite>(ItemName);

            if (ItemIcon != null)
            {
                Item NewItem = new Item(ItemIcon, ItemName, item.price);
                InventoryManagementScript.AddItem(NewItem);
                //����
                //Destroy(coll.gameObject);
            }
            else
            {
                Debug.Log("������ ��� " + ItemName + "  �� ��������");
            }
            //
            UpdateUI();
        }
        else
        {
            Debug.Log("Heh, no!");
        }
    }

    IEnumerator Income(int param)
    { 
        while (HaveBusiness)
        {
            yield return new WaitForSeconds(1f);
            money += param;
        }
    }

    public void StartIncome(int countOfBusinesses)
    {
       switch (countOfBusinesses)
        {
            case 1:
                StartCoroutine(Income(1));
                break;
            case 2:
                StartCoroutine(Income(2));
                break;
        }
    }

    /* private void ShopPanelState()          // �������� / �������� ���������
     {

         isActiveNow = !isActiveNow;

         ActivateShopPanel(isActiveNow);
     }*/
    /*public void ActivateShopPanel(bool state)
    {
        isActiveNow = !isActiveNow;
        ShopPanel.SetActive(state);
    }/*

    /*public void DeactivateShopPanel(bool state)
    {
        ShopPanel.SetActive(false);
    }*/

}
