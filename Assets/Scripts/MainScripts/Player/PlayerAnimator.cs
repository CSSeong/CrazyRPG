using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Movement2D movement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<Movement2D>();
    }

    public void UpdateAnimation(float x)
    {
        
        animator.SetBool("isJump", !movement.IsGrounded);

        if(movement.IsGrounded)
        {
            animator.SetFloat("velocityX", Mathf.Abs(x));
        }
        else
        {
            animator.SetFloat("velocityY", 1);
        }
    }
}
