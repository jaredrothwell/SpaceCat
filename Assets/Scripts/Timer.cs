using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timer = 5.0f;
    public float currentTimer = 0.0f;


    private void FixedUpdate()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer >= timer)
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }
}
