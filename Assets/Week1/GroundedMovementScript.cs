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
    }
    public void Jump(InputAction.CallbackContext context)
    {

    
    }




      
 }
