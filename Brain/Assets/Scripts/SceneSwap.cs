using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
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
        SceneManager.LoadScene(0);
    }
    public void ComingSoon()
    {
        SceneManager.LoadScene(3);
    }
}
