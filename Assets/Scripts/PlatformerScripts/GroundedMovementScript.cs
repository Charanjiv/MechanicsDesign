using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody2D))]
//if add to body without rigidbody, can't remove rb if script is attached
public class GroundedMovementScript : MonoBehaviour


{

    //private 
    [SerializeField] private Rigidbody2D m_RB;
    private CrouchScript m_CrouchScript;


    [Header("Movement")]
    [SerializeField] private float m_fMoveStrength;
    [SerializeField] private AnimationCurve m_StrengthLUT; //store a curve, nothing to do with animation
    [SerializeField] private Vector2 m_SpeedLimits; 
    private float m_fRequestedDir; //looks for direction



  

    private void Awake()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_CrouchScript = GetComponent<CrouchScript>();
        //Gravity();

    }
    public void AddMovementInput(float inMov)
    {
        if (m_fRequestedDir != inMov)
        {
            m_fRequestedDir = inMov;
        }

        StartMoving();
    }

    private void StartMoving()
    {
        if (m_CrouchScript.m_bIsCrouching == true)
        {
            m_RB.AddForce(Vector2.right * m_fRequestedDir * m_fMoveStrength * 0.5f, ForceMode2D.Force);
        }
        else
        {
            m_RB.AddForce(Vector2.right * m_fRequestedDir * m_fMoveStrength, ForceMode2D.Force);
        }



        if (m_RB.velocity.magnitude > m_SpeedLimits.y)
        {
            m_RB.velocity = m_RB.velocity.normalized * m_SpeedLimits.y;
        }
    }



   
}
