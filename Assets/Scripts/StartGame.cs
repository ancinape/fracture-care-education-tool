using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame: MonoBehaviour {
    public void StartFractureCare() {
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();  
    }  
}  