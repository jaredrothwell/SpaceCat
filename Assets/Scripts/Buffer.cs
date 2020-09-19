using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buffer : MonoBehaviour
{
    void Update()
    {
        if (FMODUnity.RuntimeManager.HasBanksLoaded)
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
}
