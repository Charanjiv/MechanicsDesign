using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody2D m_RB;
    private JumpFunctionality m_JumpScript;


    [Header("Gravity")]
    [HideInInspector] public float gravityStrength; 

    [HideInInspector] public float gravityScale; 
                                                
    [Space(5)]
    public float fallGravityMult; //Multiplier to the player's gravityScale when falling.
    public float maxFallSpeed;
    [Space(5)]
    public float fastFallGravityMult; //Larger multiplier to the player's gravityScale when they are falling and a downwards input is pressed.
                                      
    public float maxFastFallSpeed;

    public float jumpCutGravityMult; 
    [Range(0f, 1)] public float jumpHangGravityMult; 
    public float jumpHangTimeThreshold; 
    [Space(0.5f)]
    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;


    private bool _isJumpCut;
    private bool _isJumpFalling;
    public void SetGravity()
        {

            if (m_RB.velocity.y < 0)
        {
            //Much higher gravity if holding down
            SetGravityScale(gravityScale * fastFallGravityMult);
            //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
            m_RB.velocity = new Vector2(m_RB.velocity.x, Mathf.Max(m_RB.velocity.y, -maxFastFallSpeed));
        }
        else if (_isJumpCut)
        {
            //Higher gravity if jump button released
            SetGravityScale(gravityScale * jumpCutGravityMult);
            m_RB.velocity = new Vector2(m_RB.velocity.x, Mathf.Max(m_RB.velocity.y, -maxFallSpeed));
        }
        else if ((m_JumpScript.m_IsJumping || _isJumpFalling) && Mathf.Abs(m_RB.velocity.y) < jumpHangTimeThreshold)
        {
            SetGravityScale(gravityScale * jumpHangGravityMult);
        }
        else if (m_RB.velocity.y < 0)
        {
            //Higher gravity if falling
            SetGravityScale(gravityScale * fallGravityMult);
            //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
            m_RB.velocity = new Vector2(m_RB.velocity.x, Mathf.Max(m_RB.velocity.y, -maxFallSpeed));
        }
        else
        {
            //Default gravity if standing on a platform or moving upwards
            SetGravityScale(gravityScale);
        }
    }

    public void SetGravityScale(float scale)
    {
        m_RB.gravityScale = scale;
    }
}
