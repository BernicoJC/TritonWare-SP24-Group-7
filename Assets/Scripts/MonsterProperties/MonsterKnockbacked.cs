using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKnockbacked : MonoBehaviour
{
    public int resilienceModifier = 1; // Only really changed for bosses

    [SerializeField] private Rigidbody2D rb;

    public void takeKnockback(Vector2 vect, bool x)
    {
        StartCoroutine(doKnockback(vect, x));

    }

    public IEnumerator doKnockback(Vector2 vect, bool x)
    {
        if (x)
        {
            vect.y = 0;
        }
        else
        {
            vect.x = 0;
        }

        rb.velocity = new Vector2(vect.x / resilienceModifier, vect.y / resilienceModifier);

        yield return new WaitForSeconds(0.1f);

        rb.velocity = Vector3.zero;

    }
}
