using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManager ;
    private void Awake() {
        if (audioManager != null )
        {
            Destroy(gameObject);
        }
        else{ 
            audioManager = this;
            DontDestroyOnLoad(this) ;
        }
    }

    private void Update()
    {
        if (Settings.soundChecker == false)
        {
            this.gameObject.SetActive(false);
        }
        else if (Settings.soundChecker == true)
        {
            this.gameObject.SetActive(true);
        }

    }
}