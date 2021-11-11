using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    public static SceneSwap instance;
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

    public void MemoryGame()
    {
        SceneManager.LoadScene(2);
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void LogOut()
    {
        SceneManager.LoadScene(4);
    }
    public void ComingSoon()
    {
        SceneManager.LoadScene(3);
    }
    public void Login()
    {
        SceneManager.LoadScene(0);
    }
    public void FastMath()
    {
        SceneManager.LoadScene(5);
    }
}
