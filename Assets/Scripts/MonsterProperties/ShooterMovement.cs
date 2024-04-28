using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMovement : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;


    private GameObject player;
    public float speed;
    public float distanceToActivate;
    public float pauseTime;

    private float distance;
    private bool shooting = false;

    private float timer = 0;

    public Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (shooting == true)
        {
            timer += Time.deltaTime;

            if (timer > pauseTime)
            {
                shoot();
                timer = 0;
                shooting = false;
                animator.SetBool("Detected", false);
            }
        }

        if (shooting == false && distance < distanceToActivate) // detect a player
        {
            animator.SetBool("Detected", true);
            shooting = true;
        }

        if (shooting == false && distance >= distanceToActivate) // detect a player
        {
            animator.SetBool("Detected", false);
            timer = 0;
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
