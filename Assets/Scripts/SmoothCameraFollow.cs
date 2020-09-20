using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float z = gameObject.transform.position.z;
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, z);
            gameObject.transform.position = new Vector3(RoundToNearestPixel(gameObject.transform.position.x, gameObject.GetComponent<Camera>()), RoundToNearestPixel(gameObject.transform.position.y, gameObject.GetComponent<Camera>()), z);
        }

    }

    public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
    {
        float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
        return adjustedUnityUnits;
    }
}
