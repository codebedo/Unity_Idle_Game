using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    public Slider ProgressSlider;
    public TMP_Text BuyButtonText;
    public Button BuyButton;
    public TMP_Text StoreCountText;

    public Store store;

    public Button ManagerButton;


   

    void OnEnable()
    {
        GameManager.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;

    }
    void OnDisable()
    {
        GameManager.OnUpdateBalance -= UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;


    }

    void Awake()
    {
        store = transform.GetComponent<Store>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StoreCountText.text = store.StoreCount.ToString();
        //BuyButtonText.text = "Buy $ " + store.GetNextStoreCost().ToString("F2");

    }

    // Update is called once per frame
    void Update()
    {
        ProgressSlider.value = store.GetCurrentTimer() / store.GetStoreTimer();
        

    }
    public void UpdateUI()

    {
        CanvasGroup cg = this.GetComponent<CanvasGroup>();
        if(!store.StoreUnlocked && !GameManager.instance.CanBuy(store.GetNextStoreCost()))
        {
            cg.interactable = false;
            cg.alpha = 0;

        }
        else
        {
            cg.interactable = true;
            store.StoreUnlocked = true;
            cg.alpha = 1;
        }

        // Update button if you can afford the store 
        if (GameManager.instance.CanBuy(store.GetNextStoreCost()))
            BuyButton.interactable = true;
        else
            BuyButton.interactable = false;

        // Update Manager if store manager can buy 
        if (GameManager.instance.CanBuy(store.ManagerCost))
            ManagerButton.interactable = true;
        else
            ManagerButton.interactable = false;
        //TMP_Text Buttontext = ManagerButton.transform.Find("UnlockManagerButtonText").GetComponent<TMP_Text>();



    }

    public void BuyStoreOnClick()
    {
        if (!GameManager.instance.CanBuy(store.GetNextStoreCost()))
            return;
        store.BuyStore();
        StoreCountText.text = store.StoreCount.ToString();
        UpdateUI();
        

    }


    public void OnTimerClick()
    {
        store.OnStartTimer();
    }
}
