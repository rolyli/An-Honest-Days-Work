using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    GameObject player;
    GameObject farm1;
    GameObject farm2;

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
        farm2 = GameObject.Find("Farm 2 Bound");
    }

    // Update is called once per frame
    void Update()
    {
        float farm1Distance = (farm1.transform.position - player.transform.position).magnitude;
        if (farm1Distance < 3)
        {
            SceneManager.LoadScene("Farm 1");
        }

        float farm2Distance = (farm2.transform.position - player.transform.position).magnitude;
        if (farm2Distance < 5)
        {
            SceneManager.LoadScene("Farm 2");
        }
    }
}
