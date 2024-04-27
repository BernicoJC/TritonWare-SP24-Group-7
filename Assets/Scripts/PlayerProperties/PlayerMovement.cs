using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 10f;
    private float jumpingPower = 10f;
    private float dodgeStrength = 50f;
    private float dodgingTime = 0.05f;

    private bool isFacingRight = true;
    private bool canDodge = true;
    public bool cantMove = false;
    public float gravity;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // private AudioSource aud;

    void Start()
    {
        // aud = GetComponent<AudioSource>();
        gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // If grounded, reset the canDodge variable
        if (isGrounded())
        {
            canDodge = true;
        }

        // if currently dodging, don't allow controls
        if(cantMove)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal"); // getting from input
        vertical = Input.GetAxisRaw("Vertical");


        // OLD JUMP Mechanic (Maybe still want to use this?)
        /*
        // getbuttondown --> check if button is held (basically full hop)
        if(Input.GetButtonDown("Jump")) //  && isGrounded()
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpingPower);
            canDodge = true;
            
            //if(!aud.isPlaying)
            //{
            //    aud.Play();
            //}
        }

        // getbuttonup --> check for button is tapped (short hop), basically if only tap, the above one got halfed
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            canDodge = true;
        }
        */

        // New Jump mechanic (No short hop / long hop)
        if (Input.GetButtonDown("Jump")) //  && isGrounded()
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            canDodge = true;
        }

        // Air Dodge
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isGrounded() && canDodge)
        {
            StartCoroutine(airDodge());
        }

        // Check if need to flip every frame
        Flip();
    }

    private void FixedUpdate()
    {
        // if currently dodging, don't allow controls
        if(cantMove)
        {
            return;
        }

        // Read movement input
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    }

    public bool isGrounded()
    {
        // return if groundCheck.position overlap within 0.2f radius to groundLayer
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        // If facing right and input to left, flip (vise versa)
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            // transform sprite horizontally
            Vector3 localScale = transform.localScale; // I think since this script is attached to object, it's like this.transform.localScale
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator airDodge()
    {
        canDodge = false;
        // This variable is important so that you can't move while dodging
        cantMove = true;
        rb.gravityScale = 0f;

        float dH = horizontal * dodgeStrength;
        float dV = vertical * dodgeStrength;

        // Prevents airdodging up
        if(dV > 0)
        {
            dV = 0;
        }
        
        // I'm guessing use localScale here because want fixed speed
        rb.velocity = new Vector2(dH, dV);

        yield return new WaitForSeconds(dodgingTime);

        rb.gravityScale = gravity;

        cantMove = false;
    }
}