using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<Transform> spawnLocations = new List<Transform>();
    public int currentWave;
    public int waveValue;
    public float spawnTimer;
    private float timer;

    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<Transform> chosenLocations = new List<Transform>();

    private int locIter = 0;

    void Start()
    {
        GenerateWave();
        timer = spawnTimer;
    }

    void FixedUpdate()
    {
        if(timer < 0)
        {
            while (enemiesToSpawn.Count > 0)
            {
                Instantiate(enemiesToSpawn[0], chosenLocations[locIter].position, Quaternion.identity);

                enemiesToSpawn.RemoveAt(0);
                chosenLocations[locIter].gameObject.SetActive(false);

                locIter++;
            }

            timer = spawnTimer;
        }
        else
        {
            if (enemiesToSpawn.Count > 0)
            {
                for (int i = 0; i < chosenLocations.Count; i++)
                {
                    chosenLocations[i].gameObject.SetActive(true);
                }
            }
            timer -= Time.fixedDeltaTime;
        }

        // If all the monsters are dead, start a new wave
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && enemiesToSpawn.Count == 0)
        {
            currentWave += 1;
            GenerateWave();
            locIter = 0;
            timer = spawnTimer;
        }
    }

    public void GenerateWave()
    {
        waveValue = currentWave;
        GenerateEnemies();
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject> ();
        List<Transform> tempLocations = new List<Transform>();

        // So it doesn't affect the actual list, but also doesn't double spawn
        List<Transform> tempCopyofSpawnLocations = new List<Transform>(spawnLocations);

        // There are limited places to spawn them
        while (waveValue > 0 && generatedEnemies.Count < tempCopyofSpawnLocations.Count)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            int randLocationId = Random.Range (0, tempCopyofSpawnLocations.Count);

            // If can still add more, add to the list of enemies to generate, then reduce the available value
            if(waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;

                tempLocations.Add(tempCopyofSpawnLocations[randLocationId]);
                tempCopyofSpawnLocations.RemoveAt(randLocationId);
            }

            else if(waveValue <= 0)
            {
                break;
            }
        }

        enemiesToSpawn.Clear();
        chosenLocations.Clear();
        enemiesToSpawn = generatedEnemies;
        chosenLocations = tempLocations;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}