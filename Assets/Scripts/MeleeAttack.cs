using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public int damage = 10;
    public LayerMask playerMask;
    public Vector3 Direction { get; set; }

    bool right = true;

    public Transform player;
    

    void attack()
    {
        Collider2D attack = Physics2D.OverlapCircle(transform.position + Direction, .3f, playerMask);
        if (attack.IsTouchingLayers(playerMask))
        {
        //    if (EnemyAI.instance.facingRight)
        //       player.position += Velocity2.right * 3;
        //    else
        //        player.position += Velocity2.left * 3;
            Debug.Log("Player lost " + damage + " health.");
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        attack();
    }

}
