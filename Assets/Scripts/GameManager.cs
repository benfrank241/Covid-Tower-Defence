using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool Pool { get; set; }

    public float speed = 0.5f;


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
        //print("runnings");
        string type = "Monster";

        Vector3 movement = new Vector3(1, 0, 0);
        Pool.GetObject(type).transform.Translate(movement * speed * Time.deltaTime);

        yield return new WaitForSeconds(2.5f);
        //yield return null;
    }

}
