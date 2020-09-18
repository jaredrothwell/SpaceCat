using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Update is called once per frame
    void Update()
    {
        float z = gameObject.transform.position.z;
        gameObject.transform.position = player.transform.position;
        gameObject.transform.position += new Vector3(0, 0, z);

    }
}
