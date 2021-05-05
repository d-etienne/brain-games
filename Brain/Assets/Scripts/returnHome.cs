using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnHome : MonoBehaviour
{
    public void changeScene()
    {
        //SceneLoader.instance.UserDataScreen();
        SceneManager.LoadScene(0);
        
        //Application.LoadLevel("Login Screen");

    }

}