using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public List<FlockUnit> chickens = new List<FlockUnit>();

    [Range(0, 10)]
    public int cohesionDistance;

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
