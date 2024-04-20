using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public float attackTime = 0.05f;
    public float attackCooldown = 0.5f;
    [SerializeField] private Rigidbody2D rb;
    

    private GameObject attackArea = default;
    private bool attacking = false;

    private float timeToAttack;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeToAttack = attackTime; // setup
        attackArea = transform.GetChild(1).gameObject; // getting the hitbox child of this object; MAKE SURE TO PUT THE HITBOX IN THE 1ST SPOT (starting from 0)
        //attackArea.transform.localPosition = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && !attacking)
        {
            Attack();
        }

        if(attacking)
        {
            timer += Time.deltaTime; // timer that goes up per frame(?) basically
            if(timer >= timeToAttack) // if that timer already goes above the timeToAttack, reset everything
            {
                attackArea.SetActive(false); // Disable hitbox
            }
            if(timer >= timeToAttack + attackCooldown) // Allow to attack again
            {
                timer = 0;
                attacking = false;
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(true);
        
    }
}
