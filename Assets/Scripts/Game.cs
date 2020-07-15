using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Obsolete]
public class Game : MonoBehaviour
{
    public TextMeshProUGUI playerUsernameTMP;
    public TextMeshProUGUI scoreTMP;

    private const string URL = "http://localhost/UnityMySQL/savedata.php";


    private void Start()
    {
        playerUsernameTMP.text = DBManager.username;
        scoreTMP.text = "Score: " + DBManager.score.ToString();
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    private IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);

        WWW www = new WWW(URL, form);

        yield return www;

        if (www.text == "0")
        {
            Debug.Log("Gave saved");
            scoreTMP.text = "Score: " + DBManager.score + " Saved";

        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }

    }

    public void IncreaseScore()
    {
        DBManager.score++;
        scoreTMP.text = "Score: " + DBManager.score;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
