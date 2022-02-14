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
        string[] mapData = new string [] {"28888888888883","60000000000009","11100000111009","70111000101006","90001101101111","90000111000007","90000000000009","48888888888885"};

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

        //SpawnMonster();
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

    public void GeneratePath()
    {
        path = PathAlgorithm.GetPath();
    }
}
