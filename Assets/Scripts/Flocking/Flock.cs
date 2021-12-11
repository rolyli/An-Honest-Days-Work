using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public List<FlockUnit> chickens = new List<FlockUnit>();

    [Header("Distances")]
    [Range(0, 10)]
    public int cohesionDistance;
    [Range(0, 10)]
    public int avoidanceDistance;
    [Range(0, 10)]
    public int alignmentDistance;

    [Header("Weights")]
    [Range(0, 10)]
    public float cohesionWeight;
    [Range(0, 10)]
    public float avoidanceWeight;
    [Range(0, 10)]
    public float alignmentWeight;

    public float minSpeed;
    public float maxSpeed;

    private void Start()
    {
        AssignUnits();
    }

    private void Update()
    {
        foreach(FlockUnit chicken in chickens)
        {
            chicken.MoveUnit();
        }
    }

    private void AssignUnits()
    {
        var chickenTagGOs = GameObject.FindGameObjectsWithTag("FriendlyChicken");
        List<GameObject> chickenGOs = new List<GameObject>();

        foreach (GameObject chicken in chickenTagGOs)
        {
            chickenGOs.Add(chicken.transform.parent.gameObject);
        }


        foreach (GameObject chicken in chickenGOs)
        {
            chickens.Add(chicken.GetComponent<FlockUnit>());
        }

        foreach (FlockUnit unit in chickens)
        {
            unit.AssignFlock(this);
        }
    }




    
}
