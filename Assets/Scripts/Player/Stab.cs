using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;

    //if enemy hit cause damage
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag != "Player" && GetComponent<Weapon>().attacking)
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
