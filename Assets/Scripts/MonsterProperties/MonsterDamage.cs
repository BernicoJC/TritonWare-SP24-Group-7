using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is supposed to be universal for any monsters added
public class MonsterDamage : MonoBehaviour
{
    public int contactDamage;
    private GameObject player;
    public Health playerHealth; // It seems using class in this way is importing from other script (just like Java)
    public PlayerInvincible playerInvincible;
    public bool kamikaze;

    public string movementScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        playerInvincible = player.GetComponent<PlayerInvincible>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {


            playerInvincible.applyInvincibility();
            playerHealth.takeDamage(contactDamage);

            if(kamikaze)
            {
                if(GetComponent<Death>() != null)
                {
                    GetComponent<Death>().kill();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
