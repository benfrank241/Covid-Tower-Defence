using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager>
{

    public event CurrencyChanged Changed;
    
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
    private GameObject statsPanel;

    [SerializeField]
    private Text sellText;

    [SerializeField]
    private Text statTxt;


    private int health = 15;



    private List<Monster> activeMonsters = new List<Monster>();

    private int numMonsters = 0;
    private int threshold = 10;


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

            OnCurrencyChanged();
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

        //print("runnings12");
        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
 
 
        //Wave 1: Introductory Wave
        //**********
        if(wave==1)
        {
            //***
            for(int i = 0; i < 1; i++)
            {
                RandomizeMonster(1);
                numMonsters++;
 
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(3);
            //***
 
            //***
            for(int i = 0; i < 2; i++)
            {
                RandomizeMonster(2);
                numMonsters++;
 
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(3);
            //***
 
            //***
            for(int i = 0; i < 3; i++)
            {
                RandomizeMonster(3);
                numMonsters++;
 
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(3);
            //***
 
            //***
            for(int i = 0; i < 4; i++)
            {
                RandomizeMonster(4);
                numMonsters++;
 
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(1);

            //***
        }
        //**********
 
 
        //Wave 2:
        //**********
        if(wave==2)
        {
            health += 5;

            for(int i = 0; i < 20; i++)
            {
                RandomizeMonster();
                numMonsters++;
 
                yield return new WaitForSeconds(2);
            }
        }
        //**********
 
        //Wave 3:
        //**********
        if(wave==3)
        {

            for(int i = 0; i < 20; i++)
            {
                RandomizeMonster(4);
                numMonsters++;
                yield return new WaitForSeconds(1);
                RandomizeMonster();
                numMonsters++;
 
                yield return new WaitForSeconds(1.5f);
            }
        }
        //**********
 
        //Wave 4:
        //**********
        if(wave==4)
        {
            health += 10;

            for(int i = 0; i < 4; i++)
            {
                RandomizeMonster(4);
 
                yield return new WaitForSeconds(1);
            }

            for(int i = 0; i < 20; i++)
            {
                RandomizeMonster();
                yield return new WaitForSeconds(0.5f);
                RandomizeMonster();
                yield return new WaitForSeconds(2);
            }

            for(int i = 0; i < 4; i++)
            {
                RandomizeMonster(2);
 
                yield return new WaitForSeconds(1);
            }
        }
        //**********
 
        //Wave 5:
        //**********
        if(wave==5)
        {
            health += 25;

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
            if (!WaveActive)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }

        }
        //**********

    }
    
    private void RandomizeMonster(int monsterIndex = 0)
    {
        if(monsterIndex == 0)
        {
            monsterIndex = Random.Range(1,5);
        }

        string type = string.Empty;

        switch(monsterIndex)
        {
            case 1:
                type = "Monster1";
                break;
            case 2:
                type = "Monster2";
                break;
            case 3:
                type = "Monster3";
                break;
            case 4:
                type = "Monster4";
                break;
        }

        Monster monster = Pool.GetObject(type).GetComponent<Monster>();
        monster.Spawn(health);
        activeMonsters.Add(monster);

        //yield return new WaitForSeconds(2.5f);
    }


    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!WaveActive && numMonsters >= threshold)
        {
            if (wave == 5)
            {
                WinGame();
            }
            waveBtn.SetActive(true);
            threshold += 10;
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

    public void OnCurrencyChanged()
    {
        if (Changed != null)
        {
            Changed();
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

    public void WinGame()
    {
        if (!gameOver)
        {
            gameOver = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    public void ShowStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void SetTooltipText(string txt)
    {
        statTxt.text = txt;
    }
}
