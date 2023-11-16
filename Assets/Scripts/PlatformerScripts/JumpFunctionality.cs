using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpFunctionality : MonoBehaviour
{
	private Rigidbody2D m_RB;
	private GroundedScript m_GroundedScipt;
	

	[Header("Jumping")]
	[SerializeField] private float m_fJumpPower = 10.0f;
	//public bool m_IsJumping;
	//[SerializeField] private float LastOnGroundTime;
	//public float jumpForce;

	//Jump
	
	[SerializeField] private float m_CoyoteDuration;
	[SerializeField] private float m_JumpBufferDuration;
	//private float LastPressedJumpTime;

	private bool m_IsRequestingJump, m_JumpPossible, m_RecentlyJumped;
	private Coroutine m_CRCoyoteTime, m_CRJumpBuffer, m_CRPostLaunchDelay;



	public bool checkJump;

	private void Awake()
	{
		m_RB = GetComponent<Rigidbody2D>();
		//m_Grounded = true;
		m_GroundedScipt = GetComponent<GroundedScript>();
		

		m_GroundedScipt.OnGroundChanged += Handle_GroundedChanged;
		m_IsRequestingJump = m_JumpPossible = m_RecentlyJumped = false;
		

	}

	
	public void OnJumpInput(bool triggered)
	{

		//Debug.Log(m_GroundedScipt.IsGrounded);


		if(m_GroundedScipt.m_bIsGrounded)
		{
			Launch();
		}
		else if(m_JumpPossible)
		{
			Launch();
			StopCoroutine(m_CRCoyoteTime);
			m_JumpPossible = false;
		}
		else
		{
			if(m_IsRequestingJump)
			{
				StopCoroutine(m_CRJumpBuffer);
			}

			m_CRJumpBuffer = StartCoroutine(C_JumpBuffering());
		}
	}



	private void Launch()
	{

	

		//float force = jumpForce;
		//if (m_RB.velocity.y < 0)
		//	force += m_RB.velocity.y;
		//m_RB.AddForce(Vector2.up * m_fJumpPower, ForceMode2D.Impulse);

        m_RB.velocity = new Vector2(m_RB.velocity.x, m_fJumpPower);
        m_CRPostLaunchDelay = StartCoroutine(C_PostLaunchDelay());
		//Debug.Log("Jumping");

	}



	private void Handle_GroundedChanged(bool newGrounded)
	{
		if (newGrounded)
		{
			//just landed
			if(m_IsRequestingJump)
			{
                Debug.Log("JumpBuffer");
                StopCoroutine(m_CRJumpBuffer);
				m_IsRequestingJump = false;
				Launch();
			}
		}
		else
		{
			//Just left the floor
			if(!m_RecentlyJumped)
			{
				m_CRCoyoteTime = StartCoroutine(C_CoyoteTime());
			}
		}
	}

	private IEnumerator C_CoyoteTime()
	{
		m_JumpPossible = true;
		yield return new WaitForSeconds(m_CoyoteDuration);
		m_JumpPossible = false;

	}

	private IEnumerator C_JumpBuffering()
	{
		
		m_IsRequestingJump = true;
		yield return new WaitForSeconds(m_JumpBufferDuration);
		m_IsRequestingJump = false;

	}

	private IEnumerator C_PostLaunchDelay()
	{
		m_RecentlyJumped = true;
		yield return new WaitForSeconds(0.2f);
		m_RecentlyJumped = false;
	}

}