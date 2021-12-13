using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxUIManager : MonoBehaviour
{
    private MinMaxPathfindingAgent minMaxPathfindingAgent;
    private TMPro.TextMeshPro textGO;


    // Start is called before the first frame update
    void Start()
    {
        textGO = gameObject.GetComponent<TMPro.TextMeshPro>();
        minMaxPathfindingAgent = FindObjectOfType<MinMaxPathfindingAgent>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // shorthand
        var gs = minMaxPathfindingAgent.gameState;

        textGO.SetText($"<style=\"H1\">MinMax AI</style>\n\n" +
            $"aiType: <b>{minMaxPathfindingAgent.aiType}</b>\n" +
            $"MaxEvalMove: {minMaxPathfindingAgent.gameState.MaxEvalMove}\n" +
            $"MaxEval: {minMaxPathfindingAgent.gameState.MaxEval}\n" +
            $"friendlyCows: {gs.friendlyCows.Count}\n" +
            $"friendlyChickens: {gs.friendlyChickens.Count}\n" +
            $"enemies: {gs.enemies.Count}\n" +
            $"<style=\"C1\">heuristicValues</style>\n"+
            $"cowValue: {gs.heuristicValues.cowValue}\n"+
            $"chickenValue: {gs.heuristicValues.chickenValue}\n" +
            $"foxValue: {gs.heuristicValues.foxValue}");
    }
}
