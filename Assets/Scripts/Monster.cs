using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed;

    ////private float speed = 2;

    private Stack<Node> path;

    public Point GridPosition { get; set; }

    private Vector3 destination;

    private void Update()
    {
        Move();
    }



    //*****
    /*
    public Vector3 t1;
    public Vector3 t2;

    public Vector3 targetPosition;
    */
    //*****

    public void Spawn()
    {
        transform.position = LevelManager.Instance.Tiles[new Point(1,1)].GetComponent<TileScript>().WorldPosition;

        SetPath(LevelManager.Instance.Path);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if(transform.position == destination)
        {
            Vector3 endPath = LevelManager.Instance.Tiles[new Point(11,6)].GetComponent<TileScript>().WorldPosition;
            if(destination == endPath)
            {
                Destroy(gameObject);
            }

            if(path != null && path.Count > 0)
            {
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().WorldPosition;
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

    //*****
    /*
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        //t1 = new Vector3(-7 + LevelManager.Instance.TileSize,3.5f, 0);
        //t2 = new Vector3(-7 + LevelManager.Instance.TileSize,3.5f - LevelManager.Instance.TileSize, 0);
        targetPosition = new Vector3(-7 + LevelManager.Instance.TileSize*10, 3.5f - LevelManager.Instance.TileSize*5, 0);
    }
    

    // Update is called once per frame
    void Update()
    {
        //Vector3 movement = new Vector3(1, 0, 0);
        //transform.Translate(movement * speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, t1, speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(t1, t2, speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        //Destroy(this);

        //moveMonster();
        StartCoroutine(Path());
        //Destroy(gameObject);
        //StartCoroutine(Path2());
    }
    */
    //*****

    //private void moveMonster()
    //{
        //transform.position = Vector3.MoveTowards(transform.position, t1, speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(t1, t2, speed * Time.deltaTime);

    //}
    /*
    public IEnumerator Path2()
    {
        print("run");
        while(transform.position != t1)
        {
            //move towards point
            //yield return wait for 
            transform.position = Vector3.MoveTowards(transform.position, t1, speed * Time.deltaTime);
        }

        while(transform.position != t2)
        {
            //move towards point
            //yield return wait for 
            transform.position = Vector3.MoveTowards(t1, t2, speed * Time.deltaTime);
        }

        yield return new WaitForSeconds(2.5f);
        //while loop
            //move towards point
            //yield return wait for
        //...
        
    }
    */
    
    //*****
    /*
    public IEnumerator Path()
    {
        //print("run2");
        
        if(transform.position == targetPosition)
        {
            GameManager.Instance.Hp--;
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        yield return new WaitForSeconds(2.5f);
        
    }
    */
    //*****
    
}
