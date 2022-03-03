using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoLevel : MonoBehaviour
{
    public string Level;
    public void go(){
      SceneManager.LoadScene(Level);
    }
}
