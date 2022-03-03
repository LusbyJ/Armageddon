using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public PlayerController controller;
    public float runSpeed = 40f;
    public Animator animator;

    float horizontalMove = 0f;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed",Mathf.Abs(horizontalMove));
        //Check for jump
        if(Input.GetButtonDown("Jump"))
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.jumping);
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public void OnLand(){
      animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        //Move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
