using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame: MonoBehaviour {
    public static bool isPreTest = false;

    public void StartPreTest() {
        isPreTest = true;
        SceneManager.LoadScene(1);
    }

    public void StartPostTest() {
        isPreTest = false;
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();  
    }  
}  