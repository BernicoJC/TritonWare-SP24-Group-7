using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator animator;
    public float deathAnimationLength;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void kill()
    {
        animator.SetTrigger("Death");
        //Destroy(rb);
        Destroy(transform.parent.gameObject, deathAnimationLength);
    }
}
