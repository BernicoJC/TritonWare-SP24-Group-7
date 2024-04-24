using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterBullet : MonoBehaviour
{
    public float speed;
    public float distanceToRelease;

    private GameObject player;
    private Rigidbody2D rb;
    private float distance;

    private bool released;
    private Vector3 releasedDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance > distanceToRelease && !released)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            Vector3 direction = player.transform.position - transform.position;
            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }
        if(distance < distanceToRelease && !released)
        {
            releasedDirection = player.transform.position - transform.position;
            rb.velocity = new Vector2(releasedDirection.x, releasedDirection.y).normalized * speed;
            released = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) // MAKE SURE GROUND IS 6
        {
            Destroy(gameObject);
        }
    }
}
