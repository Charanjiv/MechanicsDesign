using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
//if add to body without rigidbody, can't remove rb if script is attached
public class GroundedMovementScript : MonoBehaviour
{
    [SerializeField] private float m_fMoveStrength;
    [SerializeField] private AnimationCurve m_StrengthLUT; //store a curve, nothing to do with animation
    [SerializeField] private Vector2 m_SpeedLimits; //Vector can instead be used as 2 floats, it saves memory 

    [SerializeField] private Rigidbody2D m_RB;



    //[SerializeField] Transform groundCheckPos;
    //[SerializeField] Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    //[SerializeField] LayerMask groundLayer;

    [SerializeField] private float jumpPower = 10.0f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private int jumpsRemaining;


    private float m_fRequestedDir; //looks for direction

    private void Awake()
    {
        m_RB = GetComponent<Rigidbody2D>();
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
        //m_RB.velocity = (m_RB.velocity.magnitude > m_SpeedLimits.y) ? m_RB.velocity.normalized * m_SpeedLimits.y

        if(m_RB.velocity.magnitude > m_SpeedLimits.y)
        {
            m_RB.velocity = m_RB.velocity.normalized * m_SpeedLimits.y;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {

       /* if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                m_RB.velocity = new Vector2(m_RB.velocity.x, jumpPower);
                jumpsRemaining--;
            }
            else if (context.canceled)
            {
                m_RB.velocity = new Vector2(m_RB.velocity.x, m_RB.velocity.y * 0.5f);
                jumpsRemaining--;
            }
       }*/
    }




      
 }
