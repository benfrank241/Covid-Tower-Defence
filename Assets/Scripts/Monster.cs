using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed;


    private Stack<Node> path;

    public Point GridPosition { get; set; }

    private Vector3 destination;

    private void Update()
    {
        Move();
    }


    public bool IsActive {get ; set; }

    public void Spawn()
    {
        transform.position = LevelManager.Instance.Tiles[new Point(0,2)].GetComponent<TileScript>().WorldPosition;

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
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }
    
}
