using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static MinMaxAIMoves;

// GameState is an bstraction of the following
// 1) actions available to agents
// 2) state of the game including number of cows, enemies, etc

public class GameState
{
    public List<GameObject> friendlies { get; set; }
    public List<GameObject> enemies { get; set; }

    public int Eval;
    public int MaxEval;
    public int MinEval;
    public static int inf = 9999999; // winning or losing condition
    public HeuristicValues heuristicValues; // enemy or fox heuristic values
    public MaximizingMoves MaxEvalMove; // the actual move to take as the AI

    public GameState()
    {
        this.heuristicValues = new HeuristicValues(HeuristicValues.AIType.Defensive);
        friendlies = new List<GameObject>();
        enemies = new List<GameObject>();

        foreach (GameObject friendly in GameObject.FindGameObjectsWithTag("Friendly"))
        {
            friendlies.Add(friendly);
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);
        }
    }

    // This constructor is used for cloning parentGameState into childGameState
    // e.g. GameState childGameState = GameState.Clone(parentGameState);

    public GameState(List<GameObject> friendlies, List<GameObject> enemies, int eval, int maxEval, int minEval, MaximizingMoves maxEvalMove)
    {
        this.heuristicValues = new HeuristicValues(HeuristicValues.AIType.Defensive);
        this.friendlies = friendlies;
        this.enemies = enemies;
        this.Eval = eval;
        this.MaxEval = maxEval;
        this.MinEval = minEval;
        this.MaxEvalMove = maxEvalMove;
    }

    // Heuristic evaluation
    public GameState Evaluate()
    {
        // more cow = good
        // more enemy = bad
        // Debug.Log($"evaluater... friendly count {friendlies.Count} enemyCount {enemies.Count}");
        
        // Loss condition
        if (friendlies.Count == 0)
        {
            this.Eval = -inf;
        }

        // Win condition
        else if (enemies.Count == 0)
        {
            this.Eval = inf;
        }

        // Heuristic evaluation
        else
        {
            Eval = (friendlies.Count * this.heuristicValues.cowValue) - (enemies.Count * this.heuristicValues.foxValue);
        }
        return this;
    }

    public void AttackFriendly()
    {
        if (friendlies.Count > 0)
        {
            friendlies.RemoveAt(friendlies.Count - 1);
        }
    }

    public void AttackEnemy()
    {
        if (enemies.Count > 0)
        {
            enemies.RemoveAt(enemies.Count - 1);
        }
    }

    // This method is used for cloning parentGameState into childGameState
    // e.g. GameState childGameState = GameState.Clone(parentGameState);
    public static GameState Clone(GameState parentGameState)
    {
        List<GameObject> childEnemies = new List<GameObject>();
        List<GameObject> childFriendlies = new List<GameObject>();

        foreach (GameObject enemy in parentGameState.enemies)
        {
            childEnemies.Add(enemy);
        }

        foreach (GameObject friendly in parentGameState.friendlies)
        {
            childFriendlies.Add(friendly);
        }

        return new GameState(childFriendlies, childEnemies, parentGameState.Eval, parentGameState.MaxEval, parentGameState.MinEval, parentGameState.MaxEvalMove);
    }
}