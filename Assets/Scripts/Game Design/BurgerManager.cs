using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerManager : MonoBehaviour
{
    public RigidBody rigidBody;
    public GameObject player;

    // Mass to be added to player when eaten
    public float burgerMass = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<RigidBody>();
        player = GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        if (Object.ReferenceEquals(rigidBody.collisionObject, player)) {

            var playerRigidBody = player.GetComponent<RigidBody>();
            var mass = 1.0f / playerRigidBody.inverseMass;

            mass += burgerMass;
            playerRigidBody.inverseMass = 1 / mass;

            Object.Destroy(gameObject);
        }
    }
}
