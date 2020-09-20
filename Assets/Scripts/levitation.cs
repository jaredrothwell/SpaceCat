using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levitation : MonoBehaviour
{
    public float timer = 1.0f;
    public float currentTimer = 0.0f;
    public float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void FixedUpdate()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer >= timer)
        {
            currentTimer = 0.0f;
            speed = -speed;
        }
    }
}
