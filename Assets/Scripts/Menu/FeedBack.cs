using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class FeedBack : MonoBehaviour
{
    [SerializeField] InputField feedback;
    [SerializeField] Text output;

     string url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSe0gwTSyEFxPE5wOO8gHH5sph7zE-8jgFChx2xrZKTn1q-YWg/formResponse" ;
     public void Send() {
        {
            
            if (feedback.text.Length >4)
            {

                StartCoroutine(Post(feedback.text));
                
                
            }
            else
            {
                output.text = "Feedback Too short or empty";
            }
        }
       
    }


    IEnumerator Post(string feed)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1614354982",feed);
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();
        if (www.error == null)
        {
            output.text = "Connect to a network to send";
        }
        else
        { 
            output.text = "Feedback Sent";  
        }

    }
}
