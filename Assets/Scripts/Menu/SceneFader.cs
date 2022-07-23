using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Animator transition ;

    public float transitionTime = 1f ;

    
    public void LoadNextScene (int SceneID)
    {
        StartCoroutine(loadScene(SceneManager.GetActiveScene().buildIndex +  SceneID));
    }
    IEnumerator loadScene (int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}
