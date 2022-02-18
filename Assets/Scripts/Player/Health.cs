using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        if(PickUp.left)
        {
            Weapon item = GetComponent<PickUp>().item1.GetComponent<Weapon>();
            item.integrity -= damage;
            if(item.integrity <= 0)
            {
                Destroy(GetComponent<PickUp>().item1);
                GetComponent<PickUp>().item1.GetComponent<Weapon>().holding = 0;
                GetComponent<PickUp>().item1.transform.parent = null;
                GetComponent<PickUp>().item1 = null;
                PickUp.left = false;
            }
            Debug.Log("Arm1 integrity =" + item.integrity);
        }
        else if(!PickUp.left && PickUp.right)
        {
            Weapon item2 = GetComponent<PickUp>().item2.GetComponent<Weapon>();
            item2.integrity -= damage;
            if(item2.integrity <= 0)
            {
                Destroy(GetComponent<PickUp>().item2);
                GetComponent<PickUp>().item2.GetComponent<Weapon>().holding = 0;
                GetComponent<PickUp>().item2.transform.parent = null;
                GetComponent<PickUp>().item2 = null;
                PickUp.right = false;
            }
            Debug.Log("Arm1 integrity =" + item2.integrity);


        }
        else
        {
            health -= 1;
             Debug.Log("Players health = " + health);
            if(health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
