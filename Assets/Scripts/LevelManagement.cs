using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public void loadNexeLevel(){
        SceneManager.LoadScene(StaticScene.getNextLevel());
    }

    public void replay(){
        SceneManager.LoadScene(StaticScene.getCurrentLevel());
    }

    public void loadMainMenu(){
        SceneManager.LoadScene("mainMenu");
    }

}
