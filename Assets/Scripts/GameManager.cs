using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//New Code
public class GameManager : Singleton<GameManager>
{
    public ObjectPool Pool { get; set; }
    public TowerBtn ClickedBtn { get;set; }


    public float speed = 0.5f;

    private int currency;
    private int hp;

    [SerializeField]
    private Text currencyTxt;

    
    // The current selected tower
    private Tower selectedTower;

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            this.currency = value;
            print(value);
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
    void Start()
    {
        Currency = 5;
        Hp = 10;
    }

     void Update()
    {
        HandleEscape();
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    public void StartWave()
    {
        //print("runnings12");
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        print("runnings");
        string type = "Monster";

        Vector3 movement = new Vector3(1, 0, 0);
        Pool.GetObject(type).transform.Translate(movement * speed * Time.deltaTime);

        yield return new WaitForSeconds(2.5f);
        //yield return null;
    }
    public void PickTower(TowerBtn towerBtn)
    {
        this.ClickedBtn = towerBtn;
        Hover.Instance.Activate(towerBtn.Sprite);
    }

    public void BuyTower()
    {
        Hover.Instance.Deactivate();
        
    }
    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }
    public void SelectTower(Tower tower)
    {
        if(selectedTower !=null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();
    }
    public void DeselectTower()
    {
        if(selectedTower !=null)
        {
            selectedTower.Select();
        }
        selectedTower = null;
    }
}
