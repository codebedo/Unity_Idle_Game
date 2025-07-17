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


   

    void OnEnable()
    {
        GameManager.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;

    }
    void OnDisable()
    {
        GameManager.OnUpdateBalance -= UpdateUI;
        LoadGameData.OnLoadDataComplete -= UpdateUI;


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
            cg.alpha = 1;
            store.StoreUnlocked = true;
        }

        // Update button if you can afford the store 
        if (GameManager.instance.CanBuy(store.GetNextStoreCost()))
            BuyButton.interactable = true;
        else
            BuyButton.interactable = false;
        BuyButtonText.text = "Buy $ " + store.GetNextStoreCost().ToString("F2");


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
