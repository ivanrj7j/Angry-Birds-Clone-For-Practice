using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame(){
        // This loads the first level when the start game button is pressed
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame(){
        Debug.Log("Application quit");
        Application.Quit();
        // quits out of the app 
    }
}
