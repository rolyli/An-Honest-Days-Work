using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MinMaxAIMoves;

// Min-max AI for AI teammate (maximizing player)
// Calculate heuristic value of GameState node
// Setting different AIType changes heuristic values of the agents
// For example, with AIType.Defensive, defending farm animals are valued more than hunting foxes
// Chickens always valued more than cows due to lower HP
// Node heuristic value is calculated as (cows.Count * this.heuristicValues.cowValue) + (chickens.count * this.heuristicValues.chickenValue) - (enemies.Count * this.heuristicValues.foxValue) in GameState.Evaluate()
public class MinMax
{
    public GameState parentGameState;

    public MinMax(GameState parentGameState)
    {
        this.parentGameState = parentGameState;
    }

    void RecurseFromMaxNode(GameState childGameState, GameState parentGameState, int depth, MaximizingMoves move)
    {
        GenerateMinMaxTree(childGameState, depth - 1, false);
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
        GenerateMinMaxTree(childGameState, depth - 1, true);
        childGameState.Evaluate();

        //Debug.Log($"eval: {childGameState.Eval}, maxeval: {childGameState.MaxEval}");

        if (childGameState.Eval < parentGameState.MaxEval)
        {
            parentGameState.MaxEval = childGameState.Eval;
        }
    }

    // Recursive MinMax algorithm
    public GameState GenerateMinMaxTree(GameState parentGameState, int depth, bool maximizingPlayer)
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
                    case MaximizingMoves.AttackFox:
                        {
                            // Assume attacking the enemy also results in loss of friendly due to not
                            GameState childGameState = GameState.Clone(parentGameState);

                            childGameState.AttackEnemy();
                            childGameState.AttackFriendlyCow();
                            childGameState.AttackFriendlyChicken();

                            //Debug.Log($"childgamestate enemies: {childGameState.enemies.Count} parentgamestate enemies: {parentGameState.enemies.Count}");



                            RecurseFromMaxNode(childGameState, parentGameState, depth, move);
                        }
                        break;

                    case MaximizingMoves.DefendCow:
                        {
                            // Assume defending cow results in loss of chicken
                            GameState childGameState = GameState.Clone(parentGameState);

                            childGameState.AttackFriendlyChicken();

                            RecurseFromMaxNode(childGameState, parentGameState, depth, move);
                        }
                        break;

                    case MaximizingMoves.DefendChicken:
                        {
                            // Assume defending chicken results in loss of cow
                            GameState childGameState = GameState.Clone(parentGameState);

                            childGameState.AttackFriendlyCow();

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
            parentGameState.MaxEval = GameState.inf;

            foreach (MinimizingMoves move in Enum.GetValues(typeof(MinimizingMoves)))
            {
                switch (move)
                {
                    case MinimizingMoves.AttackCow:
                        {
                            GameState childGameState = GameState.Clone(parentGameState);

                            childGameState.AttackFriendlyCow();

                            RecurseFromMinNode(childGameState, parentGameState, depth);
                        }
                        break;

                    case MinimizingMoves.AttackChicken:
                        {
                            GameState childGameState = GameState.Clone(parentGameState);

                            childGameState.AttackFriendlyChicken();

                            RecurseFromMinNode(childGameState, parentGameState, depth);
                        }
                        break;

                    case MinimizingMoves.Flee:
                        {
                            // Fleeing  results in no change of GameState
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
}
