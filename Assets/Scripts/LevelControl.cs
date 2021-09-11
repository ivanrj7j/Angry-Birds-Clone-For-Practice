using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{

    Monster[] monster_list;
    [SerializeField] string NextLevel;
    [SerializeField] string CurrentLevel;
    private void OnEnable()
    {
        monster_list = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (allEnemeiesDead())
        {
            StartCoroutine(loadNexeLevel());

        }

    }
    bool allEnemeiesDead()
    {
        foreach (var monster in monster_list)
        {
            if (!monster.isDead)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator loadNexeLevel()
    {
        StaticScene.setNextLevel(NextLevel);
        StaticScene.setCurrentLevel(CurrentLevel);
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene("Level");
    }
}
