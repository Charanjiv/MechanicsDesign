using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpFunctionality : MonoBehaviour
{
    private Rigidbody2D m_RB;
    private GroundedScript m_GroundedScipt;


    [Header("Jumping")]
    [SerializeField] private float m_fJumpPower = 10.0f;
    [SerializeField] private float m_CoyoteDuration = 0.33f;
    private float m_CoyoteTimer;
    [SerializeField] private float m_JumpBufferDuration = 0.33f;
    private float m_JumpBufferTimer;
    [SerializeField] private float m_JumpSkipGroundCheckDuration = 0.5f;
    private float m_JumpSkipGroundCheckTimer;
    [SerializeField] private float m_JumpDuration;
    [SerializeField] private float m_JumpTimer;
    [SerializeField] private AnimationCurve m_AnalogueJumpUpForce;
    private bool m_Grounded;


    
 

    private void Awake()
    {
        m_RB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_Grounded = false;
        if (m_JumpSkipGroundCheckTimer > 0f)
        {
            m_JumpSkipGroundCheckTimer -= Time.deltaTime;
        }
        else
        {
            if (m_CoyoteTimer > 0f && m_JumpBufferDuration > 0f)
            {
                m_RB.velocity = new Vector2(m_RB.velocity.x, 0f);
                m_JumpSkipGroundCheckTimer = m_JumpSkipGroundCheckDuration;
                m_JumpTimer = m_JumpDuration;
                m_Grounded = false;
                m_JumpBufferTimer = 0f;
            }
            if (m_JumpTimer > 0f)
            {
                m_JumpTimer -= Time.fixedDeltaTime;

            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //COYOTE TIME
        if(!m_Grounded)
        {
            if(m_CoyoteTimer > 0f)
            {
                m_CoyoteTimer -= Time.deltaTime;
            }
        }
        else
        {
            if(m_CoyoteTimer != m_CoyoteDuration)
            {
                m_CoyoteTimer = m_CoyoteDuration;
            }
            if(m_JumpTimer != 0f)
            {
                m_JumpTimer = 0f;
            }
        }

        //JUMP BUFFERING
        if(context.performed)
        {
            m_JumpBufferTimer = m_JumpBufferDuration;
        }
        else if(m_JumpBufferTimer > 0f)
        {
            m_JumpBufferTimer -= Time.deltaTime;
        }



        //JUMPING
        if(m_JumpTimer > 0f)
        {
            m_JumpTimer -= Time.deltaTime;
            if(context.performed)
            {
                m_RB.AddForce(Vector2.up * m_fJumpPower * m_AnalogueJumpUpForce.Evaluate(m_JumpTimer / m_JumpDuration),ForceMode2D.Force);
            }
        }



       
       /* if (context.performed)
        {

            m_RB.velocity = new Vector2(m_RB.velocity.x, m_fJumpPower);
            m_GroundedScipt.m_bGrounded = false;

            Debug.Log("Performe normal jump");

        }
        else if (context.canceled)
        {

            m_RB.velocity = new Vector2(m_RB.velocity.x, m_fJumpPower * 0.5f);
            m_GroundedScipt.m_bGrounded = false;
            Debug.Log("Performe half jump");
        }*/

    }
}
