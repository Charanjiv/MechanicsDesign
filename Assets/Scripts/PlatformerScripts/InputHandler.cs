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
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem smokeFX;
    private PlayerInput m_Input;

    //Movement
    private float m_fInMove;
    private bool m_bIsMove;
    private Coroutine m_CRMove;

 

    private GroundedMovementScript m_GMoveComp;
    public PlayerOneWayPlatform m_OneWayP;
    public JumpFunctionality m_JumpScript;
    public GroundedScript m_GroundedScript;
    public CrouchScript m_CrouchScript;
    private Dash m_DashScript;

    #endregion
    





    private void Awake()
    {
        m_Input = GetComponent<PlayerInput>();
        m_GMoveComp = GetComponent<GroundedMovementScript>();
        m_OneWayP = GetComponent<PlayerOneWayPlatform>();
        m_JumpScript = GetComponent<JumpFunctionality>();
        m_GroundedScript = GetComponent<GroundedScript>();
        m_CrouchScript = GetComponent<CrouchScript>();
        m_DashScript = GetComponent<Dash>();
        
        
    }
    private void Start()
    {
        m_Input.currentActionMap.FindAction("Move").performed += Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled += Handle_MoveCancelled;

        m_Input.currentActionMap.FindAction("Jump").performed += Handle_JumpPertformed;
        m_Input.currentActionMap.FindAction("Jump").canceled += Handle_JumpCancelled;

        m_Input.currentActionMap.FindAction("HeldJump").performed += Handle_PoweredJumpPertformed;

        m_Input.currentActionMap.FindAction("OneWayPlatform").performed += Handle_OneWayPlatformPerformed;

        m_Input.currentActionMap.FindAction("Crouch").performed += Handle_CrouchPerformed;
        m_Input.currentActionMap.FindAction("Crouch").canceled += Handle_CrouchCancelled;
        
        m_Input.currentActionMap.FindAction("Dash").performed += Handle_DashPerformed;
    }

    /*private void OnDestroy()
    {
        m_Input.currentActionMap.FindAction("Move").performed -= Handle_MovePerformed;
        m_Input.currentActionMap.FindAction("Move").canceled -= Handle_MoveCancelled;

        m_Input.currentActionMap.FindAction("Jump").performed -= Handle_JumpPertformed;
        m_Input.currentActionMap.FindAction("Jump").canceled -= Handle_JumpCancelled;
        m_Input.currentActionMap.FindAction("Crouch").performed -= Handle_CrouchPerformed;
    }*/

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
        
        while (m_bIsMove)
        {

            m_GMoveComp.AddMovementInput(m_fInMove);
            smokeFX.Play();
            //Debug.Log($"Move Update! Value: {m_fInMove}");  
            yield return new WaitForFixedUpdate();
 
        }
    }

    #endregion

    #region Jump
    private void Handle_JumpPertformed(InputAction.CallbackContext context)
    {
		m_JumpScript.OnJumpInput(true);
        smokeFX.Play();
    }

    private void Handle_JumpCancelled(InputAction.CallbackContext context)
    {
		m_JumpScript.OnJumpInput(true);
    }

    private void Handle_PoweredJumpPertformed(InputAction.CallbackContext context)
    {
        m_JumpScript.ChargedLaunch();
    }
    #endregion

    #region One WayPlatform
    private void Handle_OneWayPlatformPerformed(InputAction.CallbackContext context)
    {
        m_OneWayP.CrouchOn();
    }

    #endregion


    #region Crouch
    
    private void Handle_CrouchPerformed(InputAction.CallbackContext context)
    {
        m_CrouchScript.Crouch();
    }
    private void Handle_CrouchCancelled(InputAction.CallbackContext context)
    {
        m_CrouchScript.StandUp();
    }
    #endregion
    private void Handle_DashPerformed(InputAction.CallbackContext context)
    {
        m_DashScript.StartCoroutine(m_DashScript.C_Dash());//startDash();

    }

}
