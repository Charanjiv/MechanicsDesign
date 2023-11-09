using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody2D m_RB;
    private JumpFunctionality m_JumpScript;


    [Header("Gravity")]
    [HideInInspector] public float gravityStrength; //Downwards force (gravity) needed for the desired jumpHeight and jumpTimeToApex.
    [HideInInspector] public float gravityScale; //Strength of the player's gravity as a multiplier of gravity (set in ProjectSettings/Physics2D).
                                                 //Also the value the player's rigidbody2D.gravityScale is set to.
    [Space(5)]
    public float fallGravityMult; //Multiplier to the player's gravityScale when falling.
    public float maxFallSpeed; //Maximum fall speed (terminal velocity) of the player when falling.
    [Space(5)]
    public float fastFallGravityMult; //Larger multiplier to the player's gravityScale when they are falling and a downwards input is pressed.
                                      //Seen in games such as Celeste, lets the player fall extra fast if they wish.
    public float maxFastFallSpeed;

    public float jumpCutGravityMult; //Multiplier to increase gravity if the player releases thje jump button while still jumping
    [Range(0f, 1)] public float jumpHangGravityMult; //Reduces gravity while close to the apex (desired max height) of the jump
    public float jumpHangTimeThreshold; //Speeds (close to 0) where the player will experience extra "jump hang". The player's velocity.y is closest to 0 at the jump's apex (think of the gradient of a parabola or quadratic function)
    [Space(0.5f)]
    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;


    private bool _isJumpCut;
    private bool _isJumpFalling;
    private void gravity()
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
