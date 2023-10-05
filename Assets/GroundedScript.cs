using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class GroundedScript : MonoBehaviour
{
    public event Action<bool> OnGroundChanged;
    private bool m_bGrounded;

    [SerializeField] private Collider2D m_GroundedCol;
    [SerializeField] private LayerMask m_GroundedLayer;


    private void Awake()
    {
        if(m_GroundedCol == null)
        {
            m_GroundedCol = GetComponent<Collider2D>();
        }
    }
    public bool CheckGround()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = m_GroundedLayer;
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        if(m_bGrounded != (m_GroundedCol.Cast(Vector2.down, filter, results, 0.1f, true) > 0))
        {
            //Fires when we just hit the ground  or have just left the ground
            m_bGrounded = !m_bGrounded;
        }
        return m_bGrounded;
    }

}
