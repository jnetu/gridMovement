using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuLoadScene : MonoBehaviour
{
    
    public float number;
    public MenuLoadScene(string scene){
        Debug.Log("carregado= "+scene);
        if(scene == "Start"){
        SceneManager.LoadScene("level1");

        }
    }
}
