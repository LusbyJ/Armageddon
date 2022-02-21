using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DigitalClock : MonoBehaviour
{
    bool gameover = false;
    private Text textClock;
    float currentTime;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        textClock = GetComponent<Text>();
        currentTime = 0;

        InvokeRepeating("CheckGameover", 0f, .5f);
    }

    void CheckGameover()
    {
        if (player.GetComponent<Health>().health == 0) 
            gameover = true;
    }

    // Update is called once per frame
    void Update()
    {
       if(gameover == false)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        textClock.text = time.ToString(@"hh\:mm\:ss");

        
    }

    
}
