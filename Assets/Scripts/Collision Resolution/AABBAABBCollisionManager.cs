using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionInfo
{
    public CollisionInfo(Vector3 normal, float penetration)
    {
        this.normal = normal;
        this.penetration = penetration;
    }
    public Vector3 normal { get; set; }
    public float penetration { get; set; }
}

public class AABBAABBCollisionManager : MonoBehaviour
{
    public List<GameObject> AABBObjects;

    // Start is called before the first frame update
    void Start()
    {

        //todo: breaks if object has two box collider components, check for this condition
        BoxCollider[] rigidBodyArray = GameObject.FindObjectsOfType<BoxCollider>();
        foreach (BoxCollider rigidBody in rigidBodyArray)
        {
            AABBObjects.Add(rigidBody.gameObject);
        }

    }

    // Check collision and if collision is occuring, return CollisionInfo object to be consumed by ResolveCollision method
    CollisionInfo CheckCollision(BoxCollider boxA, BoxCollider boxB)
    {
        bool isColliding = false;

        Vector3 minBoundsA = boxA.bounds.min;
        Vector3 maxBoundsA = boxA.bounds.max;

        Vector3 minBoundsB = boxB.bounds.min;
        Vector3 maxBoundsB = boxB.bounds.max;


        if (
            (minBoundsA.x <= maxBoundsB.x && maxBoundsA.x >= minBoundsB.x) &&
            (minBoundsA.y <= maxBoundsB.y && maxBoundsA.y >= minBoundsB.y) &&
            (minBoundsA.z <= maxBoundsB.z && maxBoundsA.z >= minBoundsB.z)
        )
        {
            isColliding = true;
        }

        if (GameObject.ReferenceEquals(boxA, boxB))
        {
            isColliding = false;
        }


        //boxA.GetComponent<Renderer>().material.color = isColliding ? Color.red : Color.white;
        //boxB.GetComponent<Renderer>().material.color = isColliding ? Color.red : Color.white;

        if (isColliding)
        {
            Vector3 sizeSum = boxA.bounds.extents + boxB.bounds.extents;

            Vector3 deltaPosition = boxB.transform.position - boxA.transform.position;

            float minPenetration = sizeSum.x - Mathf.Abs(deltaPosition.x);
            int minAxis = 0;

            for (int i = 0; i < 3; i++)
            {
                float axisPenetration = sizeSum[i] - Mathf.Abs(deltaPosition[i]);
                if (axisPenetration < minPenetration)
                {
                    minPenetration = axisPenetration;
                    minAxis = i;
                }
            }

            Vector3 normal = Vector3.zero;
            normal[minAxis] = deltaPosition[minAxis] < 0 ? -1.0f : 1.0f;

            //Debug.Log("Boxes intersecting  by " + minPenetration);
            //Debug.DrawLine(boxA.transform.position, boxA.transform.position + (normal * 5.0f), Color.green);

            return new CollisionInfo(normal, minPenetration);
        }

        return null;
    }

    // Calculate and apply projection amount
    float ProjectAndResolveCollision(GameObject objA, GameObject objB, CollisionInfo collisionInfo, bool resolveCollision)
    {
        float objAMass = objA.GetComponent<RigidBody>().inverseMass;
        float objBMass = objB.GetComponent<RigidBody>().inverseMass;
        Vector3 objAVelocity = objA.GetComponent<RigidBody>().velocity;
        Vector3 objBVelocity = objB.GetComponent<RigidBody>().velocity;

        //Get minimum value of coefficient of restitution between object A and B
        float objAE = objA.GetComponent<RigidBody>().e;
        float objBE = objB.GetComponent<RigidBody>().e;
        float elasticity = Mathf.Min(objAE, objBE);

        Vector3 collisionNormal = collisionInfo.normal;
        float penetration = collisionInfo.penetration;

        // Calculate and apply projection amount
        float mTotal = objAMass + objBMass;
        Vector3 projA = collisionNormal * penetration * (objAMass / mTotal);
        Vector3 projB = collisionNormal * penetration * (objBMass / mTotal);



        objA.transform.position -= projA;
        objB.transform.position += projB;


        float impulse = 0;
        // Calculate and apply impulse
        if (resolveCollision)
        {
            Vector3 relV = objBVelocity - objAVelocity;
            impulse = (-(1.0f + elasticity) * Vector3.Dot(relV, collisionNormal)) / mTotal;
            //Debug.Log("Impulse: " + impulse);

            objA.GetComponent<RigidBody>().velocity -= objAMass * impulse * collisionNormal;
            objB.GetComponent<RigidBody>().velocity += objBMass * impulse * collisionNormal;
        }

        return impulse;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update collider object list every frame
        AABBObjects.Clear();
        BoxCollider[] rigidBodyArray = GameObject.FindObjectsOfType<BoxCollider>();
        foreach (BoxCollider rigidBody in rigidBodyArray)
        {
            AABBObjects.Add(rigidBody.gameObject);
        }

        // Check collision between every item
        if (AABBObjects.Count > 1)
        {
            for (int i = 0; i < AABBObjects.Count - 1; i++)
            {
                for (int j = i + 1; j < AABBObjects.Count; j++)
                {
                    RigidBody boxA = AABBObjects[i].GetComponent<RigidBody>();
                    RigidBody boxB = AABBObjects[j].GetComponent<RigidBody>();




                    bool resolveCollisionEnabled = boxA.resolveCollision & boxB.resolveCollision;

                    CollisionInfo collisionInfo = CheckCollision(AABBObjects[i].GetComponent<BoxCollider>(), AABBObjects[j].GetComponent<BoxCollider>());

                    if (collisionInfo != null)
                    {
                        float impulse = ProjectAndResolveCollision(AABBObjects[i], AABBObjects[j], collisionInfo, resolveCollisionEnabled);
                        boxA.collisionInfo = collisionInfo;
                        boxA.impulse = impulse;
                        boxA.isColliding = true;
                        boxA.collisionObject = boxB.gameObject;


                        boxB.collisionInfo = collisionInfo;
                        boxB.impulse = impulse;
                        boxB.isColliding = true;
                        boxB.collisionObject = boxA.gameObject;
                    }
                }
            }
        }
    }
}


