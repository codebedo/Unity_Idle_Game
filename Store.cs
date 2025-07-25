using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System.Globalization;
using Unity.VisualScripting;

public class Store : MonoBehaviour
{
    
    public string StoreName;
    public float BaseStoreCost;
    public float BaseStoreProfit;
    public int StoreCount;
    public bool ManagerUnlocked;
    public bool UpgradeUnlocked;
    public float StoreMultiplier;
    public float StoreTimer;
    float CurrentTimer = 0;
    public float ManagerCost;
    public float UpgradeCost;
    public float UpgradeMultiplier;
    public float CurrentMultiplier;
    public bool StartTimer;    
    public bool StoreUnlocked;
    public int StoreTimerDivision ;        
    float NextStoreCost;




    void Start()
    {
        
        StartTimer = false;
        CurrentMultiplier = 1f;
    }

    public void SetNextStoreCost(float amt)
    {
        NextStoreCost = amt;
    }

    public float GetNextStoreCost()
    {
        return NextStoreCost;
    }
    public float GetStoreTimer()
    {
        return StoreTimer;
    }


    public float GetCurrentTimer()
    {
        return CurrentTimer;
    }

    


    void Update()
    {
        

        if (StartTimer)
        {
            CurrentTimer += Time.deltaTime;
            if (CurrentTimer > StoreTimer)
            {
                if(!ManagerUnlocked)
                    StartTimer = false;
                CurrentTimer = 0f;
                GameManager.instance.AddToBalance(BaseStoreProfit * StoreCount * CurrentMultiplier);
            }
        }
    }

    



    public void BuyStore()
    {
        StoreCount = StoreCount + 1;

        float Amt =  -NextStoreCost;
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount));
        GameManager.instance.AddToBalance(Amt);


        if (StoreCount % StoreTimerDivision == 0)
            StoreTimer = StoreTimer / 2;
    }


    public void OnStartTimer()
    {
        if(!StartTimer && StoreCount > 0)
          StartTimer = true;
    }

    public  void UnlockManager()
    {   if (ManagerUnlocked)
            return;
        if (GameManager.instance.CanBuy(ManagerCost))
        {
            GameManager.instance.AddToBalance(-ManagerCost);
            ManagerUnlocked = true;
            this.transform.GetComponent<UIStore>().ManagerUnlocked();


        }
    }
     public  void UnlockUpgrade()
    {   if (UpgradeUnlocked)
            return;
        if (GameManager.instance.CanBuy(UpgradeCost))
        {
            GameManager.instance.AddToBalance(-UpgradeCost);
            UpgradeUnlocked = true;
            CurrentMultiplier = CurrentMultiplier + UpgradeMultiplier;
            this.transform.GetComponent<UIStore>().UpgradeUnlocked();


        }
    }

    
}
