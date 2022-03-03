using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip armPickUp;
    public AudioClip armBlowUp;
    public AudioClip carpalGun;
    public AudioClip cannonGun;
    public AudioClip coreDamage;
    public AudioClip playerDamage;
    public AudioClip openDoor;
    public AudioClip hammer;
    public AudioClip sword;
    public AudioClip jumping;
    public AudioClip enemyBlowUp;
    public AudioClip enemyTakeDamage;
    public static SfxManager sfxInstance;

    private void Awake()
    {
        if(sfxInstance != null && sfxInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sfxInstance = this;
        DontDestroyOnLoad(this);
    }
}
