using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    


    public enum State
    {
        Main, Managers
    }
    public Text CurrentBallanceText;
    public Text CompanyNameText;
    public State CurrentState;
    public GameObject ManagerPanel;
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = State.Main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        GameManager.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;
        
    }
    void onShowManagers()
    {
        CurrentState = State.Managers;
        ManagerPanel.SetActive(true);
    }
    void onShowMain()
    {
        CurrentState = State.Main;
        ManagerPanel.SetActive(false);
    }
    public void onClickManager()
    {
        if (CurrentState == State.Main)
            onShowManagers();
        else
            onShowMain();
    }
    void OnDisable()
    {
        GameManager.OnUpdateBalance -= UpdateUI;
        LoadGameData.OnLoadDataComplete -= UpdateUI;

    }



    public void UpdateUI()
    {
        CurrentBallanceText.text = "$ " + GameManager.instance.GetCurrentBalance().ToString("F2");
        CompanyNameText.text = GameManager.instance.CompanyName;
    }
}
