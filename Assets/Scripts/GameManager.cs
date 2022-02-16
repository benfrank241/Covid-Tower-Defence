using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


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

    [SerializeField]
    private GameObject upgradePanel;

    [SerializeField]
    private Text sellText;

    private bool IsSpawning = false;


    private int health = 12;



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
        Currency = 25;
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

        SoundManager.Instance.PlaySFX("monsterspawn");

        waveTxt.text = string.Format("Wave: {0}/5", wave);

        //print("running");
        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
 
        IsSpawning = true;
 
        //Wave 1: Introductory Wave
        //**********
        if(wave==1)
        {
            //***
            for(int i = 0; i < 4; i++)
            {
                RandomizeMonster(1); 
                yield return new WaitForSeconds(1.5f);
            }
 
            yield return new WaitForSeconds(5);
            //***
 
            //***
            for(int i = 0; i < 3; i++)
            {
                RandomizeMonster(2);
                yield return new WaitForSeconds(2);
            }
            yield return new WaitForSeconds(5);
            //***
 
            //***
            for(int i = 0; i < 2; i++)
            {
                RandomizeMonster(3);
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(5);
            //***
 
            //***
            for(int i = 0; i < 1; i++)
            {
                RandomizeMonster(4);
                //yield return new WaitForSeconds(1.5f);
            }
            //***
            IsSpawning = false;
        }
        //**********
 
 
        //Wave 2:
        //**********
        if(wave==2)
        {
            health += 5;

            for(int i = 0; i < 10; i++)
            {
                RandomizeMonster();
                yield return new WaitForSeconds(3);
                RandomizeMonster(1);
                yield return new WaitForSeconds(3);
            }

            IsSpawning = false;
        }
        //**********
 
        //Wave 3:
        //**********
        if(wave==3)
        {

            for(int i = 0; i < 20; i++)
            {
                RandomizeMonster(4);
                yield return new WaitForSeconds(3);
                RandomizeMonster(); 
                yield return new WaitForSeconds(3);
            }

            IsSpawning = false;
        }
        //**********
 
        //Wave 4:
        //**********
        if(wave==4)
        {
            health += 5;

            for(int i = 0; i < 4; i++)
            {
                RandomizeMonster(4);
                yield return new WaitForSeconds(1);
            }

            for(int i = 0; i < 20; i++)
            {
                RandomizeMonster();
                yield return new WaitForSeconds(1.5f);
                RandomizeMonster();
                yield return new WaitForSeconds(2.5f);
            }

            for(int i = 0; i < 4; i++)
            {
                RandomizeMonster(2);
                yield return new WaitForSeconds(1);
            }

            IsSpawning = false;
        }
        //**********
 
        //Wave 5:
        //**********
        if(wave==5)
        {
            health += 10;

            for(int i = 0; i < 5; i++)
            {
                RandomizeMonster(2);
                yield return new WaitForSeconds(0.5f);
            }

            for(int i = 0; i < 30; i++)
            {
                RandomizeMonster();
                yield return new WaitForSeconds(1);
                if(i%4 == 0)
                {
                    RandomizeMonster(2);
                    yield return new WaitForSeconds(1);
                }
                RandomizeMonster();
                yield return new WaitForSeconds(2);
            }

            IsSpawning = false;
        }
        //**********

        //IsSpawning = false;
    }
    
    private void RandomizeMonster(int monsterIndex = 0)
    {
        if(monsterIndex == 0)
        {
            monsterIndex = Random.Range(1,5);
        }

        Monster monster;
        string type = string.Empty;

        switch(monsterIndex)
        {
            case 1:
                type = "Monster1";
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn(health);
                activeMonsters.Add(monster);
                break;
            case 2:
                type = "Monster2";
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn(health+5);
                activeMonsters.Add(monster);
                break;
            case 3:
                type = "Monster3";
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn(health+10);
                activeMonsters.Add(monster);
                break;
            case 4:
                type = "Monster4";
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn(health+15);
                activeMonsters.Add(monster);
                break;
        }
    }


    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!WaveActive && !IsSpawning)
        {
            waveBtn.SetActive(true);
        }
    }

    public void PickTower(TowerBtn towerBtn)
    {
        if(Currency >= towerBtn.Price){
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }
    }

    public void BuyTower()
    {
        if(Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
            Hover.Instance.Deactivate();
        }
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

        sellText.text = "+" + (selectedTower.Price / 2).ToString();
        upgradePanel.SetActive(true);
    }
    public void DeselectTower()
    {
        if(selectedTower !=null)
        {
            selectedTower.Select();
        }
        upgradePanel.SetActive(false);
        selectedTower = null;
    }

    public void SellTower ()
    {
        if (selectedTower != null)
        {
            Currency += selectedTower.Price / 2;

            selectedTower.GetComponentInParent<TileScript>().IsEmpty = true;

            Destroy(selectedTower.transform.parent.gameObject);

            SoundManager.Instance.PlaySFX("sell");

            DeselectTower();
        }
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
