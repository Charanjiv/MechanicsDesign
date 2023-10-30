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



    [SerializeField] private Collider2D m_GroundedCol; //ground
    [SerializeField] private LayerMask m_GroundedLayer; //collider


    private void Awake()
    {
        if(m_GroundedCol == null)
        {
            m_GroundedCol = GetComponent<Collider2D>(); //setting GroundCol to the collider
        }
    }
    public bool CheckGround()
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
            

        }



        return m_bGrounded = false;
    }


}


