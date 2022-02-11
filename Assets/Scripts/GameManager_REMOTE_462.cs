using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    
    public ObjectPool Pool { get; set; }
    public TowerBtn ClickedBtn { get;set; }


    public float speed = 0.5f;

    private int currency;
    private int hp;
    private bool gameOver = false;
    private int wave = 0;

    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    private Text currencyTxt;


    [SerializeField]
    private GameObject waveBtn;

    private List<Monster> activeMonsters = new List<Monster>();


    public bool WaveActive
    {
        get
        {
            return activeMonsters.Count > 0;
        }
    }

    
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

            if (hp <= 0)
            {
                this.hp = 0;
                GameOver();
            }

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
        wave++;


        waveTxt.text = string.Format("Wave: {0}/20", wave);

        //print("runnings12");
        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
 
        string type;
        Monster monster;
 
 
        //Wave 1: Introductory Wave
        //**********
        if(wave==1)
        {
            //***
            type = "Monster1";
 
            monster = Pool.GetObject(type).GetComponent<Monster>();
            monster.Spawn();
            activeMonsters.Add(monster);
 
            yield return new WaitForSeconds(7f);
            //***
 
            //***
            for(int i = 0; i < 2; i++)
            {
                type = "Monster2";
 
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn();
                activeMonsters.Add(monster);
 
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(6f);
            //***
 
            //***
            for(int i = 0; i < 3; i++)
            {
                type = "Monster3";
 
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn();
                activeMonsters.Add(monster);
 
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(6f);
            //***
 
            //***
            for(int i = 0; i < 4; i++)
            {
                type = "Monster4";
 
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn();
                activeMonsters.Add(monster);
 
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(6f);
            //***
        }
        //**********
 
 
        //Wave 2:
        //**********
        if(wave==2)
        {
            for(int i = 0; i < wave; i++)
            {
                type = "Monster2";
 
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn();
                activeMonsters.Add(monster);
 
                yield return new WaitForSeconds(2.5f);
            }
        }
        //**********
 
        //Wave 3:
        //**********
        if(wave==3)
        {
            for(int i = 0; i < wave; i++)
            {
                type = "Monster3";
 
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn();
                activeMonsters.Add(monster);
 
                yield return new WaitForSeconds(2.5f);
            }
        }
        //**********
 
        //Wave 4:
        //**********
        if(wave==4)
        {
            for(int i = 0; i < wave; i++)
            {
                type = "Monster4";
 
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn();
                activeMonsters.Add(monster);
 
                yield return new WaitForSeconds(2.5f);
            }
        }
        //**********
 
        //Wave 5:
        //**********
        if(wave==5)
        {
            for(int i = 0; i < wave; i++)
            {
                type = "Monster4";
 
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn();
                activeMonsters.Add(monster);
 
                yield return new WaitForSeconds(2.5f);
            }
        }
        //**********
 
    }


    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);


        if (!WaveActive)
        {
            waveBtn.SetActive(true);
        }
    }

    public void PickTower(TowerBtn towerBtn)
    {
        if(!WaveActive){
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }
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

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
