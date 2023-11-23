using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class BumpedHeadCorrection : MonoBehaviour
{
    private Rigidbody2D m_RB;
    private LayerMask m_GroundedLayer;
    /*[SerializeField] private float topRayCastLength;
    [SerializeField] private Vector3 edgeRayCastOffset;
    [SerializeField] private Vector3 _innerRayCastOffset;
    private bool canCornerCorrect;*/



    private RaycastHit2D hitInnerLeft;
    private RaycastHit2D hitInnerRight;
    private RaycastHit2D hitOuterLeft;
    private RaycastHit2D hitOuterRight;
    private RaycastHit2D hitTopLeft;
    private RaycastHit2D hitTopRight;
    public float distanceUpwards;
    public float nudgeAmount;
    public float innerSize;
    public float outerSize;
    private float translateAmount;

    private void Start()
    {
        StartCoroutine(C_CollisionDetector());
    }
    private void FixedUpdate()
    {
        //CheckCorners();
        //if (canCornerCorrect) CornerCorrect(m_RB.velocity.y);

        CornerCollisionMove();
        
    }

    /*private void CornerCorrect(float m_yVelocity)
    {
        //push player to right
        RaycastHit2D hit = Physics2D.Raycast(transform.position - _innerRayCastOffset + Vector3.up * topRayCastLength, Vector3.left, topRayCastLength, m_GroundedLayer);
        if (hit.collider != null)
        {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRayCastLength,
                transform.position - edgeRayCastOffset + Vector3.up * topRayCastLength);
            transform.position = new Vector3(transform.position.x + newPos, transform.position.y, transform.position.z);
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

    }*/


    /*private void OnDrawGizmos()
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





    }*/

    private IEnumerator C_CollisionDetector()
    {
        CornerCollisionDetection();
        yield return new WaitForFixedUpdate();
    }
    void CornerCollisionDetection()
    {
        // corner collision top
        hitInnerLeft = Physics2D.Raycast(transform.position - new Vector3(innerSize, 0, 0), Vector2.up, distanceUpwards, m_GroundedLayer);
        hitInnerRight = Physics2D.Raycast(transform.position + new Vector3(innerSize, 0, 0), Vector2.up, distanceUpwards, m_GroundedLayer);
        hitOuterLeft = Physics2D.Raycast(transform.position - new Vector3(outerSize, 0, 0), Vector2.up, distanceUpwards, m_GroundedLayer);
        hitOuterRight = Physics2D.Raycast(transform.position + new Vector3(outerSize, 0, 0), Vector2.up, distanceUpwards, m_GroundedLayer);
        hitTopLeft = Physics2D.Raycast(transform.position + new Vector3(-innerSize, distanceUpwards, 0), Vector2.left, outerSize - innerSize, m_GroundedLayer);
        hitTopRight = Physics2D.Raycast(transform.position + new Vector3(innerSize, distanceUpwards, 0), Vector2.right, outerSize - innerSize, m_GroundedLayer);
    }

    void CornerCollisionMove()
    {
        // corner collision top
        if (hitOuterLeft.collider != null && hitInnerLeft.collider)
        {
            translateAmount = Mathf.Abs(hitTopLeft.point.x - hitOuterLeft.point.x + 0.01f);
            transform.Translate(translateAmount, 0, 0);
        }
        if (hitOuterRight.collider != null && hitInnerRight.collider)
        {
            translateAmount = Mathf.Abs(hitOuterRight.point.x - hitTopRight.point.x + 0.01f);
            transform.Translate(-translateAmount, 0, 0);
        }
    }


    }
