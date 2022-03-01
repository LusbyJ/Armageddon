using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    //Core health
    public int health = 3;

    public void TakeDamage(int damage)
    {
        StartCoroutine("Blink");

        //Left and right arms
        GameObject item = GetComponent<PickUp>().item1;
        GameObject item2 = GetComponent<PickUp>().item2;

        if (GetComponent<PickUp>().hasKey)
        {
            item.GetComponent<Weapon>().integrity -= damage;
            if (item.GetComponent<Weapon>().integrity <= 0)
            {
                Destroy(GetComponent<PickUp>().item1);
                Weapon.holding1 = 0;
                GetComponent<PickUp>().item1.transform.parent = null;
                GetComponent<PickUp>().item1 = null;
                PickUp.left = false;
                GetComponent<PickUp>().hasKey = false;
            }
        }

        //If left arm last used, take damage to left arm
        else if (Weapon.holding1 == 1 && item != null)
        {
            item.GetComponent<Weapon>().integrity -= damage;
            if (item.GetComponent<Weapon>().integrity <= 0)
            {
                Destroy(GetComponent<PickUp>().item1);
                Weapon.holding1 = 0;
                GetComponent<PickUp>().item1.transform.parent = null;
                GetComponent<PickUp>().item1 = null;
                PickUp.left = false;
            }
        }

        //If right arm last used, take damage to the right arm
        else if (Weapon.holding2 == 1 && item2 != null)
        {
            item2.GetComponent<Weapon>().integrity -= damage;
            if (item2.GetComponent<Weapon>().integrity <= 0)
            {
                Destroy(GetComponent<PickUp>().item2);
                Weapon.holding2 = 0;
                GetComponent<PickUp>().item2.transform.parent = null;
                GetComponent<PickUp>().item2 = null;
                PickUp.right = false;
            }
        }

        //If no arm last used and arm present on left, take damage to left arm
        else if (Weapon.holding1 == 0 && item != null)
        {
            item.GetComponent<Weapon>().integrity -= damage;
            if (item.GetComponent<Weapon>().integrity <= 0)
            {
                Destroy(GetComponent<PickUp>().item1);
                Weapon.holding1 = 0;
                GetComponent<PickUp>().item1.transform.parent = null;
                GetComponent<PickUp>().item1 = null;

            }
        }

        //If no arm last used and arm present on right but no left, take damage to right arm
        else if(Weapon.holding2 == 0 && item2 != null)
        {
            item2.GetComponent<Weapon>().integrity -= damage;
            if (item2.GetComponent<Weapon>().integrity <= 0)
            {
                Destroy(GetComponent<PickUp>().item2);
                Weapon.holding2 = 0;
                GetComponent<PickUp>().item2.transform.parent = null;
                GetComponent<PickUp>().item2 = null;
                PickUp.right = false;
            }
        }

        //If no arms present, take core damage
        else
        {
            health -= 1;
            Debug.Log("Players health = " + health);
            if (health <= 0)
            {
                Die();
            }
        }
    }
    
    //Flash once when player gets hit
    private IEnumerator Blink()
    {
        GetComponent<Renderer>().material.color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material.color = Color.white;
        StopCoroutine("Blink");  
    }

    //Call game over scene
    void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}