using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera camera;
    public Transform player;
    Vector2 startPosition;
    float startZ;

    Vector2 travel => (Vector2)camera.transform.position- startPosition;
    //find the difference between the player's position and the parallax position
    float differenceFromSubject => transform.position.z- player.position.z;
    //determine the clipping plane that is being used
    float clippingPlane => (camera.transform.position.z + (differenceFromSubject>0?camera.farClipPlane:camera.nearClipPlane));
    float parallaxFactor => Mathf.Abs(differenceFromSubject)/clippingPlane;


    public void Start(){
      startPosition=transform.position;
      startZ=transform.position.z;
    }

    public void Update(){
      Vector2 newPos=startPosition+travel*parallaxFactor;
      transform.position=new Vector3(newPos.x,newPos.y,startZ);
    }

}
