using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvetoryManagement : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;     // ������ ��������
    bool isActiveNow = false;

    [SerializeField] private Text textTotalItemsCount;      // ����� ����� ������ �����
    private int maxItems = 15;                              // ����������� ������� ���������
    private int currentItemsCount = 0;

    public List<Item> items = new List<Item>();             // ���������� ��������

    public GameObject slotPrefab;                           // ������� ���� � ��������
    public Transform slotParent;                            // ����� � ��� ����� ����, ��� �� ��������� ��������� ���������

    private AudioSource zvuk;
    public AudioClip useItemClip;
    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(false);
        textTotalItemsCount.text = "Items: " + currentItemsCount.ToString();
        zvuk = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        InventoryPanelState();
    }
    private void InventoryPanelState()          // �������� / �������� ���������
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isActiveNow = !isActiveNow;
            UpdateUI();
        }
        ActivateInventoryPanel(isActiveNow);
    }
    private void ActivateInventoryPanel(bool state)
    {
        inventoryPanel.SetActive(state);
    }

    public Item AddItem(Item item)          // ������ ����� �������
    {
        if (items.Count < maxItems)         // ��������� �� �������
        {
            items.Add(item);
            currentItemsCount += 1;
            //
            zvuk.PlayOneShot(useItemClip);
            //
            textTotalItemsCount.text = "Items: " + currentItemsCount.ToString();        // ��������� ������ ��� ������
            Debug.Log("Item added: " + item.itemName);
            UpdateUI();
            return item;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return null;  // ������ �� ������
        }
    }
    public void RemoveItem(Item itemToRemove)       // ��������� 1 �������
    {
        bool itemFound = false;                     // ��� ���������, �� �������� �������

        // ��������� ��������
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemToRemove.itemName)
            {
                items.RemoveAt(i);
                currentItemsCount -= 1;
                //
                zvuk.PlayOneShot(useItemClip);
                //
                textTotalItemsCount.text = "Items: " + currentItemsCount.ToString();        // ��������� ������ ��� ������
                itemFound = true;                   // �������� � ��������
                UpdateUI();
                Debug.Log("Item removed: " + itemToRemove.itemName);
                break;                              // ������ � ����� ���� ��������� ��������
            }
        }

        if (!itemFound)
        {
            Debug.Log("Item not found in inventory.");
        }
    }
    public void UpdateUI()
    {
        // ��������� ��� ������ �����
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // ��� ����� ��� ������� �������� � ��������
        foreach (Item item in items)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            newSlot.GetComponent<ButtonSlot>().SetSlot(item);                     ///////EXTRA ATTENTION!!!!!
            newSlot.name = item.itemName;
        }
    }
    public bool HasItem(string itemName)      // �������� �������� �������� �� ���� ������
    {
        foreach (Item item in items)
        {
            if (item.itemName == itemName)
            {
                return true; // ������� ���������
            }
        }
        return false; // ������� �� ���������
    }
}
