using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels ;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayFabLogin : MonoBehaviour
{
    public GameObject registerUi ;
    public GameObject errorUi ;
    [SerializeField] private Text output = default ;

    [SerializeField] private InputField usernameInput = default ; 

    [SerializeField] private InputField emailInput = default ; 
    
    [SerializeField] private InputField passwordInput = default ; 

    public static string SessionTicket ; 

    private void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest 
        {
            Username = usernameInput.text,
            Email = emailInput.text, 
            Password = passwordInput.text
        },  result =>
        {
            SessionTicket = result.SessionTicket ;
            registerUi.SetActive(false);
            errorUi.SetActive(true);
            output.text = "Account Created! ";
        },  error =>
        {
            PlayFabErrorMsg(error);
            registerUi.SetActive(false);
            errorUi.SetActive(true);
        });
    }

    private void logIn ()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest 
        {
            Username = usernameInput.text,
            Password = passwordInput.text,
        },  result =>
        {
            AccountManager.userName = usernameInput.text;
            SessionTicket = result.SessionTicket ;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        },  error =>
        {
            PlayFabErrorMsg(error);
            registerUi.SetActive(false);
            errorUi.SetActive(true);
        });
    }

    public void LoginCustomID ()
    {
       /* PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest 
        {

            TitleId = "172EC",
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        },  result =>
        {
            SessionTicket = result.SessionTicket ;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        },  error =>
        {
            
        });*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //reset playfab resset password request 
    private void ResetPassword ()
    {
        PlayFabClientAPI.SendAccountRecoveryEmail(new SendAccountRecoveryEmailRequest 
        {
            Email = emailInput.text,
            TitleId = "172EC",
        },  result =>
        {
            output.text = "Password Reset Mail Sent! ";
            registerUi.SetActive(false);
            errorUi.SetActive(true);
            

        },  error =>
        {
            registerUi.SetActive(false);
            errorUi.SetActive(true);
            PlayFabErrorMsg(error);
            
        });
    }
    //error message handeler
    private void PlayFabErrorMsg(PlayFabError error) 
    {
        switch (error.Error)
        {
             case PlayFabErrorCode.InvalidUsername:
                output.text = "Your Username Is Invalid! ";
                break; 
            case PlayFabErrorCode.InvalidEmailAddress:
                output.text = "Your Email Is Invalid! ";
                break; 
            case PlayFabErrorCode.InvalidPassword:
                output.text = "Your Password Is Invalid! ";
                break; 
            case PlayFabErrorCode.InvalidEmailOrPassword:
                output.text = "Invalid Email Or Password!";
                break ;
            case PlayFabErrorCode.InvalidUsernameOrPassword:
                output.text = "Invalid Username Or Password!";
                break ;

            case PlayFabErrorCode.AccountNotFound:
                output.text = "Account Not Found!" ;
                break ;
            case PlayFabErrorCode.InvalidTitleId:
                output.text = "Check Your Username!" ;
                break ;
            case PlayFabErrorCode.InvalidParams :
                output.text ="One Of The Required Field Is Empty Or Invalid" ;
                break ;
            case PlayFabErrorCode.ProfileDoesNotExist:
                output.text = "Account Not Exist!" ;
                break ;
            case PlayFabErrorCode.EmailAddressNotAvailable:
                output.text = "Email Is Used Already";
                break;

            case PlayFabErrorCode.UsernameNotAvailable:
                output.text = "Username Is Used Already";
                break;



            default :
                Debug.Log(error.GenerateErrorReport());
                output.text= "Check Your Network Connection Or Try Again Later ";
                break;
        }
    }
        
    

}
