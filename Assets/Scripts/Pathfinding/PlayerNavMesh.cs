using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField]
    private Vector3 movePositionTransform;

    public GameState gameState;

    private MinMax minMax;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Navigate to MinMax output
        gameState = new GameState();
        minMax = new MinMax(gameState);
        minMax.GenerateMinMaxTree(gameState, 2, true);
        Debug.Log($"{gameState.MaxEvalMove} {gameState.MaxEval}");

        switch (gameState.MaxEvalMove)
        {
            case MinMaxAIMoves.MaximizingMoves.AttackFox:
                if (gameState.enemies.Count > 0)
                {
                    movePositionTransform = gameState.enemies[0].transform.position;
                }
                break;
            case MinMaxAIMoves.MaximizingMoves.DefendCow:
                if (gameState.friendlyCows.Count > 0)
                {
                    movePositionTransform = gameState.friendlyCows[0].transform.position;
                }
                break;
            case MinMaxAIMoves.MaximizingMoves.DefendChicken:
                if (gameState.friendlyChickens.Count > 0)
                {
                    movePositionTransform = gameState.friendlyChickens[0].transform.position;
                }
                break;
            default:
                break;
        }

        navMeshAgent.destination = movePositionTransform;
    }
}
