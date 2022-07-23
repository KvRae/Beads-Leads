using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgColor : MonoBehaviour
// Update is called once per frame
{   

    public Sprite bg1 ;
    public Sprite bg2 ; 
    
    void Start()
    {
        if (Settings.bgColorIndex == 0) 
      {
        this.GetComponent<SpriteRenderer>().sprite = bg1 ;
        Debug.Log("bg1");
      }  
       else
      {
        this.GetComponent<SpriteRenderer>().sprite = bg2 ;
        Debug.Log("bg2");
      }  
    }
}

