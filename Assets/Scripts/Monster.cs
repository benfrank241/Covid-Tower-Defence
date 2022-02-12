using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed;



    [SerializeField]
    private Stat health;





    private Stack<Node> path;

    public Point GridPosition { get; set; }

    private Vector3 destination;

    private void Update()
    {
        Move();
    }



    private void Awake()
    {
       // myAnimator = GetComponent<Animator>();
        health.Initialize();
    }





    public bool IsActive {get ; set; }

    public void Spawn(int health)
    {
        transform.position = LevelManager.Instance.Tiles[new Point(0,2)].GetComponent<TileScript>().WorldPosition;

        this.health.MaxVal = health;
        this.health.CurrentVal = this.health.MaxVal;


        IsActive = true;

        SetPath(LevelManager.Instance.Path);
        //SoundManager.Instance.PlaySFX("monsterspawn");
    }

    private void Move()
    {
        if(IsActive == true){
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if(transform.position == destination)
            {
                Vector3 endPath = LevelManager.Instance.Tiles[new Point(13,4)].GetComponent<TileScript>().WorldPosition;
                if(destination == endPath)
                {
                    GameManager.Instance.Hp--;

                    Release();

                    SoundManager.Instance.PlaySFX("oof");
                    GameManager.Instance.RemoveMonster(this);

                }

                if(path != null && path.Count > 0)
                {
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            this.path = newPath;

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }

    private void Release()
    {
        IsActive = false;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
        
    }
    
    public void TakeDamage(int damage)
    {
        if(IsActive)
        {
            health.CurrentVal -= damage;

            if(health.CurrentVal <= 0)
            {
                GameManager.Instance.Currency +=2;
                Release();

                GameManager.Instance.RemoveMonster(this);

                IsActive = false;
                GetComponent<SpriteRenderer>().sortingOrder--;
            }
        }
    }


}
