using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{ 

    public int damage = 1;
    public LayerMask playerMask;
    public Vector3 Direction { get; set; }

    bool executed = true;

    public Transform target;
    
    private IEnumerator Attack()
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
                //Write some code so that the player is knockedback to the right.
            }
            else(target.position.x < transform.position.x)
            {
                //Write some code so that the player is knockedback to the left.
            }
            
        }
        yield return new WaitForSeconds(0);
        executed = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(executed) StartCoroutine("Attack");
    }

}
