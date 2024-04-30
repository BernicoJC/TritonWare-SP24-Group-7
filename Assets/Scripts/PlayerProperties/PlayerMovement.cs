using System.Collections;
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

    public Animator animator;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;

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
        if (isGrounded() && isGroundedPlatform())
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

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetFloat("YSpeed", rb.velocity.y);

        // New Jump mechanic (No short hop / long hop)
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            canDodge = true;

            if (!isGrounded() && !isGroundedPlatform())
            {
                animator.SetBool("IsMidairJumping", true);
            }
        }

        // Air Dodge
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isGrounded() && !isGroundedPlatform() && canDodge)
        {
            StartCoroutine(airDodge());
        }

        if(rb.velocity.y == 0 && (isGrounded() || isGroundedPlatform()))
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsMidairJumping", false);
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
    /*
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsMidairJumping", false);
    }
    */

    public bool isGrounded()
    {
        // return if groundCheck.position overlap within 0.2f radius to groundLayer
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool isGroundedPlatform()
    {
        // return if groundCheck.position overlap within 0.2f radius to groundLayer
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, platformLayer);
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
        animator.SetTrigger("Dash");
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