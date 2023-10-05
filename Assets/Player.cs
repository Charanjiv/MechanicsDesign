using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GroundedMovementScript))]

public class Player : MonoBehaviour
{
    [SerializeField] private GroundedMovementScript m_GMoveComp;

    private void Awake()
    {
        m_GMoveComp = GetComponent<GroundedMovementScript>();

    }
    private void Update()
    {
        
    }


}
