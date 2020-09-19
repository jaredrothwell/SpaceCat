using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool shouldAttract = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldAttract)
        {
            Attractor[] attractors = FindObjectsOfType<Attractor>();
            foreach (Attractor attractor in attractors)
            {
                if (attractor != this)
                    Attract(attractor);
            }
        }
    }

    void Attract(Attractor objToAttract)
    {
        Rigidbody2D rb2 = objToAttract.GetComponent<Rigidbody2D>();
        Vector3 direction = rb.position - rb2.position;
        float distance = direction.magnitude;
        float forceMagnitude = (rb.mass * rb2.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rb2.AddForce(force);
    }
}
