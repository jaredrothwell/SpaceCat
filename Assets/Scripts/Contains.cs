using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contains : MonoBehaviour
{
    CompositeCollider2D cc;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CompositeCollider2D>();
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
    }
}
