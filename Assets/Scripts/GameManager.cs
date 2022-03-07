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

    private bool IsSpawning = false;

    [SerializeField]
    private Text statTxt;

    [SerializeField]
    private GameObject prompt1;

    [SerializeField]
    private Text promptText;

    [SerializeField]
    private GameObject prompt3;

    [SerializeField]
    private Text promptText3;

    [SerializeField]
    private GameObject prompt4;

    [SerializeField]
    private Text promptText4;

    [SerializeField]
    private GameObject prompt5;

    [SerializeField]
    private Text promptText5;

    [SerializeField]
    private GameObject fullAch;

    [SerializeField]
    private GameObject oneAch;

    [SerializeField]
    private GameObject advance;

    [SerializeField]
    private GameObject esc;

    [SerializeField]
    private Text upgradePrice;

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
            this.currencyTxt.text = value.ToString();

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
        Fade.Instance.OnButtonClick();
        Fade.Instance.OnFadeTip();
        Fade.Instance.OnFadeText();
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
           // RandomizeMonster(5); 
            //***
            for (int i = 0; i < 4; i++)
            {
                RandomizeMonster(1); 
                yield return new WaitForSeconds(1.5f);
            }

            yield return new WaitForSeconds(5); //
            //***

            //***
            for (int i = 0; i < 3; i++)
            {
                RandomizeMonster(2);
                yield return new WaitForSeconds(2);
            }
            yield return new WaitForSeconds(5);
            //***

            //***
            for (int i = 0; i < 2; i++)
            {
                RandomizeMonster(3);
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(5);
            //***

            //***
            for (int i = 0; i < 1; i++)
            {
                RandomizeMonster(4);
                yield return new WaitForSeconds(1.5f);
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

            RandomizeMonster();
            yield return new WaitForSeconds(3);

            for (int i = 0; i < 9; i++)
            {
                RandomizeMonster();
                yield return new WaitForSeconds(3);
                RandomizeMonster(1);
                yield return new WaitForSeconds(3);
            }
            RandomizeMonster();

            IsSpawning = false;
        }
        //**********
 
        //Wave 3:
        //**********
        if(wave==3)
        {
            for (int i = 0; i < 1; i++)
            {
                RandomizeMonster(1);
                yield return new WaitForSeconds(1.5f);
            }
            health += 5;

            for (int i = 0; i < 20; i++)
            {
                RandomizeMonster(4);
                yield return new WaitForSeconds(3);
                RandomizeMonster();
                yield return new WaitForSeconds(3);
            }
            RandomizeMonster();

            IsSpawning = false;
        }
        //**********
 
        //Wave 4:
        //**********
        if(wave==4)
        {
            health += 5;

            for (int i = 0; i < 4; i++)
            {
                RandomizeMonster(4);
                yield return new WaitForSeconds(1);
            }

            for (int i = 0; i < 20; i++)
            {
                RandomizeMonster();
                yield return new WaitForSeconds(1.5f);
                RandomizeMonster();
                yield return new WaitForSeconds(2.5f);
            }

            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(1);
                RandomizeMonster(2);
            }

            IsSpawning = false;
        }
        //**********
 
        //Wave 5:
        //**********
        if(wave==5)
        {
            health += 5;

            for(int i = 0; i < 3; i++)
            {
                RandomizeMonster(2);
                yield return new WaitForSeconds(0.5f);
            }

            for (int i = 0; i < 30; i++)
            {
                yield return new WaitForSeconds(2);
                RandomizeMonster();
                yield return new WaitForSeconds(1);
                if (i % 4 == 0)
                {
                    RandomizeMonster(2);
                    yield return new WaitForSeconds(1);
                }
                RandomizeMonster();
            }

            yield return new WaitForSeconds(5);
            RandomizeMonster(5);

            IsSpawning = false;
        }
        //**********

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
                type = "Monster1_Colored";
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
                type = "Monster3_Colored";
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn(health+10);
                activeMonsters.Add(monster);
                break;
            case 4:
                type = "Monster4_Colored";
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn(health+15);
                activeMonsters.Add(monster);
                break;
            case 5:
                type = "Monster_Boss";
                monster = Pool.GetObject(type).GetComponent<Monster>();
                monster.Spawn(health+400);
                activeMonsters.Add(monster);
                break;
        }
    }


    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!WaveActive && !IsSpawning)
        {
            if (wave == 1)
            {
                Prompt();
                waveBtn.SetActive(true);
            }

            if (wave == 2)
            {
                Prompt3();
                waveBtn.SetActive(true);
            }

            if (wave == 3)
            {
                Prompt4();
                waveBtn.SetActive(true);
            }

            if (wave == 4)
            {
                Prompt5();
                waveBtn.SetActive(true);
            }

            if (wave == 5)
            {
                if (hp == 10)
                {
                    Achievement();
                }

                if (hp == 1)
                {
                    Achievement2();
                }

                WinGame();
            }

        }
    }

    public void PickTower(TowerBtn towerBtn)
    {
        if(Currency >= towerBtn.Price){
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
            esc.SetActive(true);
        }
    }

    public void BuyTower()
    {
        if(Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
            Hover.Instance.Deactivate();
            esc.SetActive(false);
        }
    }
    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
            esc.SetActive(false);
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

        sellText.text = "+" + (selectedTower.Price / 2).ToString() +"$";
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
            if(Time.timeScale == 2)
            {
                Time.timeScale = 1;
            }
            gameOver = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void WinGame()
    {
        if (!gameOver)
        {
            if(Time.timeScale == 2)
            {
                Time.timeScale = 1;
            }
            gameOver = true;
            advance.SetActive(true);
        }
    }

    public void ShowStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void ShowSelectedTowerStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        UpdateUpgradeTip();
    }

    public void SetTooltipText(string txt)
    {
        statTxt.text = txt;
    }

    public void Achievement()
    {
        fullAch.SetActive(true);
    }

    public void Achievement2()
    {
        oneAch.SetActive(true);
    }

    public IEnumerator Pause()
    {
        yield return new WaitForSeconds(10);
    }

    public void Prompt()
    {
        prompt1.SetActive(true);
        Fade.Instance.OnFadeTip2();
        Fade.Instance.OnFadeText2();

    }

    public void Prompt3()
    {
        prompt3.SetActive(true);
        Fade.Instance.OnFadeTip3();
        Fade.Instance.OnFadeText3();
    }

    public void Prompt4()
    {
        prompt4.SetActive(true);
        Fade.Instance.OnFadeTip4();
        Fade.Instance.OnFadeText4();
    }

    public void Prompt5()
    {
        prompt5.SetActive(true);
        Fade.Instance.OnFadeTip5();
        Fade.Instance.OnFadeText5();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public void FastForward()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    
    public void UpdateUpgradeTip()
    {
        if (selectedTower != null)
        {
            sellText.text = "+" + (selectedTower.Price / 2).ToString() + "$";
            SetTooltipText(selectedTower.GetStats());

            if (selectedTower.NextUpgrade != null)
            {
                upgradePrice.text = selectedTower.NextUpgrade.Price.ToString() + "$";
            }
            else
            {
                upgradePrice.text = string.Empty;
            }
        }
    }

    public void UpgradeTower ()
    {
        if (selectedTower != null)
        {
            if (selectedTower.Level <= selectedTower.Upgrades.Length && Currency >= selectedTower.NextUpgrade.Price)
            {
                selectedTower.Upgrade();
            }

        }
    }
}
