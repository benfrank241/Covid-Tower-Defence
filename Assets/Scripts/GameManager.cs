using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool Pool { get; set; }

    private int currency;
    private int hp;

    [SerializeField]
    private Text currencyTxt;

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            this.currency = value;
            this.currencyTxt.text = value.ToString() + "<color=lime>$</color>";
        }
    }

    [SerializeField]
    private Text HPTxt;

    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
            this.hp = value;
            this.HPTxt.text = value.ToString();
        }
    }

    //Use this for initialization
    void Start ()
    {
        Currency = 5;
        Hp = 10;
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    public void StartWave()
    {
        StartCoroutine("SpawnWave",2.5f);
    }

    private IEnumerable SpawnWave()
    {
        string type = "MovingMonster";

        Pool.GetObject(type);

        //yield return new WaitForSeconds(2.5f);
        yield return null;
    }

}
