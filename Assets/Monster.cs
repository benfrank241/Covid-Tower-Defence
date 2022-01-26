using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 2;

    public Vector3 t1;
    public Vector3 t2;

    public Vector3 targetPosition;


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

    private void moveMonster()
    {
        //transform.position = Vector3.MoveTowards(transform.position, t1, speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(t1, t2, speed * Time.deltaTime);

    }
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
    
    public IEnumerator Path()
    {
        //print("run2");
        
        if(transform.position == targetPosition)
        {
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2 * speed * Time.deltaTime);
        yield return new WaitForSeconds(2.5f);
        
    }
    
}
