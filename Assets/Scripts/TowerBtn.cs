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
            case "Hospital":
                tooltip = string.Format("<color=#ffa500ff><size=30><b>Hospital</b></size></color><size=15>\nDamage: 10 \nRange: 4.5 \nSpeed: 2 per sec \n<color=#00ffffff>Debuff: Slow</color></size>");
                break;

            case "Factory":
                tooltip = string.Format("<color=#00ff00ff><size=30><b>Factory</b></size></color><size=15>\nDamage: 5 \nRange: 3 \nSpeed: 3 per sec</size>");
                break;

            case "Vaccine":
                tooltip = string.Format("<color=#ffff00><size=30><b>Vaccine</b></size></color><size=15>\nDamage: 20 \nRange: 5 \nSpeed: 2 per sec </size>");
                break;

            case "Witch Doctor":
                tooltip = string.Format("<color=#ff0000><size=30><b>Witch Doctor</b></size></color><size=15>\nDamage: x \nRange: x \nSpeed: x per sec \n<color=#ff0000>Debuff: Burn</color></size>");
                break;

            case "Sanitizer":
                tooltip = string.Format("<color=#00ffffff><size=30><b>Sanitizer</b></size></color><size=15>\nDamage: x \nRange: x \nSpeed: x per sec \n<color=#00ffffff>Debuff: Freeze</color></size>");
                break;
        }
        GameManager.Instance.SetTooltipText(tooltip);
        GameManager.Instance.ShowStats();
    }
}
