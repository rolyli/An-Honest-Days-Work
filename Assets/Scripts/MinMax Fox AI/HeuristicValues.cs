using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeuristicValues
{
    public int cowValue;
    public int foxValue;
    public int chickenValue;

    public enum AIType
    {
        Aggressive, // Prioritize foxes
        Defensive // Prioritize farm animals
    };

    public HeuristicValues(AIType aiType)
    {
        switch (aiType)
        {
            case AIType.Defensive:
                cowValue = 200;
                foxValue = 100;
                chickenValue = 300;
                break;
            case AIType.Aggressive:
                cowValue = 100;
                foxValue = 300;
                chickenValue = 200;
                break;
            default:
                break;
        }
    }

}
