using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    public List<FlockUnit> cohesionNeighbors = new List<FlockUnit>();
    public List<FlockUnit> avoidanceNeighbors = new List<FlockUnit>();
    public List<FlockUnit> alignmentNeighbors = new List<FlockUnit>();


    private Flock assignedFlock;
    public float smoothDamp;
    public Vector3 velocity;
    public float speed = 1f;

    public RigidBody rigidBody;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        // rigidBody = gameObject.GetComponent<RigidBody>();

    }

    public void AssignFlock(Flock flock)
    {
        assignedFlock = flock;
    }

    public void MoveUnit()
    {
        FindNeighbors();
        CalculateSpeed();

        var cohesionVector = CalculateCohesionVector() * assignedFlock.cohesionWeight;
        var avoidanceVector = CalculateAvoidanceVector() * assignedFlock.avoidanceWeight;
        var alignmentVector = CalculateAlignmentVector() * assignedFlock.alignmentWeight;

        var moveVector = cohesionVector + avoidanceVector + alignmentVector;

        moveVector = Vector3.SmoothDamp(transform.forward, moveVector, ref velocity, smoothDamp);

        if (float.IsNaN(velocity.x) && float.IsNaN(velocity.y) && float.IsNaN(velocity.z))
        {
            velocity = Vector3.zero;
        }

        moveVector = moveVector.normalized * speed;

        if (moveVector == Vector3.zero)
        {
            moveVector = transform.forward;
        }

        if (moveVector.magnitude > float.Epsilon)
        {
            transform.forward = moveVector;
            transform.position += moveVector * Time.deltaTime;
        }
    }

    // Add to cohesion, avoidance, and alignmentNeighbors
    public void FindNeighbors()
    {
        cohesionNeighbors.Clear();
        alignmentNeighbors.Clear();
        avoidanceNeighbors.Clear();



        foreach (FlockUnit unit in assignedFlock.chickens)
        {
            if (unit != this)
            {
                float neighborDistance = Vector3.SqrMagnitude(unit.transform.position - transform.position);
                if (neighborDistance <= assignedFlock.cohesionDistance * assignedFlock.cohesionDistance)
                {
                    cohesionNeighbors.Add(unit);
                }

                if (neighborDistance <= assignedFlock.avoidanceDistance * assignedFlock.avoidanceDistance)
                {
                    avoidanceNeighbors.Add(unit);
                }

                if (neighborDistance <= assignedFlock.alignmentDistance * assignedFlock.alignmentDistance)
                {
                    alignmentNeighbors.Add(unit);
                }
            }
        }
    }

    private void CalculateSpeed()
    {
        if (cohesionNeighbors.Count == 0)
        {
            return;
        }

        speed = 0f;

        foreach (FlockUnit unit in cohesionNeighbors)
        {
            speed += unit.speed;
        }

        speed /= cohesionNeighbors.Count;
        Mathf.Clamp(speed, assignedFlock.minSpeed, assignedFlock.maxSpeed);
    }

    // cohesion vector is direction to sum of all vectors from unit to flock neighbor
    private Vector3 CalculateCohesionVector()
    {
        var cohesionVector = Vector3.zero;
        if (cohesionNeighbors.Count == 0)
        {
            return Vector3.zero;
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

    // avoidance vector is sum of all vectors from each flock neighbor unit to player
    private Vector3 CalculateAvoidanceVector()
    {
        var avoidanceVector = Vector3.zero;
        if (cohesionNeighbors.Count == 0)
        {
            return avoidanceVector;
        }

        foreach (FlockUnit unit in cohesionNeighbors)
        {
            avoidanceVector += transform.position - unit.transform.position;
        }

        // calculate average from sum
        avoidanceVector /= avoidanceNeighbors.Count;

        return avoidanceVector.normalized;

        //skip fov condition
    }

    // alignment vector is sum of all forward vectors of each neighbor unit
    private Vector3 CalculateAlignmentVector()
    {
        if (alignmentNeighbors.Count == 0)
        {
            return transform.forward;
        }

        var alignmentVector = transform.forward;

        foreach (FlockUnit unit in alignmentNeighbors)
        {
            alignmentVector += unit.transform.forward;
        }

        // calculate average from sum
        alignmentVector /= alignmentNeighbors.Count;
        return alignmentVector.normalized;
    }


}
