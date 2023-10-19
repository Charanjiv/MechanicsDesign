using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;





public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody2D rb;


    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5.0f;
    float horizontalMovement;


    [Header("Jumping")]
    [SerializeField] private float jumpPower = 10.0f;
    [SerializeField] private int maxJumps = 2;
    private int jumpsRemaining;


    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;


    [Header("Gravity")]
    [SerializeField] private float baseGravity = 2.0f;
    [SerializeField] private float maxFallSpeed = 18.0f;
    [SerializeField] private float fallSpeedMultiplier = 2.0f;



    void Update()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        GroundCheck();
        Gravity();
    }


    //Player Movement - left and right
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    //Player jump and double jump
    public void jump(InputAction.CallbackContext context)
    {
        
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpsRemaining--;   
            }
            else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsRemaining--;
            }
        }
    }

    //Grounded check
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
    public void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
        
    }

    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //fall increasingly faster
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }



}
