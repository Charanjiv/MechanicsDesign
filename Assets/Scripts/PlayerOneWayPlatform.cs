using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerOneWayPlatform : MonoBehaviour
{

    //private Collider2D collider;
    //private bool playerOnPlatform;
    private GameObject currenyOneWayPlatform;
    [SerializeField] private BoxCollider2D playerCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currenyOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currenyOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currenyOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }



    public void CrouchOn()
    {
       if(currenyOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
            
    }

























    /*private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        if (gameObject.CompareTag("OneWayPlatform"))
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                playerOnPlatform = value;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      
            SetPlayerOnPlatform(other, true);
        
            
    }

    private void OnCollisionExit2D(Collision2D other)
    {
     
            SetPlayerOnPlatform(other, true);
        
    }
    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }

    public void FallThroughPlatform(InputAction.CallbackContext context)
    {

        Debug.Log("Performe fall");
        collider.enabled = false;
            StartCoroutine(EnableCollider());
       } */

}







































    /*private GameObject currentOneWayPlatform;
    [SerializeField] private CapsuleCollider2D playerCollider;
    public void FallThroughPlatform(InputAction.CallbackContext context)
    {
        if(currentOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        CapsuleCollider2D platformCollider = currentOneWayPlatform.GetComponent<CapsuleCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(3f);
        Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
    }*/

