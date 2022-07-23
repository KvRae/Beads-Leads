using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Settings settings ;
    public GameObject audioSound ;
    private string url;
    private void Awake() 
    {
        settings.soundCheck();
    }
    

    private void Update() {
        settings.soundCheck();
    }
    
    public void PlaySingle ()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
    public void PlayMulti ()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
   }
    public void QuitGame ()
    {    
        Application.Quit();
    }

    public void Rate () 
    {
        Application.OpenURL ("https://kvrae.itch.io/beads-leads");
    }

    public void HowToPlay () 
    {
        Application.OpenURL ("https://vimeo.com/552382966");
    }
    public void AltFeed () 
    {
        Application.OpenURL ("mailto:kv4buissenuss@gmail.com");
    }

    
}
