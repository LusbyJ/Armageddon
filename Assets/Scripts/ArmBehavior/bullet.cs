using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour { 

    public float speed = 20f;
    public Rigidbody2D rb;

    /* Start is called before the first frame update
     * Shoots bullet
     * */
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Called when the bullet hits somehting 
    void OnTriggerEnter2D()
    {
        /*Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(40);
        }
        */
        //Destroy(gameObject);

    }    
}
