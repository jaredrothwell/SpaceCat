using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float spd = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spd * Time.deltaTime);
    }
}
