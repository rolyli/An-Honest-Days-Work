using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody : MonoBehaviour
{
    public float inverseMass = 1.0f;
    public Vector3 velocity;
    public Vector3 acceleration;
    public float e = 1.0f;
    public bool gravity = false;
    public bool resolveCollision = true;
    public List<Collision> collisionBuffer;
    public float impulse;
    public GameObject collisionObject;



    // Start is called before the first frame update
    void Start()
    {
        // Lazy instantiate AABBAABBCollisionManager Singleton
        AABBAABBCollisionManager.Instance.Singleton();

        collisionBuffer = new List<Collision>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Semi implicit euler
        if (gravity == true)
        {
            velocity += -transform.up;
        }
        velocity += acceleration * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }

    private void LateUpdate()
    {
        collisionBuffer.Clear();
    }
}
