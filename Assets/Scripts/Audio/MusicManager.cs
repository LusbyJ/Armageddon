using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameObject mainAudio;
    public GameObject alertAudio;
    public GameObject character;

    private AudioSource mainSource;
    private AudioSource alertSource;

    public float fadeSpeed=0.1f;
    // Start is called before the first frame update
    void Start()
    {
      alertSource=alertAudio.GetComponent<AudioSource>();
      mainSource=mainAudio.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if (character.GetComponent<PickUp>().item1 == null&&character.GetComponent<PickUp>().item2 == null){
        alertSource.volume=Mathf.Lerp(alertSource.volume,1,fadeSpeed);
        mainSource.volume=Mathf.Lerp(mainSource.volume,0,fadeSpeed);
      }else{
        alertSource.volume=Mathf.Lerp(alertSource.volume,0,fadeSpeed);
        mainSource.volume=Mathf.Lerp(mainSource.volume,1,fadeSpeed);
      }
    }
}
