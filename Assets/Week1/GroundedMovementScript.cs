using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
//if add to body without rigidbody, can't remove rb if script is attached
public class GroundedMovementScript : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float m_fMoveStrength;
    [SerializeField] private AnimationCurve m_StrengthLUT; //store a curve, nothing to do with animation
    [SerializeField] private Vector2 m_SpeedLimits; //Vector can instead be used as 2 floats, it saves memory 

    [SerializeField] private Rigidbody2D m_RB;

    [Header("Jumping")]
    public float jumpPower = 10.0f;
    public int maxJumps = 2;
    public int jumpsRemaining;

    [Header("Gravity")]
    [SerializeField] private float baseGravity = 2.0f;
    [SerializeField] float maxFallSpeed = 18.0f;
    [SerializeField] private float fallSpeedMultiplier = 2.0f;

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
        

        if(m_RB.velocity.magnitude > m_SpeedLimits.y)
        {
            m_RB.velocity = m_RB.velocity.normalized * m_SpeedLimits.y;
        }
        Gravity();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        m_RB.AddForce(new Vector2(m_RB.velocity.x, 330f));
       /* if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                m_RB.velocity = new Vector2(m_RB.velocity.x, jumpPower);
                //m_RB.AddForce(new Vector2(m_RB.velocity.x, 300f));
                Debug.Log("Performe");
                jumpsRemaining--;
            }
            else if (context.canceled)
            {
                m_RB.velocity = new Vector2(m_RB.velocity.x, m_RB.velocity.y * 0.5f);
                jumpsRemaining--;
            }
        }*/

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





}
