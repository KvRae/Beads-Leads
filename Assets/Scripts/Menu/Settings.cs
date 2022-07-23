using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Settings : MonoBehaviour
{
    public Toggle toggler  ;
    public static bool soundChecker = true ;
    public Dropdown dropDown ;

    public static int beadColorIndex = 0 ;
    public Dropdown bgTheme ;
    public static int bgColorIndex = 0 ;

    [SerializeField] GameObject audioManager ;
    [SerializeField] GameObject bgMusic ;
     void Awake() 
    {
        toggler.GetComponent<Toggle>() ;

        dropDown.GetComponent<Dropdown>();
        
        bgTheme.GetComponent<Dropdown>();
        
    }
    
    private void Start()  
    {  
        toggler.isOn = soundChecker ;
        beadColorIndex = dropDown.value; 
        bgColorIndex = bgTheme.value;
    }
    private void Update()
    {
        beadColorCheck();
        soundCheck ();
        backgroundTheme();
        Debug.Log(bgColorIndex);
    }

    public void soundCheck () 
    {
        soundChecker = toggler.isOn ;
        if (soundChecker == true)
        {
            audioManager.SetActive(true);
            if(bgMusic != null)
                bgMusic.SetActive(true);

        }
        else
        {
            audioManager.SetActive(false);
            if(bgMusic != null)
                bgMusic.SetActive(false);
        }
        

    }
    public void beadColorCheck () 
    {
        beadColorIndex = dropDown.value;
    }

    
    public void backgroundTheme()
    {

      bgColorIndex = bgTheme.value;
      
    }


    
}
