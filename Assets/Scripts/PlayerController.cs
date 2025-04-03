using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float moveInput;
    public float jumpForce = 7f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] private GameObject groundCheckerScript;

    private Animator playerAnimator;
    public SpriteRenderer playerSR;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();
        TotalAnimation();
    }



    void Move()
    {
        moveInput = Input.GetAxis("Horizontal");
        if(moveInput < 1f)
        {
        playerSR.flipX = true;
        }
      else
      {
        playerSR.flipX = false;
      }
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        playerAnimator.SetFloat("SpeedX", Mathf.Abs(moveInput));
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundCheckerScript.GetComponent<GroundChecker>().GroundState())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerAnimator.Play("Jump");
        }
    }

    void TotalAnimation()
    {
      FallAnimation();
      
    }

    void FallAnimation()
    {
        playerAnimator.SetFloat("SpeedY", rb.velocity.y);
    }
}
