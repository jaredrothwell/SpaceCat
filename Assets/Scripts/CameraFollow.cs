using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float z = gameObject.transform.position.z;
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, z);
        }

    }
}
