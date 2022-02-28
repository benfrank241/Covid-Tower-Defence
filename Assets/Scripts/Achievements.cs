using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : Singleton <Achievements>
{

    [SerializeField]
    public GameObject full;

    public bool fullHealth = true;

    private void Awake()
    {
        if (fullHealth)
        {
            full.SetActive(true);
        }
    }


    public void Select ()
    {
            fullHealth = false;
    }

    //public void FullHealth()
    //{
    //    full.SetActive(true);
    //}


}
