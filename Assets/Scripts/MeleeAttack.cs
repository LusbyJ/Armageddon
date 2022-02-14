using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public int damage = 1;
    public LayerMask playerMask;
    public Vector3 Direction { get; set; }

    bool right = true;
    bool executed = true;
    
    private IEnumerator Attack()
    {
        Collider2D attack = Physics2D.OverlapCircle(transform.position + Direction, .3f, playerMask);
        if (attack.IsTouchingLayers(playerMask))
        {

        //    if (EnemyAI.instance.facingRight)
        //       player.position += Velocity2.right * 3;
        //    else
        //        player.position += Velocity2.left * 3;
            Health health = attack.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("Player lost " + damage);
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
