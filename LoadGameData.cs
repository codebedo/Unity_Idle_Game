using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;

public class LoadGameData : MonoBehaviour
{

    public delegate void LoadDataComplete();
    public static event LoadDataComplete OnLoadDataComplete;


    public TextAsset GameData;
    public GameObject StorePrefab;
    public GameObject StorePanel;
    public GameObject ManagerPanel;
    public GameObject ManagerPrefab;
    public GameObject UpgradePanel;
    public GameObject UpgradePrefab;

    public void Start()
    {
        LoadData();
        if (OnLoadDataComplete != null)
            OnLoadDataComplete();
    }

    public void LoadData()
    {
        // Create XML Document to hold game data
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(GameData.text);

        // Load Game Manager Data
        LoadGameManagerData(xmlDocument);
        // Load the stores
        LoadStores(xmlDocument);
    }
    public void LoadGameManagerData(XmlDocument xmlDocument)
    {
        // Load Game manager Info "Starting Balance " 
        //string StartingBalance = xmlDocument.GetElementsByTagName("StartingBalanve")
        GameManager.instance.AddToBalance(float.Parse(xmlDocument.GetElementsByTagName("StartingBalance")[0].InnerText));
        string CompanyName = xmlDocument.GetElementsByTagName("CompanyName")[0].InnerText;

        GameManager.instance.CompanyName = CompanyName;
    }


 
    void LoadStores(XmlDocument xmlDocument)
    {
        XmlNodeList StoreList = xmlDocument.GetElementsByTagName("store");

        foreach (XmlNode StoreInfo in StoreList)
        {

            // Load store nodes
            LoadStoreNodes(StoreInfo);
            
        }

    }
    void LoadStoreNodes(XmlNode StoreInfo)
    {
        GameObject NewStore = (GameObject)Instantiate(StorePrefab);
        Store storeobj = NewStore.GetComponent<Store>();

        XmlNodeList StoreNodes = StoreInfo.ChildNodes;
        foreach (XmlNode StoreNode in StoreNodes)
        {

            SetStoreObj(storeobj, StoreNode, NewStore);


        }
        // setup to store next cost
        storeobj.SetNextStoreCost(storeobj.BaseStoreCost);
        // Connect our store to the parent panel
        NewStore.transform.SetParent(StorePanel.transform);

    }
    void SetStoreObj(Store storeobj, XmlNode StoreNode, GameObject NewStore)
    {
        if (StoreNode.Name == "name")
        {
            string SetName = StoreNode.InnerText;
            TMP_Text StoreText = NewStore.transform.Find("StoreNameText").GetComponent<TMP_Text>();
            StoreText.text = StoreNode.InnerText;
            storeobj.StoreName = StoreNode.InnerText;
        }
        if (StoreNode.Name == "image")
        {
            Sprite newSprite = Resources.Load<Sprite>(StoreNode.InnerText);
            Image StoreImage = NewStore.GetComponent<Image>();
            StoreImage.sprite = newSprite;
        }

        if (StoreNode.Name == "BaseStoreProfit")
            storeobj.BaseStoreProfit = float.Parse(StoreNode.InnerText, System.Globalization.CultureInfo.InvariantCulture);
        if (StoreNode.Name == "BaseStoreCost")
            storeobj.BaseStoreCost = float.Parse(StoreNode.InnerText, System.Globalization.CultureInfo.InvariantCulture);

        if (StoreNode.Name == "StoreTimer")
            storeobj.StoreTimer = float.Parse(StoreNode.InnerText, System.Globalization.CultureInfo.InvariantCulture);
        if (StoreNode.Name == "StoreMultiplier")
            storeobj.StoreMultiplier = float.Parse(StoreNode.InnerText, System.Globalization.CultureInfo.InvariantCulture);
        if (StoreNode.Name == "StoreTimerDivision")
            storeobj.StoreTimerDivision = int.Parse(StoreNode.InnerText);
        if (StoreNode.Name == "StoreCount")
            storeobj.StoreCount = int.Parse(StoreNode.InnerText);
        if (StoreNode.Name == "ManagerCost")
            CreateManager(StoreNode, storeobj);


    }
    void CreateManager(XmlNode StoreNode, Store storeobj)
    {
        GameObject NewManager = (GameObject)Instantiate(ManagerPrefab);
        NewManager.transform.SetParent(ManagerPanel.transform);
        TMP_Text ManagerNameText = NewManager.transform.Find("ManagerNameText").GetComponent<TMP_Text>();
        ManagerNameText.text = storeobj.StoreName;
        storeobj.ManagerCost = float.Parse(StoreNode.InnerText);
        Button ManagerButton = NewManager.transform.Find("UnlockManagerButton").GetComponent<Button>();
        TMP_Text Buttontext = ManagerButton.transform.Find("UnlockManagerButtonText").GetComponent<TMP_Text>();
        Buttontext.text = "Unlock" + storeobj.ManagerCost.ToString("F2");


        UIStore UIManager = storeobj.GetComponent<UIStore>();
        UIManager.ManagerButton = ManagerButton;
        ManagerButton.onClick.AddListener(storeobj.UnlockManager);
    }
    void CreateUpgrade(XmlNode StoreNode, Store storeobj)
    {
        GameObject NewUpgrade = (GameObject)Instantiate(UpgradePrefab);
        NewUpgrade.transform.SetParent(ManagerPanel.transform);
        TMP_Text UpgradeNameText = NewUpgrade.transform.Find("UpgradeNameText").GetComponent<TMP_Text>();
        UpgradeNameText.text = storeobj.StoreName;
        storeobj.UpgradeCost = float.Parse(StoreNode.InnerText);
        Button UpgradeButton = NewUpgrade.transform.Find("UnlockUpgradeButton").GetComponent<Button>();
        TMP_Text Buttontext = UpgradeButton.transform.Find("UnlockUpgradeButtonText").GetComponent<TMP_Text>();
        Buttontext.text = "Upgrade" + storeobj.UpgradeCost.ToString("F2");


        UIStore UIManager = storeobj.GetComponent<UIStore>();
        UIManager.UpgradeButton = UpgradeButton;
        UpgradeButton.onClick.AddListener(storeobj.UnlockManager);
    }
   
   
}
