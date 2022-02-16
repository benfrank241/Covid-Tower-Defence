using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    
    private Sprite sprite;

    [SerializeField]
    private int price;

    [SerializeField]
    private Text priceText;

    public int Price
    {
        get
        {
            return price ;
        }
    }

    public Sprite Sprite
    {
        get
        {
             return sprite;
        }
    }

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }

    private void Start()
    {
        priceText.text = price + "$";

        GameManager.Instance.Changed += new CurrencyChanged(PriceCheck);
    }

    private void PriceCheck()
    {
        if (price <= GameManager.Instance.Currency)
        {
            GetComponent<Image>().color = Color.white;
            priceText.color = Color.white;
        }
        else 
        {
            GetComponent<Image>().color = Color.grey;
            priceText.color = Color.grey;
        }
    }

    public void ShowInfo(string type)
    {
        string tooltip = string.Empty;

        switch (type)
        {
            case "One":
                tooltip = string.Format("<color=#ffa500ff><size=30><b>Archer</b></size></color><size=15>\nDamage: 2 \nRange: 2 \nSpeed: 2 per sec</size>");
                break;

            case "Two":
                tooltip = string.Format("<color=#00ffffff><size=30><b>Catapult</b></size></color><size=15>\nDamage: 1 \nRange: 1 \nSpeed: 1 per sec</size>");
                break;

            case "Three":
                tooltip = string.Format("<color=#00ff00ff><size=30><b>Sniper</b></size></color><size=15>\nDamage: 3 \nRange: 3 \nSpeed: 3 per sec</size>");
                break;
        }
        GameManager.Instance.SetTooltipText(tooltip);
        GameManager.Instance.ShowStats();
    }
}
