using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;
    public float speed;
    Vector3 targetPos;

    GroundedMovementScript movementScript;
    Rigidbody2D rb;
    Vector3 moveDirection;
    bool move = true;

    private void Awake()
    {
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<GroundedMovementScript>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        targetPos = posB.position;
        DirectionCalculate();
        StartCoroutine(C_PlatformMove());
    }

    //private void Update()
    private IEnumerator C_PlatformMove()
    {

        while (move)
        {
            if (Vector2.Distance(transform.position, posA.position) < 0.05f)
            {
                targetPos = posB.position;
                DirectionCalculate();
            }

            if (Vector2.Distance(transform.position, posB.position) < 0.05f)
            {
                targetPos = posA.position;
                DirectionCalculate();
            }
            yield return new WaitForFixedUpdate();
        }



    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    private void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            movementScript.isOnPlatform = true;
            movementScript.platformRB = rb;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementScript.isOnPlatform = false;
            
        }
    }
}
