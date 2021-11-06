using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Every spawnTime seconds, there is stochasticRate rate of an enemy being spawned
    public float enemyStochasticRate = 0.5f;
    public float burgerStochasticRate = 0.5f;

    public float enemySpawnTime = 10.0f;
    public float burgerSpawnTime = 20.0f;

    public GameObject[] enemyList;
    public GameObject[] friendlyList;
    

    private GameObject player;

    public GameObject enemyPrefab;
    public GameObject positiveFeedbackPrefab;
    

    public bool win = false;
    public bool lose = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("SpawnEnemy", 0.1f, enemySpawnTime);
        InvokeRepeating("SpawnBurger", 0.1f, burgerSpawnTime);
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-10.0f, 10.0f), 3, Random.Range(-10.0f, 10.0f));
    }
    void SpawnEnemy()
    {
        if (Random.value > (1 - enemyStochasticRate))
        {
            GameObject enemy = Instantiate(enemyPrefab, RandomPosition(), Quaternion.identity);
            enemy.GetComponent<RigidBody>().velocity = Random.insideUnitSphere;
        }
    }

    // Eating burgs increases mass -> more impulse on collision with enemy -> more damage done to hit point
    // Hence it is a positive feedback loop
    void SpawnBurger()
    {
        if (Random.value > (1 - burgerStochasticRate))
        {
           Instantiate(positiveFeedbackPrefab, RandomPosition(), Quaternion.identity);
        }
    }

    private void Update()
    {
        friendlyList = GameObject.FindGameObjectsWithTag("Friendly");
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyList.Length == 0)
        {
            win = true;
            SceneManager.LoadScene("Overworld");
        }

        if (friendlyList.Length == 0)
        {
            lose = true;
            // tc: lose logic
        }
    }
}
