using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeuristicValues
{
    public int cowValue;
    public int foxValue;

    public enum AIType
    {
        Aggressive, // Foxes are twice as more important than farm animals
        Defensive // Farm animals are twice as more important than foxes
    };

    public HeuristicValues(AIType aiType)
    {
        switch (aiType)
        {
            case AIType.Defensive:
                cowValue = 200;
                foxValue = 100;
                break;
            default:
                break;
        }
    }

}
