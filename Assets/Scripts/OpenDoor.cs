using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && PickUp.hasKey)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.openDoor);
            animator.SetTrigger("openDoor");
        }
    }
}
