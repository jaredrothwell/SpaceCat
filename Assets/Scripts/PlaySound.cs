using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private float Timer = 3f;
    private float currentTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/New Event", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        if(currentTimer >= Timer)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/New Event", gameObject);
            currentTimer = 0.0f;
        }
    }
}
