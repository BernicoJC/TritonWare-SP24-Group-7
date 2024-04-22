using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is supposed to be universal for any monsters added
public class MonsterDamage : MonoBehaviour
{
    public int contactDamage;
    public Health playerHealth; // It seems using class in this way is importing from other script (just like Java)
    public bool kamikaze;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.takeDamage(contactDamage);

            if(kamikaze)
            {
                Destroy(gameObject);
            }
        }
    }
}
