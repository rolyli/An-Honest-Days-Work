using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    RigidBody rigidBody;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
 


    }

    // Update is called once per frame
    void Update()
    {
        rigidBody = gameObject.GetComponent<RigidBody>();
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        // If in collision
        if (rigidBody.collisionBuffer.Count > 0)
        {
            Collision collision = rigidBody.collisionBuffer.Find(obj => obj.CheckReference(player));
            
            if (collision != null)
            {
                Sound sound = audioManager.GetSource("Donk");
                //Debug.Log($"collision {sound.source.isPlaying}");
                if (sound.source.isPlaying == false)
                {
                    //Debug.Log("play");
                    audioManager.PlayOneShot("Donk");
                }
            }
        }
    }
}
