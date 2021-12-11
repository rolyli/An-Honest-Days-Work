using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    public List<FlockUnit> cohesionNeighbors = new List<FlockUnit>();
    private Flock assignedFlock;
    public float smoothDamp;
    public Vector3 velocity;
    public float speed = 1;

    public RigidBody rigidBody;

    private void Awake()
    {
        // rigidBody = gameObject.GetComponent<RigidBody>();
    }

    public void AssignFlock(Flock flock)
    {
        assignedFlock = flock;
    }

    public void MoveUnit()
    {
        FindNeighbors();
        var cohesionVector = CalculateCohesionVector();
        var moveVector = Vector3.SmoothDamp(transform.forward, cohesionVector, ref velocity, smoothDamp);
        moveVector = moveVector.normalized * speed;
        transform.forward = moveVector;
        transform.position += moveVector * Time.deltaTime;
    }

    public void FindNeighbors()
    {
        cohesionNeighbors.Clear();
        foreach (FlockUnit unit in assignedFlock.chickens)
        {
            if (unit != this)
            {
                float neighborDistance = Vector3.SqrMagnitude(unit.transform.position - transform.position);
                if (neighborDistance <= assignedFlock.cohesionDistance * assignedFlock.cohesionDistance)
                {
                    cohesionNeighbors.Add(unit);
                }
            }
        }
    }

    private Vector3 CalculateCohesionVector()
    {
        var cohesionVector = Vector3.zero;
        if (cohesionNeighbors.Count == 0)
        {
            return cohesionVector;
        }

        foreach (FlockUnit unit in cohesionNeighbors)
        {
            cohesionVector += unit.transform.position;
        }

        // calculate average from sum
        cohesionVector /= cohesionNeighbors.Count;
        
        // calculate vector from flockunit
        cohesionVector -= transform.position;

        cohesionVector = Vector3.Normalize(cohesionVector);

        return cohesionVector;

        // skip fov condition
    }
}
