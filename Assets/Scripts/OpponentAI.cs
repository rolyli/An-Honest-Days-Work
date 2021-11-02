using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dToPlayer = player.transform.position - velocity;
        dToPlayer.y = 0;

        velocity = -dToPlayer.normalized * Time.fixedDeltaTime * 5;
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
