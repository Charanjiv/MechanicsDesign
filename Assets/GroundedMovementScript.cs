using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class GroundedMovementScript : MonoBehaviour
{
    [SerializeField] private float m_fMoveStrength;
    [SerializeField] private AnimationCurve m_StrengthLUT;
    [SerializeField] private Vector2 m_SpeedLimits;

    [SerializeField] private Rigidbody2D m_RB;


    private float m_fRequestedDir;

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



}
