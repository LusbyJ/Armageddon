using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    //Core health
    public int health = 3;
    private bool hit;

    public void TakeDamage(int damage)
    {
        if(hit == false)
        {
            StartCoroutine("Blink");


            //Left and right arms
            GameObject item = GetComponent<PickUp>().item1;
            GameObject item2 = GetComponent<PickUp>().item2;

            if (PickUp.hasKey)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.playerDamage);
                item.GetComponent<Weapon>().integrity -= damage;
                if (item.GetComponent<Weapon>().integrity <= 0)
                {
                    Destroy(item);
                    SceneManager.LoadScene("GameOver");
                    Weapon.holding1 = 0;
                    item.transform.parent = null;
                    item = null;
                    PickUp.left = false;
                    PickUp.hasKey = false;
                }
            }

            //If left arm last used, take damage to left arm
            else if (Weapon.holding1 == 1 && item != null)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.playerDamage);
                item.GetComponent<Weapon>().integrity -= damage;
                if (item.GetComponent<Weapon>().integrity <= 0)
                {
                    Destroy(item);
                    Weapon.holding1 = 0;
                    item.transform.parent = null;
                    item = null;
                    PickUp.left = false;
                }
            }

            //If right arm last used, take damage to the right arm
            else if (Weapon.holding2 == 1 && item2 != null)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.playerDamage);
                item2.GetComponent<Weapon>().integrity -= damage;
                if (item2.GetComponent<Weapon>().integrity <= 0)
                {
                    Destroy(item2);
                    Weapon.holding2 = 0;
                    item2.transform.parent = null;
                    item2 = null;
                    PickUp.right = false;
                }
            }

            //If no arm last used and arm present on left, take damage to left arm
            else if (Weapon.holding1 == 0 && item != null)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.playerDamage);
                item.GetComponent<Weapon>().integrity -= damage;
                if (item.GetComponent<Weapon>().integrity <= 0)
                {
                    Destroy(item);
                    Weapon.holding1 = 0;
                    item.transform.parent = null;
                    item = null;

                }
            }

            //If no arm last used and arm present on right but no left, take damage to right arm
            else if(Weapon.holding2 == 0 && item2 != null)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.playerDamage);
                item2.GetComponent<Weapon>().integrity -= damage;
                if (item2.GetComponent<Weapon>().integrity <= 0)
                {
                    Destroy(item2);
                    Weapon.holding2 = 0;
                    item2.transform.parent = null;
                    item2 = null;
                    PickUp.right = false;
                }
            }

            //If no arms present, take core damage
            else
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.coreDamage);
                health -= 1;
                if (health <= 0)
                {
                    Die();
                }
            }
        }
    }
    
    //Flash once when player gets hit
    private IEnumerator Blink()
    {
        hit = true;

        for(int i = 0; i < 5; i++)
        {
            GetComponent<Renderer>().material.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(0.1f);    
        }
        hit = false;
        StopCoroutine("Blink");  
    }

    //Call game over scene
    void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}