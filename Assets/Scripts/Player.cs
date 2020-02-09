using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private CapsuleCollider2D feetCollider;
    private BoxCollider2D bodyCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        feetCollider = GetComponent<CapsuleCollider2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
    }

    
    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Jump();
        Hurt();
    }

    private void Run()
    {
        float inputValue = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(inputValue * runSpeed,playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;

        bool isPlayerMoving = Mathf.Abs(playerRigidBody.velocity.x) > 0;
        playerAnimator.SetBool("isWalking",isPlayerMoving);
    }

    private void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerAnimator.SetBool("isJumping",false);
            return;
        } 
        
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            playerRigidBody.velocity += jumpVelocity;
            playerAnimator.SetBool("isJumping",true);
        } 


    }

    private void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(playerRigidBody.velocity.x) > 0;

        if (isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x),1f);
        }
    }

    private void Hurt()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
        //    FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}