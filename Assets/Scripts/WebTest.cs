using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class WebTest : MonoBehaviour
{
    private const string URL = "http://localhost/UnityMySQL/webtest.php";

    private void Start()
    {
        StartCoroutine(GetDataOld());
    }

    private IEnumerator GetDataNew()
    {
        UnityWebRequest www = new UnityWebRequest(URL) 
        {
            downloadHandler = new DownloadHandlerBuffer() 
        };

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Text: " + www.downloadHandler.text);
        }
    }

    private IEnumerator GetDataOld()
    {
        WWW www = new WWW(URL);
        yield return www;
        Debug.Log(www.text); // JazzMan '\t'	500
        string[] webResult = www.text.Split('\t');
        Debug.Log(webResult[0]);
        int webNumber = int.Parse(webResult[1]);
        Debug.Log(webNumber);
    }

}
