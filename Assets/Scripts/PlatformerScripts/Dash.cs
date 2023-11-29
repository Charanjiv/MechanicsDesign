using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private bool m_bCanDash;
    private bool m_bIsDashing;
    private AudioManager audioManager;
    [SerializeField] private float m_fDashingPower;
    [SerializeField] private float m_fDashingTime;
    [SerializeField] private float m_fDashingCooldown;

    [SerializeField] private Rigidbody2D m_RB;
    [SerializeField] private TrailRenderer m_TrailRenderer;

    private void Start()
    {
        m_bCanDash = true;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public IEnumerator C_Dash()
    {
        if (m_bCanDash)
        {
            audioManager.PlayerSFX(audioManager.Dash);
            m_bCanDash = false;
            m_bIsDashing = true;
            float originalGravity = m_RB.gravityScale;
            m_RB.gravityScale = 0f;
            m_RB.velocity = new Vector2(m_RB.velocity.x * m_fDashingPower, 0f); //(transform.localScale.x
            m_TrailRenderer.emitting = true;
            Debug.Log(1);
            yield return new WaitForSeconds(m_fDashingTime);
            Debug.Log(2);
            m_TrailRenderer.emitting = false;
            m_RB.gravityScale = originalGravity;
            m_bIsDashing = false;
            Debug.Log(3);
            yield return new WaitForSeconds(m_fDashingCooldown);
            Debug.Log(4);
            m_bCanDash = true;
        }

        yield return null;
    }
}
