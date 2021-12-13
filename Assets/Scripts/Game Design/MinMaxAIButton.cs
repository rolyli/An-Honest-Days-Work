using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxAIButton : MonoBehaviour
{
    private MinMaxPathfindingAgent minMaxPathfindingAgent;

    private void Awake()
    {
        minMaxPathfindingAgent = FindObjectOfType<MinMaxPathfindingAgent>();
    }

    public void ChangeMinMaxAIType()
    {
        if (minMaxPathfindingAgent.aiType == HeuristicValues.AIType.Aggressive)
        {
            minMaxPathfindingAgent.aiType = HeuristicValues.AIType.Defensive;
            return;
        }

        if (minMaxPathfindingAgent.aiType == HeuristicValues.AIType.Defensive)
        {
            minMaxPathfindingAgent.aiType = HeuristicValues.AIType.Aggressive;
            return;
        }

    }
}
