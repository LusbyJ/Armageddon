using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Follow_player : MonoBehaviour {

    public Transform player;
    public Vector2 minClamp= new Vector2(-999999,-999999);
    public Vector2 maxClamp= new Vector2(999999,999999);
    public float lerpSpeed=0.1f;
    void Start (){
      var position=player.transform.position+new Vector3(0,0,-5);
      position.x=Mathf.Clamp(position.x,minClamp.x,maxClamp.x);
      position.y=Mathf.Clamp(position.y,minClamp.y,maxClamp.y);
      transform.position = position;
    }
    // Update is called once per frame
    void FixedUpdate () {
        var position=player.transform.position+new Vector3(0,0,-5);
        position.x=Mathf.Clamp(position.x,minClamp.x,maxClamp.x);
        position.y=Mathf.Clamp(position.y,minClamp.y,maxClamp.y);
        transform.position = Vector3.Lerp(transform.position,position,lerpSpeed);
    }
}
