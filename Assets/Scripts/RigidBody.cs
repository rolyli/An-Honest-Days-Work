using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody : MonoBehaviour
{
    public float inverseMass = 1;
    public Vector3 velocity;
    public Vector3 acceleration;
    public float e = 1;
    public bool gravity = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Semi implicit euler
        if (gravity == true)
        {
            velocity += -transform.up;
            AABBAABBCollisionManager collisionManager = GameObject.FindObjectOfType<AABBAABBCollisionManager>();
            
        }
        velocity += acceleration * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
