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
                tooltip = string.Format("<color=#ffa500ff><size=30><b>Hospital</b></size></color><size=22>\nDamage 8 \nProc: 70% \nDebuff: 2sec \nRate of Fire: 2sec \nSlowing factor: 77%\n<color=#ffff00>Debuff: Slow</color></size>");
                break;

            case "Factory":
                tooltip = string.Format("<color=#00ff00ff><size=30><b>Factory</b></size></color><size=22>\nDamage: 3 \nDebuff: None \nRate of Fire: 2sec</size>");
                break;

            case "Vaccine":
                tooltip = string.Format("<color=#ffff00><size=30><b>Vaccine</b></size></color><size=22>\nDamage 20 \nDebuff: None \nRate of Fire: 3sec </size>");
                break;

            case "Witch Doctor":
                tooltip = string.Format("<color=#ffa500ff><size=30><b>Witch Doctor</b></size></color><size=22>\nDamage 6 \nProc: 70% \nDebuff: 8sec \nRate of Fire: 2sec \nTick time: 2 \nTick Damage: 1 \n<color=#ff0000>Debuff: Burn</color></size>");
                break;

            case "Sanitizer":
                tooltip = string.Format("<color=#00ffffff><size=30><b>Sanitizer</b></size></color><size=22>\nDamage 10 \nProc: 55% \nDebuff: 3sec \nRate of Fire: 3sec  \n<color=#00ffffff>Debuff: Freeze</color></size>");
                break;
        }
        GameManager.Instance.SetTooltipText(tooltip);
        GameManager.Instance.ShowStats();
    }
}
