using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance;

    [SerializeField] private GameManagerScript GMS;
    public int playerMoney = 0;
    public Text moneyText;
    public int countOfBusinesses = 0;

    public StoreSlot[] storeSlots;         // магазини

    private void Awake()
    {
        //countOfBusinesses = PlayerPrefs.GetInt("countOfBusinesses"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        GMS = GameObject.Find("GameManagerObject").GetComponent<GameManagerScript>();
        playerMoney = GMS.money;
        //countOfBusinesses = PlayerPrefs.GetInt("countOfBusinesses");
        //GMS.StartIncome(countOfBusinesses);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UnlockStore(int storeID)
    {
        playerMoney = GMS.money;

        int price = storeSlots[storeID].price;

        if (playerMoney >= price && !IsStoreUnlocked(storeID))
        {
            //playerMoney -= price;
            GMS.RemoveMoney(price);
            GMS.HaveBusiness = true;
            countOfBusinesses += 1;
            //PlayerPrefs.SetInt("countOfBusinesses", countOfBusinesses);
            GMS.StartIncome(countOfBusinesses);
            PlayerPrefs.SetInt("Store_" + storeID, 1);
            UpdateUI();
        }
    }
    bool IsStoreUnlocked(int storeID)
    {
        return PlayerPrefs.GetInt("Store_" + storeID, 0) == 1;
    }
    void UpdateUI()
    {
        moneyText.text = "Money: $" + playerMoney;

        foreach (StoreSlot slot in storeSlots)
        {
            bool unlocked = IsStoreUnlocked(slot.storeID);
            slot.unlockButton.gameObject.SetActive(!unlocked);
            slot.lockedImage.gameObject.SetActive(!unlocked);
        }
    }

    public void ResetButton()
    {
        //playerMoney = 1000;
        moneyText.text = "Money: $" + playerMoney;

        PlayerPrefs.DeleteAll(); // Видаляє всі збережені дані
        // PlayerPrefs.SetInt("Money", 1000); // Початкові гроші
        PlayerPrefs.SetInt("Store_0", 0);
        PlayerPrefs.SetInt("Store_1", 0);  // Початковий стан магазинів (0 - закритий)
        PlayerPrefs.SetInt("Store_2", 0);
        PlayerPrefs.SetInt("Store_3", 0);
        //PlayerPrefs.SetInt("countOfBusinesses", 0);
        PlayerPrefs.Save(); // Зберігаємо зміни

        UpdateUI();
    }
}

