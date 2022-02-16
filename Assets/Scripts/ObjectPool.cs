using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterPrefab;

    private GameObject monsters;

    private List<GameObject> pooledObjects = new List<GameObject>();


    private void Awake()
    {
        monsters = new GameObject("Monsters");
        //monsters.transform.position = new Vector3(-7,3.5f,0);
        //monsters.transform.position += new Vector3(LevelManager.Instance.TileSize,0,0);

    }

    public GameObject GetObject(string type)
    {
        /*
        foreach (GameObject go in pooledObjects)
        {
            if(go.name == type && !go.activeInHierarchy)
            {
                go.SetActive(true);
                return go;
            }
        }
        */

        
        for(int i = 0; i < monsterPrefab.Length; i++)
        {
            if(monsterPrefab[i].name == type)
            {
                GameObject newObject = Instantiate(monsterPrefab[i], monsters.transform);
                pooledObjects.Add(newObject);
                newObject.name = type;
                return newObject;
            }
        }

        return null;
    }

    public void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
