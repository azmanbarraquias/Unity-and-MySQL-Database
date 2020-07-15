using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Obsolete]
public class Login : MonoBehaviour
{
    private const string URL = "http://localhost/UnityMySQL/login.php";
    public TMP_InputField nameFieldTMP;
    public TMP_InputField passwordFieldTMP;
    public TextMeshProUGUI usernameTMP;
    public int charLimit = 8;
    public Button submitButton;
    public GameObject logOutBTN;
    public GameObject logInBTN;

    public GameObject loginUI;
    public GameObject mainMenu;

    private void Start()
    {
        if (string.IsNullOrWhiteSpace(DBManager.username))
            return;

        logOutBTN.SetActive(true);
        logInBTN.SetActive(false);
    }

    public void CallLogin()
    {
        StartCoroutine(LoginOld());
    }

    private IEnumerator LoginOld()
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

        if (www.text[0] == '0')
        {
            // OPEN Login UI
            Debug.Log("User successfully login, " + www.text);


            usernameTMP.text = nameFieldTMP.text;
            DBManager.username = nameFieldTMP.text;
            DBManager.score = int.Parse(www.text.Split('\t')[1]);

            mainMenu.SetActive(true);
            loginUI.SetActive(false);
            logOutBTN.SetActive(true);
            logInBTN.SetActive(false);
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.error);


        }
    }

    private IEnumerator LoginNew()
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

