using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BumpedHeadCorrection : MonoBehaviour
{
    private Rigidbody2D m_RB;
    private LayerMask m_GroundedLayer;
    [SerializeField] private float topRayCastLength;
    [SerializeField] private Vector3 edgeRayCastOffset;
    [SerializeField] private Vector3 _innerRayCastOffset;
    private bool canCornerCorrect;


    private void FixedUpdate()
    {
        CheckCorners();
        if (canCornerCorrect) CornerCorrect( m_RB.velocity.y);
        
                
    }

    private void CornerCorrect(float m_yVelocity)
    {
        //push player to right
        RaycastHit2D hit = Physics2D.Raycast(transform.position - _innerRayCastOffset + Vector3.up * topRayCastLength, Vector3.left, topRayCastLength, m_GroundedLayer);
        if(hit.collider != null)
        {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRayCastLength,
                transform.position - edgeRayCastOffset + Vector3.up * topRayCastLength);
            transform.position = new Vector3(transform.position.x + newPos, transform.position.y,transform.position.z);
            m_RB.velocity = new Vector2(m_RB.velocity.x, m_yVelocity);
            
        }


        //push player to left
        hit = Physics2D.Raycast(transform.position + _innerRayCastOffset + Vector3.up * topRayCastLength, Vector2.right, topRayCastLength, m_GroundedLayer);
        if (hit.collider != null)
        {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRayCastLength,
                transform.position + edgeRayCastOffset + Vector3.up * topRayCastLength);
            transform.position = new Vector3(transform.position.x - newPos, transform.position.y, transform.position.z);
            m_RB.velocity = new Vector2(m_RB.velocity.x, m_yVelocity);
            
        }

    }


    private void CheckCorners()
    {
        canCornerCorrect = Physics2D.Raycast(transform.position + edgeRayCastOffset, Vector2.up, topRayCastLength, m_GroundedLayer) &&
                           !Physics2D.Raycast(transform.position + _innerRayCastOffset, Vector2.up, topRayCastLength, m_GroundedLayer) ||
                           Physics2D.Raycast(transform.position - edgeRayCastOffset, Vector2.up, topRayCastLength, m_GroundedLayer) &&
                           !Physics2D.Raycast(transform.position - _innerRayCastOffset, Vector2.up, topRayCastLength, m_GroundedLayer);

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //corners
        Gizmos.DrawLine(transform.position + edgeRayCastOffset, transform.position + edgeRayCastOffset + Vector3.up * topRayCastLength);
        Gizmos.DrawLine(transform.position - edgeRayCastOffset, transform.position - edgeRayCastOffset + Vector3.up * topRayCastLength);

        Gizmos.DrawLine(transform.position + _innerRayCastOffset, transform.position + _innerRayCastOffset + Vector3.up * topRayCastLength);
        Gizmos.DrawLine(transform.position - _innerRayCastOffset, transform.position - _innerRayCastOffset + Vector3.up * topRayCastLength);

        //corner distance
        Gizmos.DrawLine(transform.position - _innerRayCastOffset + Vector3.up * topRayCastLength,
                        transform.position - _innerRayCastOffset + Vector3.up * topRayCastLength + Vector3.left * topRayCastLength);
        Gizmos.DrawLine(transform.position + _innerRayCastOffset + Vector3.up * topRayCastLength,
                        transform.position + _innerRayCastOffset + Vector3.up * topRayCastLength + Vector3.right * topRayCastLength);





    }

}
