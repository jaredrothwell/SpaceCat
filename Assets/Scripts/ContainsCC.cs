using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainsCC : MonoBehaviour
{
    CircleCollider2D cc;
    GameObject player;
    //public float spd = 100f;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = player.transform.position;
        if (cc.OverlapPoint(pos))
        {
            player.GetComponent<Movement>().gravity = tag;
        }
        //transform.Rotate(0, 0, spd * Time.deltaTime);
    }
}
