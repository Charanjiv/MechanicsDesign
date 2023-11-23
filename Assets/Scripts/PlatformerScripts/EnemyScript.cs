using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundedLayer;
    bool game = true;
    private Coroutine CRenemyMove = null;

    int test = 0;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();

        enemyMove();



    }

    private IEnumerator C_enemyMovement()
    {
        
            Debug.Log("Enemy");
        while (test < 10)
        {
            //Debug.Log("Test");
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundedLayer);
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, 1 << player.gameObject.layer);

            if (isGrounded)
            {
                rb.velocity = new Vector2(direction * chaseSpeed, rb.velocity.y);


                RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundedLayer);
                RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundedLayer);
                RaycastHit2D platformAhead = Physics2D.Raycast(transform.position, Vector2.up, 3f, groundedLayer);

                if (!groundInFront.collider && !gapAhead.collider)
                {
                    shouldJump = true;
                }
                else if (isPlayerAbove && platformAhead.collider)
                {
                    shouldJump = true;
                }

            }
            move();
            test = 0;
            
            yield return new WaitForFixedUpdate();
        }
            yield return new WaitForFixedUpdate();

        
    }

    private void move()
    {
        if(isGrounded&&shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * jumpForce;

            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);


        }
    }

    private void enemyMove()
    {
        StartCoroutine(C_enemyMovement());
    }



}
