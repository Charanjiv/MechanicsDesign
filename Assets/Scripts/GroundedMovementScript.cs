using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody2D))]
//if add to body without rigidbody, can't remove rb if script is attached
public class GroundedMovementScript : MonoBehaviour


{

    //attached components
    private GroundedScript m_GroundedScipt;

    //private 

    [SerializeField] private Rigidbody2D m_RB;


    [Header("Movement")]
    [SerializeField] private float m_fMoveStrength;
    [SerializeField] private AnimationCurve m_StrengthLUT; //store a curve, nothing to do with animation
    [SerializeField] private Vector2 m_SpeedLimits; //Vector can instead be used as 2 floats, it saves memory 

    [Header("Jumping")]
    [SerializeField] private float m_fJumpPower = 10.0f;
    /*[SerializeField] private float m_CoyoteDuration = 0.33f;
    private float m_CoyoteTimer;
    [SerializeField] private float m_JumpBufferDuration = 0.33f;
    private float m_JumpBufferTimer;
    [SerializeField] private float m_JumpSkipGroundCheckDuration = 0.5f;
    private float m_JumpSkipGroundCheckTimer;
    [SerializeField] private float m_JumpDuration;
    [SerializeField] private float m_JumpTimer;
    [SerializeField] private AnimationCurve m_AnalogueJumpUpForce;
    private bool m_Grounded;
    */
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Gravity")]
    [SerializeField] private float baseGravity = 2.0f;
    [SerializeField] private float maxFallSpeed = 18.0f;
    [SerializeField] private float fallSpeedMultiplier = 2.0f;

    //Timers (also all fields, could be private and a method returning a bool could be used)
    public float LastOnGroundTime { get; private set; }
    public float LastOnWallTime { get; private set; }
    public float LastOnWallRightTime { get; private set; }
    public float LastOnWallLeftTime { get; private set; }




    public PlayerData Data;
    //Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;
    private float m_fRequestedDir; //looks for direction
                                   //Set all of these up in the inspector
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    //Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);


    private void Awake()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_GroundedScipt = GetComponent<GroundedScript>();
        m_GroundedScipt.CheckGround();

    }
    public void AddMovementInput(float inMov)
    {
        if (m_fRequestedDir != inMov)
        {
            m_fRequestedDir = inMov;
        }

    }



        private void FixedUpdate()
    {

        m_RB.AddForce(Vector2.right * m_fRequestedDir * m_fMoveStrength, ForceMode2D.Force);


        if (m_RB.velocity.magnitude > m_SpeedLimits.y)
        {
            m_RB.velocity = m_RB.velocity.normalized * m_SpeedLimits.y;
        }
        Gravity();
        //GroundCheck();

        /*
        //COYOTE TIME
        if (!m_Grounded)
        {
            if (m_CoyoteTimer > 0f)
            {
                m_CoyoteTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (m_CoyoteTimer != m_CoyoteDuration)
            {
                m_CoyoteTimer = m_CoyoteDuration;
            }
            if (m_JumpTimer != 0f)
            {
                m_JumpTimer = 0f;
            }
        }

        //JUMP BUFFERING
        if (Input.GetButtonDown("Jump"))
        {
            m_JumpBufferTimer = m_JumpBufferDuration;
        }
        else if (m_JumpBufferTimer > 0f)
        {
            m_JumpBufferTimer -= Time.deltaTime;
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

    public void Jump(InputAction.CallbackContext context)
    {
        /*
        //m_RB.velocity = new Vector2(m_RB.velocity.x, m_fJumpPower);
        //JUMPING
        if (m_JumpTimer > 0f)
        {
            m_JumpTimer -= Time.deltaTime;
            if (context.performed)
            {
                Debug.Log("Performe normal jump");
                m_RB.AddForce(Vector2.up * m_fJumpPower * m_AnalogueJumpUpForce.Evaluate(m_JumpTimer / m_JumpDuration), ForceMode2D.Force);
            }
        }*/
       
            if (context.canceled && IsGrounded())
            {
                m_RB.velocity = new Vector2(m_RB.velocity.x, m_fJumpPower);
            }
            if (context.performed && IsGrounded()) //&& m_RB.velocity.y > 0f
            {
            m_RB.velocity = new Vector2(m_RB.velocity.x, m_fJumpPower * 0.5f);
            }
   
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }*/
    /*public void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer))
        {
            
        }

    }*/


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }



   /* public void Jump()
    {
        if (!IsJumping)
        {
            //Ground Check
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping) //checks if set box overlaps with ground
            {
                LastOnGroundTime = Data.coyoteTime; //if so sets the lastGrounded to coyoteTime
            }

            //Right Wall Check
            if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)
                    || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)) && !IsWallJumping)
                LastOnWallRightTime = Data.coyoteTime;

            //Right Wall Check
            if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)
                || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)) && !IsWallJumping)
                LastOnWallLeftTime = Data.coyoteTime;

            //Two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
            LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
        }
    }*/
}




