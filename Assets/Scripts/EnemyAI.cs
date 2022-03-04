using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Vector3 distFromTarget;
    public bool attack;
    public bool meleeEnemy;
    public bool flying = false;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public GameObject stopwatch;
    public float waitTime = 3f;
    public float currentTime;

    Path path;
    int currentWaypoint = 0;

    [SerializeField] private float jumpForce = 700f;

    public Vector2 direction;
    public float angle = .4f;
    [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.
    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool grounded;            // Whether or not the player is grounded.
    public bool facingRight = true;

    public UnityEvent OnLandEvent;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        attack = false;

        if (GetComponentInChildren<MeleeAttack>())
            meleeEnemy = true;
        else
            meleeEnemy = false;

        InvokeRepeating("UpdatePath", 0f, .5f);
        InvokeRepeating("CheckTargetDir", 0f, .5f);
        InvokeRepeating("Jump", 0f, .5f);
        InvokeRepeating("CheckDir", 0f, .5f);
    }


    // This Checks to see which direction the player is in relation to the enemy
    // so that the enemy can stop before just ramming into the player
    void CheckTargetDir()
    {
        if (meleeEnemy)
            attack = GetComponentInChildren<MeleeAttack>().attack;

        if (!meleeEnemy)
        {
            if (target.position.x >= rb.position.x)
                distFromTarget = Vector3.right * 4;
            else if (target.position.x < rb.position.x)
                distFromTarget = Vector3.left * 4;
            if (flying)
                distFromTarget.y = -2;
            Debug.Log(distFromTarget);
             
        }
        else if (attack && meleeEnemy)
        {
            currentTime = stopwatch.GetComponent<DigitalClock>().currentTime;
            if (target.position.x >= rb.position.x)
                distFromTarget = Vector3.right * 2;
            else if (target.position.x < rb.position.x)
                distFromTarget = Vector3.left * 2;
            if (meleeEnemy)
                GetComponentInChildren<MeleeAttack>().attack = false;
        }
        else if (stopwatch.GetComponent<DigitalClock>().currentTime >= currentTime + waitTime)
        {
            distFromTarget = Vector3.zero;
        }
    }


    // This checks the direction that the enemy is traveling in and flips him accordingly.
    void CheckDir()
    {
        if (target.position.x > rb.position.x && !facingRight)
            Flip();
        else if (target.position.x < rb.position.x && facingRight)
            Flip();
    }



    // Checks whether the enemy needs to jumo or not.
    void Jump()
    {
        if(groundCheck != null)
        {
            bool wasGrounded = grounded;
            grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (grounded && other.gameObject.tag != "Player" 
            && other.gameObject.layer != 3 && other.gameObject.layer != 2)
        {
            grounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }



    // Updates the path the enemy is traveling on. Will attempt to follow the player no matter where they are on the map.
    // the distFromTarget is updated based on which side the player is on in relation to the enemy.
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position - distFromTarget, OnPathComplete);

    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    // Flips the enemy around so that enemy is not walking backwards.
    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;


        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
