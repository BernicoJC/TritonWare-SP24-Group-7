using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthDisplay : MonoBehaviour
{
    private GameObject player;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            slider.maxValue = player.GetComponent<Health>().maxHealth;
            slider.value = player.GetComponent<Health>().health;
        }
        else
        {
            slider.value = 0;
        }
    }
}
