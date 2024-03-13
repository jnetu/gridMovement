using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CuttingGrass : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Grass")
        {
            Destroy(other.gameObject, 0.2f);
            GameController.instance.GrassCountDecrease();
        }
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Grass").Length <= 0)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        Debug.Log("clear! " + SceneManager.GetActiveScene().name);
        string currentLevelName = SceneManager.GetActiveScene().name;
        //format = level1,level2,level3 ...
        //nextlevel = last char + 1
        int next = int.Parse(currentLevelName.Substring(currentLevelName.Length - 1, 1)) + 1;
        Debug.Log("loading... " + next);
        //OBS: add all levels in build settings or its not work... :/ 
        SceneManager.LoadScene("level"+ next);
    }
}
