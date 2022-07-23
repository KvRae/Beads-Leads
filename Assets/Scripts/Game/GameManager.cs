using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool  audiocheck ;
    public GameObject audioManager ;

    public GameObject dragSound ;

    public GameObject failSound ; 

    public GameObject WinSound ;

    // Start is called before the first frame update
    private void Start()
    {
        
        if (Settings.soundChecker == true)
        {
            audioManager.SetActive(true);
        }
        else
        {
            audioManager.SetActive(false);
        }
    }

    private void Update()
    {
        if (Settings.soundChecker == true)
        {
            audioManager.SetActive(true);
        }
        else
        {
            audioManager.SetActive(false);
        }
    }


    
    

    
}
