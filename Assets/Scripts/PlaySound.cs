﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/New Event", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
