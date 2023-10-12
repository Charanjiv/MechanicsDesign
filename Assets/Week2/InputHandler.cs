using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(PlayerInput))]

public class InputHandler : MonoBehaviour

    #region input variables
{
    private PlayerInput m_Input;
    private float m_fInMove;
    private bool m_bIsMove;
    private Coroutine m_CRMove;

    private bool m_bISJump;

    #endregion

    private void Awake()
    {
        m_Input = GetComponent<PlayerInput>();  
    }
    private void Start()
    {
        m_Input.currentActionMap.FindAction("Move").performed += Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled += Handle_MoveCancelled;

    }
    private void Handle_MovePerformed(InputAction.CallbackContext context)
    {
        m_fInMove = context.ReadValue<float>();
        m_bIsMove = true;
        if(m_CRMove == null)
        {
            m_CRMove = StartCoroutine(C_MoveUpdate());
        }
    }

    private void Handle_MoveCancelled(InputAction.CallbackContext context)
    {
        m_fInMove = context.ReadValue<float>();
        m_bIsMove = false;
        if (m_CRMove != null)
        {
            StopCoroutine(m_CRMove);
        }
        m_CRMove = null;
    }

    private IEnumerator C_MoveUpdate()
    {
        while(m_bIsMove)
        {
            Debug.Log($"Move Update! Value: {m_fInMove}");
            yield return null;
        }
        

    }

    private void Handle_JumpPertformed(InputAction.CallbackContext context)
    {

    }

    private void Handle_JumpCancelled(InputAction.CallbackContext context)
    {

        
    }



    /* 
     * TO DO
     * bind some functions to input
     * have some vartaibles for storing input values locally
     * record the values
     * run update for move
    */
}
