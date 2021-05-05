using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [SerializeField] public GameObject sign_up;
    [SerializeField] public GameObject register;
    [SerializeField] public GameObject userData;
    [SerializeField] public GameObject scoreboard;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ClearGame()
    {
        sign_up.SetActive(false);
        register.SetActive(false);
        userData.SetActive(false);
        scoreboard.SetActive(false);
    }

    public void Register() // this is when you hit register in login screen
    {
        ClearGame();
        register.SetActive(true);
    }

    public void LoginScreen() // this is for the back button in register screen
    {
        ClearGame();
        sign_up.SetActive(true);
        
    }

    public void UserDataScreen() // this is for the back button in register screen
    {
        ClearGame();
        userData.SetActive(true);
    }
    public void ScoreboardScreen() //Scoreboard button
    {
        ClearGame();
        scoreboard.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
