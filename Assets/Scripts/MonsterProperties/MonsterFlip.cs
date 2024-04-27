using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlip : MonoBehaviour
{
    private Vector2 direction;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            direction = transform.position - player.transform.position;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
