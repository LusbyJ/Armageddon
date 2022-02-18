using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Follow_player : MonoBehaviour {

    public Transform player;
    public Vector2 minClamp= new Vector2(-999999,-999999);
    public Vector2 maxClamp= new Vector2(999999,999999);
    public float lerpSpeed=0.1f;
    public float mouseBias=10f;
    public float mouseMaxDist=10f;
    public Vector2 screenSize=new Vector2(1024,576);
    void Start (){
      var position=player.transform.position+new Vector3(0,0,-5);
      position.x=Mathf.Clamp(position.x,minClamp.x,maxClamp.x);
      position.y=Mathf.Clamp(position.y,minClamp.y,maxClamp.y);
      transform.position = position;
    }
    // Update is called once per frame
    void FixedUpdate () {
        var mousePos = Input.mousePosition;
        mousePos.x=Mathf.Clamp(mousePos.x,0,Screen.width);
        mousePos.y=Mathf.Clamp(mousePos.y,0,Screen.height);
        var mouseScale=new Vector3((mousePos.x/Screen.width-0.5f)*2,(mousePos.y/Screen.height-0.5f)*2,0);
        var playerPosition=player.transform.position;
        var mousePosition=mouseScale*mouseBias;
        mousePosition=Vector3.ClampMagnitude(mousePosition, mouseMaxDist);
        var position=playerPosition+mousePosition+new Vector3(0,0,-5);
        //position.x=Mathf.Clamp(position.x,minClamp.x,maxClamp.x);
        //position.y=Mathf.Clamp(position.y,minClamp.y,maxClamp.y);
        transform.position = Vector3.Lerp(transform.position,position,lerpSpeed);
    }
}
