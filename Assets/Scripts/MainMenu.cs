using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> list;
    public int activeScene = 0;
    public GameObject menuCamera;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            list.Add(child.gameObject);
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown("right"))
        {
            activeScene = (activeScene + 1) % list.Count;
            Debug.Log("keydown");

        }

        if (Input.GetKeyDown("return"))
        {
            SceneManager.LoadScene(list[activeScene].gameObject.name);
            Debug.Log("keydown");
        }

        Vector3 newPos = list[activeScene].transform.position;
        newPos.z -= 10;
        menuCamera.transform.position = newPos;
    }
}
