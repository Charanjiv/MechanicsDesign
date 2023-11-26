using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed;
    public float jumpForce;
    public LayerMask groundedLayer;
    bool game = true;
    

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(C_enemyMovement());
    }

    private IEnumerator C_enemyMovement()
    
    {

        while (game)
        {
            Debug.Log("Test");
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
            yield return new WaitForFixedUpdate();
        }
            
           
   
    }

    private void FixedUpdate()
    {
        if(isGrounded&&shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * jumpForce;

            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);


        }
    }

   



}
