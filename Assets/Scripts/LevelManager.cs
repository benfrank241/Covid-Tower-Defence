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
    private Point endPath;

    private Stack<Node> path;

    public Stack<Node> Path
    {
        get
        {
            if (path == null)
            {
                GeneratePath();
            }

            return new Stack<Node>(new Stack<Node>(path));
        }
    }


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
        string[] mapData = new string [] {
            "10.14.14.14.14.14.14.14.14.14.14.14.14.14.14.14.14.14.11",
            "12.17.17.17.17.17.17.17.17.17.17.17.17.17.17.17.17.20.16",
            "1.1.1.0.0.0.0.0.0.0.0.0.0.0.0.0.0.15.16",
            "10.11.1.0.0.0.0.1.1.1.1.1.1.0.0.0.0.15.16",
            "15.16.1.1.0.0.0.1.0.0.0.0.1.0.0.0.0.15.16",
            "15.16.0.1.1.1.0.1.1.1.0.0.1.0.0.0.0.12.13",
            "15.16.0.0.0.1.0.0.0.1.0.0.1.1.0.1.1.1.1",
            "15.16.0.0.0.1.1.0.0.1.0.0.0.1.0.1.0.10.11",
            "15.16.0.0.0.0.1.1.1.1.0.0.0.1.1.1.0.15.16",
            "15.16.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.15.16",
            "12.22.8.8.8.8.8.8.8.8.8.8.8.8.8.8.8.23.13"
            };

        char[] limit = { '.' };
        int mapXSize = mapData[0].Split(limit).Length;
        int mapYSize = mapData.Length;

        Vector3 mapStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for(int y = 0; y < mapYSize; y++)
        {
            //char[] newTiles = mapData[y].ToCharArray();

            string[] newTiles = mapData[y].Split(limit);

            for(int x = 0; x < mapXSize; x++)
            {
                PlaceTile(newTiles[x],x,y,mapStart);
            }

        }

        //SpawnMonster();
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 mapStart)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        newTile.Setup(new Point(x,y), new Vector3(mapStart.x + TileSize*x*0.7f, mapStart.y - TileSize*y*0.7f, 0), map);

    }

    private string[] ReadMapText()
    {
        TextAsset bindData = Resources.Load("Map") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    public void GeneratePath()
    {
        path = PathAlgorithm.GetPath();
    }
}
