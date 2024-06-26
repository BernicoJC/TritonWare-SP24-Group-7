using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(animator != null)
        {
            animator.SetTrigger("Death");
        }

        if (gameObject.tag == "Player")
        {
            SceneManager.LoadScene("GameOver");
        }
        GetComponent<Collider2D>().enabled = false;
        Destroy(transform.parent.gameObject, deathAnimationLength);
    }
}
