using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int knockbackStrength = 2;
    public int damage = 1;
    public bool x; // if true, only consider x axis for knockback

    private void OnTriggerEnter2D(Collider2D collided) // Built in function, (if collides basically)
    {
        // GetComponent is finding for the collider's script basically
        if (collided.GetComponent<Health>() != null && collided.tag == "Enemy") // ONLY if what's colliding has a health; also, it's making sure it's hitting an enemy
        {
            // Deal damage
            Health health = collided.GetComponent<Health>();
            health.takeDamage(damage);

            // Deal Knockback
            Vector2 vect = (collided.transform.position - transform.parent.transform.position).normalized * knockbackStrength;

            MonsterKnockbacked knock = collided.GetComponent<MonsterKnockbacked>();
            knock.takeKnockback(vect, x);
        }
    }
}
