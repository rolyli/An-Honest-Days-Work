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
    public List<Collision> collisionBuffer;
    public float impulse;
    public GameObject collisionObject;
    [Header("Workaround for flocks")]
    [SerializeField]
    private bool disableAcceleration = false;
    public bool resolveCollision = true;
    public bool detectCollision = true;




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

        // Workaround for flocks as they don't use rigid body euler integration
        if (disableAcceleration)
        {
            acceleration = Vector3.zero;
        }

        velocity += acceleration * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }

    private void LateUpdate()
    {
        collisionBuffer.Clear();
    }
}
