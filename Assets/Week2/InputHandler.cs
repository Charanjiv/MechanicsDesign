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
    public GroundedMovementScript m_GMoveComp;

    #endregion
    public Rigidbody2D rb;




    private void Awake()
    {
        m_Input = GetComponent<PlayerInput>();
        m_GMoveComp = GetComponent<GroundedMovementScript>();  //get movement script

    }
    private void Start()
    {
        m_Input.currentActionMap.FindAction("Move").performed += Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled += Handle_MoveCancelled;
        m_Input.currentActionMap.FindAction("Jump").performed += Handle_JumpPertformed;
        m_Input.currentActionMap.FindAction("Jump").canceled += Handle_JumpCancelled;

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
        
 
        Debug.Log("Jump");
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
     * https://mermaid.live/edit#pako:eNplkUFqwzAQRa8iZpUQ5wKiu5a2FAKB7IqgTKxxbSJpjCwXQuq7V7It1021kfTf8OePdIOSNYGE0mDXPTX46dEqJ-I6GrySFw_f-7148dw7_VhTefkPD_xFllz4Q6TYVYYxCPtRvRKaUN_jt962m-29msySOuljqnX326QLsTszm2h-Nt2ESS9orMzqZitlqp3osPbNwX9Nl8gZnVpaGWecoj-zL2khOfZ8zbPlOaa2UIAlb7HR8cHHpgpCHfsokPGo0V8UKDfEOuwDn66uBBl8TwX0rcZA8_-ArNB0USXdBPaH-QfTVkCL7p051ww_0yGZbQ
    */
}
