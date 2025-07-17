using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System.Globalization;
public class Store : MonoBehaviour
{
    public float BaseStoreCost;
    public float BaseStoreProfit;
    public int StoreCount;
    public bool ManagerUnlocked;
    public float StoreMultiplier;
    
    public float StoreTimer;
    float CurrentTimer = 0;
    public float ManagerCost;
    public bool StartTimer;

    
    public bool StoreUnlocked;
    public int StoreTimerDivision ;
    
    
    float NextStoreCost;




    void Start()
    {
        
        StartTimer = false;
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
                GameManager.instance.AddToBalance(BaseStoreProfit * StoreCount);
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
}
