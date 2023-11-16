using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour
{
    public bool m_bIsCrouching;
    [SerializeField] private float CrouchHeight = 0.5f;
    [SerializeField] private BoxCollider2D m_boxCollider;

    private Vector2 standColliderSize;
    private Vector2 standColliderOffset;

    private Vector2 crouchColliderSize;
    private Vector2 crouchColliderOffset;

    private void Awake()
    {
        standColliderSize = m_boxCollider.size;
        standColliderOffset = m_boxCollider.offset;

        crouchColliderSize = new Vector2(standColliderSize.x, standColliderSize.y * CrouchHeight);
        crouchColliderOffset = new Vector2(standColliderOffset.x, standColliderOffset.y * CrouchHeight);
    }

    public void Crouch()
    {
        m_bIsCrouching = true;
        m_boxCollider.size = crouchColliderSize;
        m_boxCollider.offset = crouchColliderOffset;
    }

    public void StandUp()
    {
        m_bIsCrouching = false;
        m_boxCollider.size = standColliderSize;
        m_boxCollider.offset = standColliderOffset;

    }
}
