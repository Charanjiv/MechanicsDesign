using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(PlayerInput))]

public class InputHandler : MonoBehaviour

    #region input variables
{
    private PlayerInput m_Input;

    //Movement
    private float m_fInMove;
    private bool m_bIsMove;
    private Coroutine m_CRMove;

    //Jump
    private bool m_bIsJump;
    private Coroutine m_CRJump;



    public GroundedMovementScript m_GMoveComp;
    public PlayerOneWayPlatform m_OneWayP;
    public JumpFunctionality m_JumpScript;
    public GroundedScript m_GroundedScript;

    #endregion
    public Rigidbody2D rb;




    private void Awake()
    {
        m_Input = GetComponent<PlayerInput>();
        m_GMoveComp = GetComponent<GroundedMovementScript>();//get movement script
        m_OneWayP = GetComponent<PlayerOneWayPlatform>();
        m_JumpScript = GetComponent<JumpFunctionality>();
        m_GroundedScript = GetComponent<GroundedScript>();
        m_GMoveComp.Gravity();
        
    }
    private void Start()
    {
        m_Input.currentActionMap.FindAction("Move").performed += Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled += Handle_MoveCancelled;

        m_Input.currentActionMap.FindAction("Jump").performed += Handle_JumpPertformed;
        m_Input.currentActionMap.FindAction("Jump").canceled += Handle_JumpCancelled;
        m_Input.currentActionMap.FindAction("Crouch").performed += Handle_CrouchPerformed;
    }

    private void OnDestroy()
    {
        m_Input.currentActionMap.FindAction("Move").performed += Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled += Handle_MoveCancelled;

        m_Input.currentActionMap.FindAction("Jump").performed += Handle_JumpPertformed;
        m_Input.currentActionMap.FindAction("Jump").canceled += Handle_JumpCancelled;
        m_Input.currentActionMap.FindAction("Crouch").performed += Handle_CrouchPerformed;
    }

    #region Movement
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
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        m_CRMove = null;
        
    }

    private IEnumerator C_MoveUpdate()
    {
        while(m_bIsMove)
        {
            m_GMoveComp.AddMovementInput(m_fInMove);
            Debug.Log($"Move Update! Value: {m_fInMove}");
            //rb.AddForce(new Vector2(m_fInMove, m_fInMove) * m_Speed, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
           
            
            yield return null;
            
        }
        

    }
    #endregion

    #region Jump
    private void Handle_JumpPertformed(InputAction.CallbackContext context)
    {
        m_bIsJump = true;
        if (m_CRJump == null && m_GroundedScript.IsGrounded())
        {
            m_CRJump = StartCoroutine(C_JumpUpdate());
            Debug.Log("Jumping");
        }
    }

    

    private void Handle_JumpCancelled(InputAction.CallbackContext context)
    {
        m_bIsJump = false;
        if (m_CRJump != null)
        {
            StopCoroutine(m_CRJump);
            
        }
        m_CRJump = null;
    }

    private IEnumerator C_JumpUpdate()
    {
        //m_JumpScript.Jump();
        yield return null;
    }
    #endregion

    #region One WayPlatform
    private void Handle_CrouchPerformed(InputAction.CallbackContext context)
    {
        m_OneWayP.CrouchOn();
    }

    #endregion


     // https://mermaid.live/edit#pako:eNplkUFqwzAQRa8iZpUQ5wKiu5a2FAKB7IqgTKxxbSJpjCwXQuq7V7It1021kfTf8OePdIOSNYGE0mDXPTX46dEqJ-I6GrySFw_f-7148dw7_VhTefkPD_xFllz4Q6TYVYYxCPtRvRKaUN_jt962m-29msySOuljqnX326QLsTszm2h-Nt2ESS9orMzqZitlqp3osPbNwX9Nl8gZnVpaGWecoj-zL2khOfZ8zbPlOaa2UIAlb7HR8cHHpgpCHfsokPGo0V8UKDfEOuwDn66uBBl8TwX0rcZA8_-ArNB0USXdBPaH-QfTVkCL7p051ww_0yGZbQ
    
}
