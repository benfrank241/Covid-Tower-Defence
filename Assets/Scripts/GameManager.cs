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
    public bool WaveActive
    {
        get { return activeMonsters.Count > 0; }
    }

    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    private Text currencyTxt;

<<<<<<< HEAD
    private int wave = 0;

=======
>>>>>>> feature_wave_cntr
    [SerializeField]
    private GameObject waveBtn;

    private List<Monster> activeMonsters = new List<Monster>();

<<<<<<< HEAD
    public bool WaveActive
    {
        get
        {
            return activeMonsters.Count > 0;
        }
    }

=======
>>>>>>> feature_wave_cntr
    
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
<<<<<<< HEAD
=======

        waveTxt.text = string.Format("Wave: {0}/20", wave);
>>>>>>> feature_wave_cntr
        //print("runnings12");
        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
<<<<<<< HEAD

        for(int i = 0; i < wave; i++){

            string type = "Monster";

            Monster monster = Pool.GetObject(type).GetComponent<Monster>();
            monster.Spawn();

            activeMonsters.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }
=======
       
        print("runnings");
        string type = "Monster";

        //Vector3 movement = new Vector3(1, 0, 0);
        Monster monster = Pool.GetObject(type).GetComponent<Monster>();//.transform.Translate(movement * speed * Time.deltaTime);
        monster.Spawn();
        activeMonsters.Add(monster);
        yield return new WaitForSeconds(2.5f);
            //yield return null;
>>>>>>> feature_wave_cntr
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

<<<<<<< HEAD
        if(!WaveActive)
=======
        if (!WaveActive)
>>>>>>> feature_wave_cntr
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
