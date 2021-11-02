using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 acceleration;
    public RigidBody rigidBody;

    // Player specific
    public float force = 1f;
    public float maxSpeed = 5f;
    private int rotationSpeed = 50;

    private void FixedUpdate()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput.Normalize();

        // Semi implicit euler
        acceleration = transform.right * playerInput.y * force;
        rigidBody.velocity += acceleration * Time.fixedDeltaTime;

        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }

        // Rotate car on user input
        if (playerInput.x > 0 || playerInput.x < 0)
        {
            transform.Rotate(Vector3.up, playerInput.x * Time.deltaTime * rotationSpeed);

            //  Make sure player always moves in the forward direction
            //  When gravity enabled, velocity.magnitude = 1 even if car is stationary on the XZ plane
            if (rigidBody.gravity == true)
            {
                // Vehicle local axis is wrong way around
                Vector3 forwardTransform = transform.right.normalized;

                Vector3 XZVelocity = rigidBody.velocity;
                XZVelocity.y = 0;
                Debug.Log(XZVelocity + " " + rigidBody.velocity);

                // Conserve previous y velocity
                Vector3 newVelocity;
                
                newVelocity = forwardTransform * XZVelocity.magnitude;
                newVelocity.y = rigidBody.velocity.y;

                // Vehicle is reversing
                if (Vector3.Dot(forwardTransform, XZVelocity) < 0 )
                {
                    newVelocity = -forwardTransform * XZVelocity.magnitude;

                }

                rigidBody.velocity = newVelocity;
            } 
            else
            {
                rigidBody.velocity = transform.right.normalized * rigidBody.velocity.magnitude;
            }
        }

    }
}
