using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MinMaxAIMoves;

// Min-max AI for AI teammate (maximizing player)
// Calculate heuristic value of GameState node
// Setting different AIType changes heuristic values of the agents
// For example, with AIType.Defensive, defending farm animals are valued more than hunting foxes
// Node heuristic value is calculated as (friendlies.Count * this.heuristicValues.cowValue) - (enemies.Count * this.heuristicValues.foxValue) in GameState.Evaluate()
public class MinMaxAI : MonoBehaviour
{
    public GameState gameState;

    void RecurseFromMaxNode(GameState childGameState, GameState parentGameState, int depth, MaximizingMoves move)
    {
        MinMax(childGameState, depth - 1, false);
        childGameState.Evaluate();

        // Debug.Log($"eval: {childGameState.Eval}");

        if (childGameState.Eval > parentGameState.MaxEval)
        {
            parentGameState.MaxEval = childGameState.Eval;
            parentGameState.MaxEvalMove = move;
        }
    }
    void RecurseFromMinNode(GameState childGameState, GameState parentGameState, int depth)
    { 
        MinMax(childGameState, depth - 1, true);
        childGameState.Evaluate();

        // Debug.Log($"eval: {childGameState.Eval}");

        if (childGameState.Eval < parentGameState.MinEval)
        {
            parentGameState.MinEval = childGameState.Eval;
        }
    }

    // Recursive MinMax algorithm
    GameState MinMax(GameState parentGameState, int depth, bool maximizingPlayer)
    { 
        // End of tree reached
        if (depth == 0)
        {
            return parentGameState.Evaluate();
        }

        // Maximizing player
        if (maximizingPlayer == true)
        {
            parentGameState.MaxEval = -GameState.inf;

            foreach(MaximizingMoves move in Enum.GetValues(typeof(MaximizingMoves)))
            {                
                switch (move)
                {
                    case MaximizingMoves.Attack:
                        {
                            // Assume attacking the enemy also results in loss of friendly due to not
                            GameState childGameState = GameState.Clone(parentGameState);

                            childGameState.AttackEnemy();
                            childGameState.AttackFriendly();

                            //Debug.Log($"childgamestate enemies: {childGameState.enemies.Count} parentgamestate enemies: {parentGameState.enemies.Count}");

                            RecurseFromMaxNode(childGameState, parentGameState, depth, move);
                        }
                        break;

                    case MaximizingMoves.Defend:
                        {
                            // Defending results in no change in game state as of yet

                            GameState childGameState = GameState.Clone(parentGameState);

                            RecurseFromMaxNode(childGameState, parentGameState, depth, move);
                        }
                        break;

                    default:
                        break;
                }
            }

            return parentGameState;

        }

        // Minimizing player
        else
        {
            parentGameState.MinEval = GameState.inf;

            foreach (MinimizingMoves move in Enum.GetValues(typeof(MinimizingMoves)))
            {
                switch (move)
                {
                    case MinimizingMoves.Attack:
                        {
                            // Assume attacking the enemy also results in loss of friendly due to not
                            GameState childGameState = GameState.Clone(parentGameState);

                            childGameState.AttackFriendly();

                            //Debug.Log($"childgamestate enemies: {childGameState.enemies.Count} parentgamestate enemies: {parentGameState.enemies.Count}");

                            RecurseFromMinNode(childGameState, parentGameState, depth);
                        }
                        break;

                    case MinimizingMoves.Flee:
                        {
                            // Fleeing  results in no change in game state as of yet

                            GameState childGameState = GameState.Clone(parentGameState);

                            RecurseFromMinNode(childGameState, parentGameState, depth);
                        }
                        break;

                    default:
                        break;
                }
            }

            return parentGameState;
        }

    }
    private void Start()
    {
        gameState = new GameState();
    }
    private void Update()
    {
        gameState = new GameState();
        var minMax = MinMax(gameState, 5, true);

        //Debug.Log($"{gameState.friendlies.Count} {gameState.enemies.Count}");
        //Debug.Log($"{minMax.MaxEval}, friendly: {gameState.friendlies.Count} enemey: {gameState.enemies.Count} {minMax.MaxEvalMove}");
    }
}
