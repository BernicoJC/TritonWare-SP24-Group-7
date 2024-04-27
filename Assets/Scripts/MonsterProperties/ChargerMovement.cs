using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerMovement : MonoBehaviour
{
    
    public float speed;
    public float distanceToActivate;
    public float chargeTime;
    public float pauseTime;
    public float extraRechargeTime;

    private GameObject player;
    private float distance;
    private bool charging = false;

    private float timer = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (charging == true) // for when it goes berserk and only wants blood
        {
            timer += Time.deltaTime;
            if(timer > pauseTime + chargeTime + extraRechargeTime) // Recharging done
            {
                timer = 0;
                charging = false;
            }

            else if (timer > pauseTime + chargeTime)
            {

            }

            else if (timer > pauseTime)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }

        if (charging == false && distance < distanceToActivate) // detect a player
        {
            charging = true;
        }

        if (charging == false && distance >= distanceToActivate) // detect a player
        {
            timer = 0;
        }
    }
}
