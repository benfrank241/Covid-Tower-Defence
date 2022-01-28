using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private SpriteRenderer mySpriterRenderer;
     
    
    // Start is called before the first frame update
    void Start()
    {
        mySpriterRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        mySpriterRenderer.enabled = !mySpriterRenderer.enabled;
    }
}
