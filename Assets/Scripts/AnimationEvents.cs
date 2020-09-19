using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void stopJump()
    {
        FindObjectOfType<Movement>().stopJump();
    }
}
