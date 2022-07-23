using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab ;
using PlayFab.ClientModels ;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AccountManager : MonoBehaviour
{
    public GameObject accountUI;
    public GameObject errorUI ;
    public static int gamesCount;
    public static string userName= "N/A" ;

    public static int gamesWonCount ;
    [SerializeField] public Text userID ;

    [SerializeField] public Text gamesPlayed; 

    [SerializeField] public Text gamesWon ; 

    private void Start() 
    {    
            userID.text = userName;    
    }


    public static void setStat()
    {
        PlayerPrefs.SetInt("GamesPlayed", gamesCount);
        PlayerPrefs.SetInt("GamesWon", gamesWonCount);
        PlayerPrefs.Save(); 


        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest 
        {
            Data = new Dictionary<string, string> {
                {"GameCount",gamesCount.ToString()},
                {"GameWon",gamesWonCount.ToString()}

            }
        },  
        result =>
        {

            Debug.Log("Success");
        }
        ,  error =>
        {
            Debug.Log("fail");
        });
    }
    public static void loadStat ()
    { 
        int playCount;
        int winCount ;
        if(PlayerPrefs.HasKey ("GamesPlayed") && PlayerPrefs.HasKey ("GamesWon"))
        {
            playCount= PlayerPrefs.GetInt("GamesPlayed") ;
            winCount= PlayerPrefs.GetInt("GamesWon");
            if(gamesCount== 0){
            gamesCount=playCount;
            gamesWonCount=winCount;
            }

        }
        else
        {
            setStat();
        }
        


    }

    private void Update() 
    { 
        gamesPlayed.text = gamesCount.ToString ();
        gamesWon.text = gamesWonCount.ToString ();
        //Debug.Log(PlayerPrefs.GetInt("GamesPlayed"));
    }

    public void resetStats ()
    {
        gamesCount = 0 ;
        gamesWonCount = 0 ;
        PlayerPrefs.DeleteKey("GamesPlayed");
        PlayerPrefs.DeleteKey("GamesWon");
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest 
        {
            Data = new Dictionary<string, string> {
                {"GameCount",gamesCount.ToString()},
                {"GameWon",gamesWonCount.ToString()}

            }
        },  
        result =>
        {

            Debug.Log("Success");
        }
        ,  error =>
        {
            Debug.Log("fail");
        });
        
        
    }



    public void signOut ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        userName = "N/A";

    }


}
