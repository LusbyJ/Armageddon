using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{



    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ran into door");
        if(collision.gameObject.tag == "Player" && PickUp.hasKey)
        {
            Debug.Log("Open door");
            SceneManager.LoadScene("Victory");
        }
    }
}
