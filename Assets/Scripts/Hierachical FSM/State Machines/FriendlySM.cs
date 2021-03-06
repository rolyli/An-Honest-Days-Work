using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySM : StateMachine
{
    private float maxHealth;
    public float health = 1000;


    private void Awake()
    {
        maxHealth = health;
    }

    protected override void Update()
    {
        base.Update();

        var player = GameObject.FindGameObjectWithTag("Player");
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var rigidBody = gameObject.GetComponent<RigidBody>();


        // Calculate hitpoint reductions during collision and die


        foreach (GameObject enemy in enemies)
        {
            if (enemies != null)
            {
                if ((enemy.transform.position - transform.position).magnitude < 5)
                {
                    health -= 1;
                }
            }
        }



        transform.localScale = new Vector3(Mathf.Lerp(0.5f, 1.0f, health / maxHealth), Mathf.Lerp(0.5f, 1.0f, health / maxHealth), Mathf.Lerp(0.5f, 1.0f, health / maxHealth));

        if (health < 0)
        {
            Object.Destroy(gameObject);
        }
    }

}
