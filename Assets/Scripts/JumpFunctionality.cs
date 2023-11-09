using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpFunctionality : MonoBehaviour
{
    private Rigidbody2D m_RB;
    private GroundedScript m_GroundedScipt;


    [Header("Jumping")]
    [SerializeField] private float m_fJumpPower = 10.0f;
    public bool m_IsJumping;
    public float LastOnGroundTime;
    public float jumpForce;

    //Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;
    public float coyoteTime;
    public float jumpInputBufferTime;
    private float LastPressedJumpTime;
    
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        m_RB = GetComponent<Rigidbody2D>();
        //m_Grounded = true;
        m_GroundedScipt = GetComponent<GroundedScript>();
    }


    private void Update()
    {

        LastOnGroundTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            OnJumpInput();
        }

        if (Input.GetButtonUp("Jump"))
        {
            OnJumpUpInput();
        }

        //Collision
        if (!m_IsJumping)
        {
            //Ground Check
            //if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !m_IsJumping) //checks if set box overlaps with ground
            if(m_GroundedScipt.IsGrounded() && !m_IsJumping)
            {
              LastOnGroundTime = coyoteTime; //if so sets the lastGrounded to coyoteTime
            }

        }
        //Jump Checks
        if (m_IsJumping && m_RB.velocity.y < 0)
        {
            m_IsJumping = false;
        }
        if (LastOnGroundTime > 0 && !m_IsJumping)
        {
            _isJumpCut = false;

            if (!m_IsJumping)
                _isJumpFalling = false;
        }
        //Jump
        if (CanJump() && LastPressedJumpTime > 0)
        {
            m_IsJumping = true;
            _isJumpCut = false;
            _isJumpFalling = false;
            Jump();
        }

    }




    public void OnJumpInput()
    {
        LastPressedJumpTime = jumpInputBufferTime;
    }

    public void OnJumpUpInput()
    {
        if (CanJumpCut())
            _isJumpCut = true;

    }

    public void Jump()
    {

        //Stops double jump
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;


        float force = jumpForce;
        if (m_RB.velocity.y < 0)
            force -= m_RB.velocity.y;

        m_RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        
    }

    private bool CanJump()
    {
        return LastOnGroundTime > 0 && !m_IsJumping;
    }

    private bool CanJumpCut()
    {
        return m_IsJumping && m_RB.velocity.y > 0;
    }

}

