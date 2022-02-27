using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{ 

    public int damage = 1;

    public bool attack = true;

    //If enemy hit cause damage
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        attack = true;
        Health health = hitInfo.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
               
    }
}