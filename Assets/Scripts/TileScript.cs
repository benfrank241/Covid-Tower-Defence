using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    public bool IsEmpty { get; set; }

    private Color32 fullColor = new Color32(255,118,118,225);

    private Color32 emptyColor = new Color32(96,255,90,255);

    private SpriteRenderer spriteRenderer;

    private Tower myTower;
    

    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x/2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y/2));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Point GridPos, Vector3 MapPosition, Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = GridPos;
        transform.position = MapPosition;
        transform.SetParent(parent);
        
        LevelManager.Instance.Tiles.Add(GridPos,this);
    }
     private void OnMouseOver()
    {
        ColorTile(fullColor);

        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if(IsEmpty)
            {
                ColorTile(emptyColor);
            }
            if (!IsEmpty)
            {
                ColorTile(fullColor);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }

        }
        else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn == null && Input.GetMouseButtonDown(0))
        {
            if(myTower !=null)
            {
                GameManager.Instance.SelectTower(myTower);
            }
            else
            {
                GameManager.Instance.DeselectTower();
            }
        }
        
    }
    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        
        GameObject tower =(GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab,transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);
        this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
        
        IsEmpty = false;

        ColorTile(Color.white);

        myTower.Price = GameManager.Instance.ClickedBtn.Price;
        
        GameManager.Instance.BuyTower();
        SoundManager.Instance.PlaySFX("construction");
    }
    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}
