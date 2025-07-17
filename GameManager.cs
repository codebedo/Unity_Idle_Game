using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System.Globalization;

public class GameManager : MonoBehaviour {

    public delegate void UpdateBalance();
    public static event UpdateBalance OnUpdateBalance;

    public string CompanyName;
    public static GameManager instance;

    //public Text CurrentBallanceText;
    float CurrentBalance;

    // Start is called before the first frame update
    void Start()
    {
        CurrentBalance = 6.0f;
        if (OnUpdateBalance != null)
            OnUpdateBalance();


    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    public void AddToBalance(float amt)
    {
        CurrentBalance += amt;
        if (OnUpdateBalance != null)
            OnUpdateBalance();

    }

    public bool CanBuy(float AmtToSpend)
    {
        if (AmtToSpend > CurrentBalance)

            return false;

        else

            return true;
    }


    public float GetCurrentBalance()
    {
        return CurrentBalance;
    }
}
