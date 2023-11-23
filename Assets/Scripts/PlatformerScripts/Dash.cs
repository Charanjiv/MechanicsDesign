using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private bool m_bCanDash;
    private bool m_bIsDashing;
    [SerializeField] private float m_fDashingPower;
    [SerializeField] private float m_fDashingTime;
    [SerializeField] private float m_fDashingCooldown;

    [SerializeField] private Rigidbody2D m_RB;
    [SerializeField] private TrailRenderer m_TrailRenderer;

    public void startDash()
    {
        if (m_bIsDashing)
        {
            return;
        }
        if (m_bCanDash)
        {
            StartCoroutine(C_Dash());
        }
    }
    private void FixedUpdate()
    {
        if (m_bIsDashing)
        {
            return;
        }
    }
    public IEnumerator C_Dash()
    {

        m_bCanDash = false;
        m_bIsDashing = true;
        float originalGravity = m_RB.gravityScale;
        m_RB.gravityScale = 0f;
        m_RB.velocity = new Vector2(m_RB.velocity.x * m_fDashingPower, 0f); //(transform.localScale.x
        m_TrailRenderer.emitting = true;
        yield return new WaitForSeconds(m_fDashingTime);
        m_TrailRenderer.emitting= false;
        m_RB.gravityScale=originalGravity;
        m_bIsDashing = false;
        yield return new WaitForSeconds(m_fDashingCooldown);
        m_bCanDash = true;

    }
}
