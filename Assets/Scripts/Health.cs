using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public int health; // Current health

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void takeDamage(int damage) // this parameter will be from the enemy's script
    {
        health -= damage;

        if(health <= 0)
        {
            GetComponent<Death>().kill();
        }
    }
}
