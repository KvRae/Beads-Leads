using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuPlayer : MonoBehaviour
{
    public Vector3 initPos;

    public bool locked = false;

    public Collider2D cpucollider ;


    void Start()
    {
        if(Settings.beadColorIndex ==0)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
           GetComponent<Renderer>().material.color = Color.yellow; 
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cpuMove (Transform pos) 
    { 
         this.transform.localPosition = new Vector2(pos.position.x , pos.position.y);
         this.initPos = this.transform.localPosition ;
        
    }

}


