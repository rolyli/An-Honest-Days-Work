using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    GameObject player;
    GameObject farm1;
    // Start is called before the first frame update
    
    // Main UI button down event
    public void UIButtonDown()
    {
        Object.Destroy(GameObject.FindGameObjectWithTag("UI"));
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        farm1 = GameObject.Find("Farm 1 Bound");


    }

    // Update is called once per frame
    void Update()
    {
        float distance = (farm1.transform.position - player.transform.position).magnitude;
        if (distance < 5)
        {
            SceneManager.LoadScene("Farm 1");
        }
    }
}
