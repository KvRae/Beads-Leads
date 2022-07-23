using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject pause ;
    public GameController gameController ;
    public DragController dragController ;
    public Text playerText ;
    public void setup(int player, int sceneID)
    {
        pause.SetActive(false) ;
        gameObject.SetActive(true);
        playerText.text ="PLAYER " + player.ToString() + " WINS" ;

        if(sceneID == 2)
            gameController.stopAll();
        else if(sceneID == 3)
            dragController.stopAll();

    }
    public void restartButton(int sceneID)
    {
        SceneManager.LoadScene(sceneID); 
    }
    public void exitButton(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

}
