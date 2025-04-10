using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGrounded;

    public bool GroundState()
    {
        return isGrounded;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       if(other.gameObject.CompareTag("Ground"))
       {
          isGrounded = true;
       }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
           isGrounded = false;
        }
    }
}
