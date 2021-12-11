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
    public List<GameObject> friendlyCows { get; set; }
    public List<GameObject> friendlyChickens { get; set; }
    public List<GameObject> enemies { get; set; }

    public int Eval;
    public int MaxEval;

    public static int inf = 9999999; // winning or losing condition
    public HeuristicValues heuristicValues; // enemy or fox heuristic values
    public MaximizingMoves MaxEvalMove; // the actual move to take as the AI

    public GameState()
    {
        this.heuristicValues = new HeuristicValues(HeuristicValues.AIType.Aggressive);
        friendlyCows = new List<GameObject>();
        friendlyChickens = new List<GameObject>();

        enemies = new List<GameObject>();

        foreach (GameObject friendlyCow in GameObject.FindGameObjectsWithTag("Friendly"))
        {
            friendlyCows.Add(friendlyCow);
        }

        foreach (GameObject friendlyChicken in GameObject.FindGameObjectsWithTag("FriendlyChicken"))
        {
            friendlyChickens.Add(friendlyChicken);
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);
        }
    }

    // This constructor is used for cloning parentGameState into childGameState
    // e.g. GameState childGameState = GameState.Clone(parentGameState);

    public GameState(List<GameObject> friendlyCows, List<GameObject> friendlyChickens, List<GameObject> enemies, int eval, int maxEval, MaximizingMoves maxEvalMove)
    {
        this.heuristicValues = new HeuristicValues(HeuristicValues.AIType.Defensive);
        this.friendlyCows = friendlyCows;
        this.friendlyChickens = friendlyChickens;
        this.enemies = enemies;
        this.Eval = eval;
        this.MaxEval = maxEval;
        this.MaxEvalMove = maxEvalMove;
    }

    // Heuristic evaluation
    public GameState Evaluate()
    {
        // more cow = good
        // more enemy = bad
        // Debug.Log($"evaluater... friendly count {friendlies.Count} enemyCount {enemies.Count}");
        
        // Loss condition
        if (friendlyCows.Count == 0)
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
            Eval = (((friendlyCows.Count * this.heuristicValues.cowValue) + (friendlyChickens.Count * this.heuristicValues.chickenValue))
                - (enemies.Count * this.heuristicValues.foxValue));
        }
        return this;
    }

    public void AttackFriendlyCow()
    {
        if (friendlyCows.Count > 0)
        {
            friendlyCows.RemoveAt(friendlyCows.Count - 1);
        }
    }

    public void AttackFriendlyChicken()
    {
        if (friendlyChickens.Count > 0)
        {
            friendlyChickens.RemoveAt(friendlyChickens.Count - 1);
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
        List<GameObject> childFriendlyCows = new List<GameObject>();
        List<GameObject> childFriendlyChickens = new List<GameObject>();

        foreach (GameObject enemy in parentGameState.enemies)
        {
            childEnemies.Add(enemy);
        }

        foreach (GameObject friendlyCow in parentGameState.friendlyCows)
        {
            childFriendlyCows.Add(friendlyCow);
        }

        foreach (GameObject friendlyChicken in parentGameState.friendlyChickens)
        {
            childFriendlyChickens.Add(friendlyChicken);
        }

        return new GameState(childFriendlyCows, childFriendlyChickens, childEnemies, parentGameState.Eval, parentGameState.MaxEval, parentGameState.MaxEvalMove);
    }
}