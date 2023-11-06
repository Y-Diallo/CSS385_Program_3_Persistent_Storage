using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void OnPlayButton(){
        SceneManager.LoadScene(1);
    }
    public void OnNewGameButton(){
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(1);
    }
    public void OnExitButton(){
        Application.Quit();
    }
}
