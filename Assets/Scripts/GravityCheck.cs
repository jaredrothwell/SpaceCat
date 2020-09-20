using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "down" || collision.tag == "zero" || collision.tag == "up" || collision.tag == "right" || collision.tag == "left")
        {
            
            GetComponentInParent<Movement>().gravity = collision.tag;
            Debug.Log("entered " + collision.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "down" || collision.tag == "zero" || collision.tag == "up" || collision.tag == "right" || collision.tag == "left")
        {

            GetComponentInParent<Movement>().gravity = "zero";
           Debug.Log("Exited " + collision.tag);
        }
    }
}
