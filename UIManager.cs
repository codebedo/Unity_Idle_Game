using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text CurrentBallanceText;
    public Text CompanyNameText;


    // Start is called before the first frame update
    void Start()
    {
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
