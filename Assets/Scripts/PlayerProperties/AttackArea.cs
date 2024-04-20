using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int knockbackStrength = 2;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collided) // Built in function, (if collides basically)
    {
        // GetComponent is finding for the collider's script basically
        if (collided.GetComponent<Health>() != null && collided.tag == "Enemy") // ONLY if what's colliding has a health; also, it's making sure it's hitting an enemy
        {
            Health health = collided.GetComponent<Health>();
            health.takeDamage(damage);

            // Vector2 vect = (collided.transform.position - transform.parent.transform.position).normalized * knockbackStrength;
            // collided.attachedRigidbody.velocity = new Vector2(vect.x, vect.y);

            // Temporary 0 Knockback
            // collided.attachedRigidbody.AddForce(vect * knockbackStrength, ForceMode2D.Impulse);

            StartCoroutine(doKnockback(collided));
        }
    }

    public IEnumerator doKnockback(Collider2D collided)
    {
        Vector2 vect = (collided.transform.position - transform.parent.transform.position).normalized * knockbackStrength;
        collided.attachedRigidbody.velocity = new Vector2(vect.x, vect.y);

        yield return new WaitForSeconds(0.1f);

        collided.attachedRigidbody.velocity = Vector3.zero;

    }
}
