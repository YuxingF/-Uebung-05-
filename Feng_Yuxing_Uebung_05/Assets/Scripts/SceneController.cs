using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadSpaceInvasion() 
    {
        SceneManager.LoadScene("SpaceInvasion");
    }
    public void QuitGame(){
        UnityEditor.EditorApplication.isPlaying = false; 
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
     
    }
    
    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }
    public void LoadEnd(){
         SceneManager.LoadScene("End");
    }

}
