using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Point GridPos, Vector3 MapPosition, Transform parent)
    {
        this.GridPosition = GridPos;
        transform.position = MapPosition;
        transform.SetParent(parent);
        
        LevelManager.Instance.Tiles.Add(GridPos,this);
    }
}
