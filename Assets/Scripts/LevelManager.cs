using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private Transform map;

    public Dictionary<Point,TileScript> Tiles { get; set; }

    private Point startPath;
    //private Point endPath;

    [SerializeField]

    private GameObject monsterPrefab;


    public float TileSize
    {
        get {return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        //string[] mapData = ReadMapText();
        string[] mapData = new string [] {"0000000000000","0210000000000","0011000000000","0001100000000","0000110000000","0000011110000","0000000011120","0000000000000"};

        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        Vector3 mapStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for(int y = 0; y < mapYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for(int x = 0; x < mapXSize; x++)
            {
                PlaceTile(newTiles[x].ToString(),x,y,mapStart);
            }

        }

        SpawnMonster();
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 mapStart)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        newTile.Setup(new Point(x,y), new Vector3(mapStart.x + TileSize*x, mapStart.y - TileSize*y, 0), map);

    }

    private string[] ReadMapText()
    {
        TextAsset bindData = Resources.Load("Map") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    private void SpawnMonster()
    {
        startPath = new Point(1,1);
        
        //GameObject monster = Instantiate(monsterPrefab, Tiles[startPath].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        /*
        Instantiate(monsterPrefab, Tiles[new Point(2,1)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(2,2)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(3,2)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(3,3)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(4,3)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(4,4)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(5,4)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(5,5)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(6,5)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(7,5)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(8,5)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(8,6)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(9,6)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(10,6)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(monsterPrefab, Tiles[new Point(11,6)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        */

        //monster.transform.localScale += TileSize;

        

    }
}
