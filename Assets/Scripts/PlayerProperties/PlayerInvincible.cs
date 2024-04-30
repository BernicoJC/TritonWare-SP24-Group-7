using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{
    public float invincibleTime;
    public float damagedTime;

    private int LayerDefault;
    private int LayerInvincible;

    public Animator animator;

    public void Start()
    {
        LayerDefault = LayerMask.NameToLayer("Default");
        LayerInvincible = LayerMask.NameToLayer("Invincible");
        animator = GetComponent<Animator>();
    }
    public void applyInvincibility()
    {
        StartCoroutine(invincibilityFrame());
    }

    public IEnumerator invincibilityFrame()
    {
        // float old = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<PlayerMovement>().cantMove = true;

        gameObject.layer = LayerInvincible;

        animator.SetTrigger("Hurt");

        yield return new WaitForSeconds(damagedTime);

        gameObject.GetComponent<Rigidbody2D>().gravityScale = gameObject.GetComponent<PlayerMovement>().gravity;
        gameObject.GetComponent<PlayerMovement>().cantMove = false;

        yield return new WaitForSeconds(invincibleTime - damagedTime);

        gameObject.layer = LayerDefault;
    }
}
