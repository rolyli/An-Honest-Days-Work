using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TMPro.TextMeshPro textGO;
    private RigidBody playerRB;
    private GameManager gameManager;

    // Start is called before the first frame update

    void Start()
    {
        textGO = gameObject.GetComponent<TMPro.TextMeshPro>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<RigidBody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var mass = (1 / playerRB.inverseMass);
        var enemySpawnTime = gameManager.enemySpawnTime;
        var enemySpawnRate = 1 - gameManager.enemyStochasticRate;
        textGO.SetText($"Mass: {mass}\n" +
            $"Fox spawn rate: {enemySpawnRate} every {enemySpawnTime} seconds\n" +
            $"Foxes: {gameManager.enemyList.Length}\n");
    }
}
