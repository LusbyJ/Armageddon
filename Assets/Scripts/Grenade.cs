using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private float speed = 5;
    public int damage;
    public Rigidbody2D rb;
    public float splashRange = 1;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Character").GetComponent<PlayerController>().m_FacingRight == true)
        {
            rb.velocity = new Vector2(4f, 3f) * speed;
        }
        else
        {
            rb.velocity = new Vector2(-4f, 3f) * speed;
        }
    }



    //If bullet hits object destroy it, if enemy hit cause damage
    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.tag != "Player" && hitInfo.gameObject.layer != 3)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.armBlowUp);
            if (splashRange > 0)
            {
                var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
                foreach (var hitCollider in hitColliders)
                {
                    Enemy enemy = hitCollider.GetComponent<Enemy>();
                    if (enemy)
                    {
                        var closestPoint = hitCollider.ClosestPoint(transform.position);
                        var distance = Vector3.Distance(closestPoint, transform.position);

                        var damagePercent = Mathf.InverseLerp(splashRange, 0, distance);
                        enemy.TakeDamage(damage);
                        
                    }
                }
            }
            else
            {
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.TakeDamage(damage);
                }
                
            }
            if (gameObject.name != "newKey")
            {
                Destroy(gameObject);
            }
        }
    }

}