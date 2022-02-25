using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject character;
    public GameObject carpalGrenade;
    public GameObject cannonGrenade;
    public Transform arm1;
    public Transform arm2;
    public Transform melee;
    public LayerMask pickUpMask;
    public Vector3 Direction { get; set; }
    private float speed = 20f;



    // public ProjectileBehaviour LaunchProjectilePrefab;
    //public Transform LaunchOffset;


    //Objects for arm1 and arm2
    public GameObject item1;
    public GameObject item2;
    private GameObject temp;

    //Flags to indicate if arm slot is full, left is arm1, right is arm2
    public static bool left;
    public static bool right;
    bool flip;

    void Update()
    {
        //Call Pick up item on user input
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            if (!left || !right)
            {
                if (character.GetComponent<PlayerController>().m_FacingRight == false)
                {
                    flip = true;

                }
                else
                {
                    flip = false;
                }
                pickUpItem();
            }
        }

        //Call throw item upon user input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            checkThrow();
        }

        //Call swap upon user input
        if (Input.GetMouseButtonDown(1))
        {
            swapArms();
        }
    }
    //Pick up an item
    void pickUpItem()
    {
        Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .3f, pickUpMask);

        //If player is facing left rotate arm 180 before picking up
        if (!flip && pickUpItem.gameObject.tag == "Melee")
        {
            pickUpItem.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //If player is facing left rotate arm 180 before picking up
        if (flip && pickUpItem.gameObject.tag != "Melee")
        {
            pickUpItem.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //If no arm on left pick up left arm
        if (!left)
        {
            if (pickUpItem.gameObject.transform.position != arm2.position)
            {
                if (item1 == null)
                {
                    item1 = pickUpItem.gameObject;
                    item1.GetComponent<Weapon>().holding = 1;
                    item1.GetComponent<Weapon>().pickedUp = 1;
                    item1.transform.position = arm1.position;
                    item1.transform.parent = transform;
                    if (item1.GetComponent<Rigidbody2D>())
                        item1.GetComponent<Rigidbody2D>().simulated = false;
                    left = true;
                }
                else
                {
                    item2 = pickUpItem.gameObject;
                    item2.GetComponent<Weapon>().holding = 1;
                    item1.GetComponent<Weapon>().pickedUp = 1;
                    item2.transform.position = arm1.position;
                    item2.transform.parent = transform;
                    if (item2.GetComponent<Rigidbody2D>())
                        item2.GetComponent<Rigidbody2D>().simulated = false;
                    left = true;
                }
            }
        }
        //If no arm on right pick up right arm
        else if (!right)
        {
            if (pickUpItem.gameObject.transform.position != arm1.position)
            {
                if (item2 == null)
                {
                    item2 = pickUpItem.gameObject;
                    item2.GetComponent<Weapon>().pickedUp = 1;
                    item2.transform.position = arm2.position;
                    item2.transform.parent = transform;
                    if (item2.GetComponent<Rigidbody2D>())
                        item2.GetComponent<Rigidbody2D>().simulated = false;
                    right = true;
                }
                else
                {
                    item1 = pickUpItem.gameObject;
                    item1.GetComponent<Weapon>().pickedUp = 1;
                    item1.transform.position = arm2.position;
                    item1.transform.parent = transform;
                    if (item1.GetComponent<Rigidbody2D>())
                        item1.GetComponent<Rigidbody2D>().simulated = false;
                    right = true;
                }
            }
        }
    }

    //Check which arm to throw
    void checkThrow()
    {
        //If holding 2 arms or just the lest arm, Throw left arm
        if ((left && right) || (left && !right))

        {
            if (item1 != null && item1.transform.position == arm1.position)
            {
                throwArm(item1, 1);
            }
            else
            {
                throwArm(item2, 1);
            }
        }

        else if (!left && right)
        {
            if (item1 != null && item1.transform.position == arm2.position)
            {
                throwArm(item1, 2);
            }
            else
            {
                throwArm(item2, 2);
            }
        }
    }

    //Throw specified arm and destroy game object
    void throwArm(GameObject item, int spot)
    {
        if (item.tag == "Carpal")
        {
            if (spot == 1)
                Instantiate(carpalGrenade, arm1.position, arm1.rotation);
            else
                Instantiate(carpalGrenade, arm2.position, arm2.rotation);
        }

        if (item.tag == "Cannon")
        {
            Debug.Log("cannon");
            if (spot == 1)
                Instantiate(cannonGrenade, arm1.position, arm1.rotation);
            else
                Instantiate(cannonGrenade, arm2.position, arm2.rotation);
        }

        item.GetComponent<Weapon>().holding = 0;
        item.transform.parent = null;
        Destroy(item);
        item = null;

        if (spot == 1)
        {
            left = false;
        }
        else
        {
            right = false;
        }
    }

    //Swap arms
    void swapArms()

    {
        //If only one arm being held
        if ((item1 == null) && (item2 != null))
        {
            if (item2.transform.position == arm2.position)
            {
                item2.transform.position = arm1.position;
                item2.GetComponent<Weapon>().holding = 1;
                left = true;
                right = false;
            }
            else
            {
                item2.transform.position = arm2.position;
                item2.GetComponent<Weapon>().holding = 0;
                right = true;
                left = false;
            }
        }
        else if ((item1 != null) && (item2 == null))
        {
            if (item1.transform.position == arm1.position)
            {
                item1.transform.position = arm2.position;
                item1.GetComponent<Weapon>().holding = 0;
                right = true;
                left = false;
            }
            else
            {
                item1.transform.position = arm1.position;
                item1.GetComponent<Weapon>().holding = 1;
                left = true;
                right = false;
            }
        }
        else
        {
            if (item1.transform.position == arm1.position)
            {
                item1.transform.position = arm2.position;
                item1.GetComponent<Weapon>().holding = 0;
                item2.transform.position = arm1.position;
                item2.GetComponent<Weapon>().holding = 1;
            }
            else
            {
                item1.transform.position = arm1.position;
                item1.GetComponent<Weapon>().holding = 1;
                item2.transform.position = arm2.position;
                item2.GetComponent<Weapon>().holding = 0;
            }
            left = true;
            right = true;
        }
    }
}
