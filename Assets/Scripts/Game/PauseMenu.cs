
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class PauseMenu : MonoBehaviour
{
    public GameController gameController ;
    public DragController dragController ;

    
   
    [SerializeField] GameObject pauseMenu ;

    public void Pause (int sceneID)
    {
        pauseMenu.SetActive(true);
        if (sceneID == 2)
            gameController.stopAll() ;
        else if (sceneID == 3)
            dragController.stopAll() ;


        
    }
    public void Resume(int sceneID)
    {
        pauseMenu.SetActive(false);
        
        if (sceneID == 2)
            gameController.resumeGame();
        else if (sceneID == 3)
            dragController.resumeGame() ;
        
    }

    public void Restart(int sceneID)
    {
        Time.timeScale = 1f ;
        SceneManager.LoadScene(sceneID);
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f ;
        SceneManager.LoadScene(sceneID);


    }
}
