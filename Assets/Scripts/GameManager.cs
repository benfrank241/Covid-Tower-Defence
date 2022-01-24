using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    public void StartWave()
    {
        StartCoroutine("SpawnWave",2.5f);
    }

    private IEnumerable SpawnWave()
    {
        string type = "MovingMonster";

        Pool.GetObject(type);

        //yield return new WaitForSeconds(2.5f);
        yield return null;
    }

}
