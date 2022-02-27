using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{ 

    public int damage = 1;

    public bool attack = true;

    /* private IEnumerator Attack()
     {
         Collider2D attack = Physics2D.OverlapCircle(transform.position + Direction, .3f, playerMask);
         if (attack.IsTouchingLayers(playerMask))
         {
             Health health = attack.GetComponent<Health>();
             if(health != null)
             {
                 health.TakeDamage(damage);
                 Debug.Log("Player lost " + damage);
             }
             if(target.position.x > transform.position.x)
             {
                 //Write some code so that the player is knockedback to the right
             }
             else if(target.position.x < transform.position.x)
             {
                 //Write some code so that the player is knockedback to the left.
             }

         }
         yield return new WaitForSeconds(0);
         executed = true;
     }*/
    //If bullet hits object destroy it, if enemy hit cause damage
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        attack = true;
        Health health = hitInfo.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
               
    }

   /* void attack()
    {
        Collider2D attack = Physics2D.OverlapCircle(transform.position + Direction, .3f, playerMask);
        if (attack.IsTouchingLayers(playerMask))
        {
            Health health = attack.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("Player lost " + damage);
            }
            if (target.position.x > transform.position.x)
            {
                //Write some code so that the player is knockedback to the right
            }
            else if (target.position.x < transform.position.x)
            {
                //Write some code so that the player is knockedback to the left.
            }
        }
    }*/
 /*   // Update is called once per frame
    void Update()
    {
        Attack();
        //if(executed) StartCoroutine("Attack");
    }*/

}