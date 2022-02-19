using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string playScene;
    public void startGame(){
      SceneManager.LoadScene(playScene);
    }
    public void endGame(){
      Application.Quit();
    }
}
