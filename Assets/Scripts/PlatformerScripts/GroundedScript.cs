    using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedScript : MonoBehaviour
{
    public event Action<bool> OnGroundChanged;
    public bool m_bGrounded = true;
    public bool m_bIsGrounded { get { return m_bGrounded; } }
    [Header("Grounded")]
    [SerializeField] private Collider2D m_GroundedCol;
    [SerializeField] private LayerMask m_GroundedLayer;

    [Header("Gravity")]
    [SerializeField] private Rigidbody2D m_RB;
    [SerializeField] private float m_fBaseGravity = 2.0f;
    [SerializeField] private float m_fMaxFallSpeed = 18.0f;
    [SerializeField] private float m_fFallSpeedMultiplier = 2.0f;

    private void Awake()
    {
        if(m_GroundedCol == null)
        {
            m_GroundedCol = GetComponent<Collider2D>();
        }
    }

    private void Update()
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(m_GroundedLayer);
        bool newGrounded = (m_GroundedCol.Cast(Vector2.down, filter, hits, 0.1f, true) > 0);
        if(m_bGrounded != newGrounded)
        {
            m_bGrounded = newGrounded;
            OnGroundChanged?.Invoke(m_bGrounded);
        }

        //Debug.Log(m_bGrounded);
        Gravity();
    }

    public void Gravity()
    {
        if (m_RB.velocity.y < 0)
        {
            m_RB.gravityScale = m_fBaseGravity * m_fFallSpeedMultiplier; //fall increasingly faster
            m_RB.velocity = new Vector2(m_RB.velocity.x, Mathf.Max(m_RB.velocity.y, -m_fMaxFallSpeed));
        }
        else
        {
            m_RB.gravityScale = m_fBaseGravity;
        }
    }
}




/*public bool CheckGround()
{
    ContactFilter2D filter = new ContactFilter2D();
    filter.layerMask = m_GroundedLayer;
    List<RaycastHit2D> results = new List<RaycastHit2D>();
    if(m_bGrounded != (m_GroundedCol.Cast(Vector2.down, filter, results, 0.1f, true) > 0))//casting a collider, return true if collided with something
    {
        //Fires when we just hit the ground  or have just left the ground
        m_bGrounded = !m_bGrounded;

        OnGroundChanged(m_bGrounded);
        m_bGrounded = true;
        Debug.Log("IsGrounded");


    }

    return m_bGrounded = false;
}*/
