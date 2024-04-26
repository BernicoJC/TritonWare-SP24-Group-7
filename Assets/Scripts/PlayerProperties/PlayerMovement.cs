using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float baseSpeed = 10f;
    private float speed = 10f;
    private float sprintSpeed = 20f;
    private float jumpingPower = 30f;
    private float fastFallSpeed = 60f;
    private float softFallSpeed = 5f;
    private float dodgeStrength = 50f;
    private float dodgingTime = 0.05f;

    private bool isFacingRight = true;
    private bool canDodge = true;
    private bool isDodging = false;
    private bool isSprinting = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // private AudioSource aud;

    void Start()
    {
        // aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // If grounded, reset the canDodge variable
        if (isGrounded())
        {
            canDodge = true;
            // isDodging is put down there instead of here; since if it touch ground and this gets disabled, then other controls got reactivated
            // isDodging = false;
        }

        // if currently dodging, don't allow controls
        if (isDodging)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal"); // getting from input
        vertical = Input.GetAxisRaw("Vertical");


        // getbuttondown --> check if button is held (basically full hop)
        if (Input.GetButtonDown("Vertical")) //  && isGrounded()
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpingPower);
            canDodge = true;

            //if(!aud.isPlaying)
            //{
            //    aud.Play();
            //}
        }

        // getbuttonup --> check for button is tapped (short hop), basically if only tap, the above one got halfed
        if (Input.GetButtonUp("Vertical") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            canDodge = true;
        }

        // Fast fall
        if (vertical < 0 && !isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, -fastFallSpeed);
        }

        // Air Dodge
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isGrounded() && canDodge)
        {
            StartCoroutine(airDodge());
        }

        // Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded() && !isSprinting)
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded() && isSprinting)
        {
            isSprinting = false;
        }
        else if (!isGrounded() || isDodging) //change to if x direction changed
        {
            isSprinting = false;
        }

        // Soft fall
        if (!isGrounded() && rb.velocity.y < -10 && Input.GetAxisRaw("Vertical") > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -softFallSpeed);
        }

        // Check if need to flip every frame
        Flip();
    }

    private void FixedUpdate()
    {
        // if currently dodging, don't allow controls
        if (isDodging)
        {
            return;
        }

        /*
        if(isGrounded() && isDodging)
        {
            rb.velocity = new Vector2(0f, 0f);
            isDodging = false;
        }
        */

        // changing its velocity every update to x = the input * the base speed, y = current y
        /*
        if(horizontal != 0){ // So can keep momentum, don't change velocity if no new input
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            // Vector2 position = rb.position;
            // Vector2 move = new Vector2(horizontal, rb.velocity.y);

            // if(rb.velocity.y < 0)
            // {
            //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - regularFallSpeed);
            // }
            
            // position = position + move * speed * Time.fixedDeltaTime;
            // rb.MovePosition(position);

        }
        */

        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = baseSpeed;
        }

        // Read movement input
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    }

    private bool isGrounded()
    {
        // return if groundCheck.position overlap within 0.2f radius to groundLayer
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        // If facing right and input to left, flip (vise versa)
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
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
        isDodging = true;
        float oldGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float dH = horizontal * dodgeStrength;
        float dV = vertical * dodgeStrength;

        // Prevents airdodging up
        if (dV > 0)
        {
            dV = 0;
        }

        // I'm guessing use localScale here because want fixed speed
        rb.velocity = new Vector2(dH, dV);

        yield return new WaitForSeconds(dodgingTime);

        // Smash Bros airdodge mechanic
        //if(!isGrounded())
        //{
        //    rb.velocity = new Vector2(0, 0);
        //    yield return new WaitForSeconds(0.3f);
        //}

        rb.gravityScale = oldGravity;

        // isDodging is put here instead of when touching ground; since if it touch ground and this gets disabled, then other controls got reactivated
        isDodging = false;
    }
}