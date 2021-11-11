using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshProThing : MonoBehaviour
{
    public TMPro.TextMeshProUGUI health;
    public int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        health = health.GetComponent<TMPro.TextMeshProUGUI>();  

        health.SetText("hello");
    }
}
