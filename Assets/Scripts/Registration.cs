using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

[System.Obsolete]
public class Registration : MonoBehaviour
{
    private const string URL = "http://localhost/UnityMySQL/register.php";
    public TMP_InputField nameFieldTMP;
    public TMP_InputField passwordFieldTMP;
    public int charLimit = 8;
    public Button submitButton;

    public GameObject registrationUI;
    public GameObject loginUI;


    public void CallRegister()
    {
        StartCoroutine(RegisterOld());
    }

    private IEnumerator RegisterOld()
    {
        /* e WWWForm object directly to the WWW constructor, but you will need this variable if you want to 
         * change the request headers sent to the web server.
         */
        WWWForm form = new WWWForm();
        form.AddField("name", nameFieldTMP.text);
        form.AddField("password", passwordFieldTMP.text);

        // Request connection
        WWW www = new WWW(URL, form);

        //UnityWebRequest.Post(URL, form);
        //www.SendWebRequest();
        yield return www;

        if (www.text == "0")
        {
            // OPEN Login UI
            Debug.Log("User created successfully " + www.text);
            registrationUI.SetActive(false);
            loginUI.SetActive(true);

        }
        else
        {
            Debug.Log("Error has occur: " + www.error + " " + www.text);
        }
    }

    private IEnumerator RegisterNew()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameFieldTMP.text);
        form.AddField("password", passwordFieldTMP.text);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {

            var progress = www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                while (!progress.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
                    Debug.Log("Form upload complete!");
                    yield return null;

                }
            }
        }


    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameFieldTMP.text.Length >= 8 && passwordFieldTMP.text.Length >= charLimit);
    }
}

