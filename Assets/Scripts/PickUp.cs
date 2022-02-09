using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform arm1;
    public Transform arm2;
    public LayerMask pickUpMask;
    public Vector3 Direction { get; set; }
    private Rigidbody2D rigidBody;
    public float projectileSpeed;
   
  

   // public ProjectileBehaviour LaunchProjectilePrefab;
    //public Transform LaunchOffset;
 

    //Objects for arm1 and arm2
    private GameObject item1;
    private GameObject item2;

    //Flags to indicate if arm slot is full, left is arm1, right is arm2
    bool left;
    bool right;

    void Update()
    {
        //Call Pick up item on user input
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            if (!left || !right)
            {
                pickUpItem();
            }
        }

        //Call Drop item upon user input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Instantiate(LaunchProjectilePrefab, LaunchOffset.position, transformm.rotation);
            dropItem();
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
        if (!left)
        {
            if (pickUpItem.gameObject.transform.position != arm2.position)
            {

                item1 = pickUpItem.gameObject;
                item1.GetComponent<Weapon>().holding = 1;
                item1.transform.position = arm1.position;
                item1.transform.parent = transform;
                if (item1.GetComponent<Rigidbody2D>())
                    item1.GetComponent<Rigidbody2D>().simulated = false;
                left = true;
            }
        }
        else
        {
            if (pickUpItem.gameObject.transform.position != arm1.position)
            {
                item2 = pickUpItem.gameObject;
                item2.transform.position = arm2.position;
                item2.transform.parent = transform;
                if (item2.GetComponent<Rigidbody2D>())
                    item2.GetComponent<Rigidbody2D>().simulated = false;
                right = true;
            }
        }
    }

    //Drop an item
    void dropItem()
    {

 
        if (left && right)
        {
            item1.transform.position = transform.position + Direction;
            Debug.Log("Dropped left double");
            if (item1.GetComponent<Rigidbody2D>())
                item1.GetComponent<Rigidbody2D>().simulated = true;
           
            Destroy(item1, 3);
            item1.GetComponent<Weapon>().holding = 0;
            item1.transform.parent = null;
            item1 = null;
            left = false;
        }
        else if (left && !right)
        {
            item1.transform.position = transform.position + Direction;
            Debug.Log("Dropped left single");
            if (item1.GetComponent<Rigidbody2D>())
                item1.GetComponent<Rigidbody2D>().simulated = true;
            Destroy(item1, 3);
            item1.GetComponent<Weapon>().holding = 0;
            item1.transform.parent = null;
            item1 = null;
            left = false;
        }
        else if (!left && right)
        {
            item2.transform.position = transform.position + Direction;
            Debug.Log("Dropped right");
            if (item2.GetComponent<Rigidbody2D>())
                item2.GetComponent<Rigidbody2D>().simulated = true;
            Destroy(item2, 3);
            item2.GetComponent<Weapon>().holding = 0;
            item2.transform.parent = null;
            item2 = null;
            right = false;
        }
    }

    //Swap arms
    void swapArms()
    {
        if (item2.transform.position == arm2.position)
        {
            item2.transform.position = arm1.position;
            item2.GetComponent<Weapon>().holding = 1;
            item1.GetComponent<Weapon>().holding = 0;
            item1.transform.position = arm2.position;
        }
        else
        {
            item1.transform.position = arm1.position;
            item1.GetComponent<Weapon>().holding = 1;
            item2.GetComponent<Weapon>().holding = 0;
            item2.transform.position = arm2.position;
        }
    }
}