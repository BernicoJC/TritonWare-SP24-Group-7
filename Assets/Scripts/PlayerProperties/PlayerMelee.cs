using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public float attackTime = 0.05f;
    public float attackCooldown = 0.5f;


    private GameObject attackAreaF = default;
    private GameObject attackAreaD = default;
    private GameObject attackAreaU = default;

    private bool attacking = false;

    private float timeToAttack;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeToAttack = attackTime; // setup
        attackAreaF = transform.GetChild(1).gameObject; // getting the hitbox child of this object; MAKE SURE TO PUT THE HITBOX IN THE 1ST SPOT (starting from 0)
        attackAreaD = transform.GetChild(2).gameObject; // Down hitbox must be 2nd
        attackAreaU = transform.GetChild(3).gameObject; // Up hitbox must be 3rd

        attackAreaF.transform.localPosition = new Vector3(1.33f, 0, 0);
        attackAreaD.transform.localPosition = new Vector3(0, -1.33f, 0);
        attackAreaU.transform.localPosition = new Vector3(0, 1.33f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        // Up attack
        if (Input.GetKeyDown(KeyCode.X) && !attacking && Input.GetAxisRaw("Vertical") > 0)
        {
            AttackU();
            
        }

        // Down attack
        if (Input.GetKeyDown(KeyCode.X) && !attacking && Input.GetAxisRaw("Vertical") < 0 && !gameObject.GetComponent<PlayerMovement>().isGrounded())
        {
            AttackD();
        }

        // Forward attack
        if (Input.GetKeyDown(KeyCode.X) && !attacking && Input.GetAxisRaw("Vertical") == 0)
        {
            AttackF();
        }

        if(attacking)
        {
            timer += Time.deltaTime; // timer that goes up per frame(?) basically
            if(timer >= timeToAttack) // if that timer already goes above the timeToAttack, reset everything
            {
                attackAreaF.SetActive(false); // Disable hitbox
                attackAreaD.SetActive(false);
                attackAreaU.SetActive(false);
            }
            if(timer >= timeToAttack + attackCooldown) // Allow to attack again
            {
                timer = 0;
                attacking = false;
            }
        }
    }

    private void AttackF()
    {
        attacking = true;
        attackAreaF.SetActive(true);
        
    }

    private void AttackD()
    {
        attacking = true;
        attackAreaD.SetActive(true);

    }

    private void AttackU()
    {
        attacking = true;
        attackAreaU.SetActive(true);

    }
}
