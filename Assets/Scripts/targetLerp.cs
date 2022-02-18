using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLerp : MonoBehaviour
{
    public Vector2 target;
    public float lerpval=0.1f;
    // Start is called before the first frame update
    void Start()
    {
        target=transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition=Vector2.Lerp(transform.localPosition,target,lerpval);
    }
}
