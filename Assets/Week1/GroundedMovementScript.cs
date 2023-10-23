using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody2D))]
//if add to body without rigidbody, can't remove rb if script is attached
public class GroundedMovementScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_RB;


    [Header("Movement")]
    [SerializeField] private float m_fMoveStrength;
    [SerializeField] private AnimationCurve m_StrengthLUT; //store a curve, nothing to do with animation
    [SerializeField] private Vector2 m_SpeedLimits; //Vector can instead be used as 2 floats, it saves memory 

    

    [Header("Jumping")]
    [SerializeField] private float m_fJumpPower = 10.0f;
    [SerializeField] private int maxJumps = 2;
    private int jumpsRemaining;


    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    bool m_bIsJumping;

    [Header("Gravity")]
    [SerializeField] private float baseGravity = 2.0f;
    [SerializeField] float maxFallSpeed = 18.0f;
    [SerializeField] private float fallSpeedMultiplier = 2.0f;

    private float m_fRequestedDir; //looks for direction

    private void Awake()
    {
        m_RB = GetComponent<Rigidbody2D>();
        Gravity();

    }
    public void AddMovementInput(float inMov)
    {
        if(m_fRequestedDir != inMov)
        {
            m_fRequestedDir = inMov;
        }
        
    }

    private void FixedUpdate()
    {
        m_RB.AddForce(Vector2.right * m_fRequestedDir * m_fMoveStrength, ForceMode2D.Force);
        

        if(m_RB.velocity.magnitude > m_SpeedLimits.y)
        {
            m_RB.velocity = m_RB.velocity.normalized * m_SpeedLimits.y;
        }
        Gravity();
        GroundCheck();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //jumpsRemaining = maxJumps;
        Debug.Log("Performe");
        //m_RB.AddForce(new Vector2(m_RB.velocity.x, m_fJumpPower));
         if (jumpsRemaining > 0)
         {
             if (context.performed)
             {
                 m_RB.velocity = new Vector2(m_RB.velocity.x, m_fJumpPower);
                 
                 Debug.Log("Performe");
                 jumpsRemaining--;
             }
             else if (context.canceled)
             {
              
                m_RB.velocity = new Vector2(m_RB.velocity.x, m_fJumpPower * 0.5f);

                jumpsRemaining--;
             }
         }

    }

    private void Gravity()
    {
        if (m_RB.velocity.y < 0)
        {
            m_RB.gravityScale = baseGravity * fallSpeedMultiplier; //fall increasingly faster
            m_RB.velocity = new Vector2(m_RB.velocity.x, Mathf.Max(m_RB.velocity.y, -maxFallSpeed));
        }
        else
        {
            m_RB.gravityScale = baseGravity;
        }
    }

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
    





}
