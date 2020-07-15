using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenu : MonoBehaviour
{

    public TextMeshProUGUI playerDisplay;

    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            playerDisplay.text = DBManager.username;
        }
    }

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(DBManager.username))
        { 
            Debug.Log("Please login first");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void Logout()
    {
        DBManager.LogOut();
        playerDisplay.text = DBManager.username;
    }

}
