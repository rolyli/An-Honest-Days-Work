using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 velocity;
    public Vector3 force;
    private int rotationSpeed = 50;

    public RigidBody rigidBody;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        
        // Semi implicit euler
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput.Normalize();

        force = transform.forward * playerInput.y;

        rigidBody.velocity += force * Time.fixedDeltaTime;

        //  Make sure bicycle always moves in the forward direction
        //rigidBody.velocity = transform.forward * rigidBody.velocity.magnitude;

        transform.Rotate(Vector3.up, playerInput.x * Time.deltaTime * rotationSpeed);
        
    }
}
