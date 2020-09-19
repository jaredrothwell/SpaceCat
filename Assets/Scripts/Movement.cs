﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float Gspd = 1.0f;
    [SerializeField] private float spd = 10.0f;
    [SerializeField] private float Rspd = 1.0f;
    [SerializeField] private float JumpForce = 30.0f;
    [SerializeField] private float Gscale = 1.0f;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private CircleCollider2D cc;
    private Animator anime;
    private string gravity = "zero";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        cc = GetComponent<CircleCollider2D>();
        anime = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gravity == "zero")
        {
            rb.gravityScale = 0;
            anime.SetBool("IsWalking", false);
            ZeroGMovement();
        }
        else if (gravity == "down")
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
            rb.gravityScale = Gscale;
            MovementDown();
        }
        else if (gravity == "up")
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
            rb.gravityScale = -Gscale;
            MovementUp();
        }
        else if (gravity == "right")
        {
            Physics2D.gravity = new Vector2(-9.81f, 0);
            rb.gravityScale = -Gscale;
            MovementRight();
        }
        else if (gravity == "left")
        {
            Physics2D.gravity = new Vector2(-9.81f, 0);
            rb.gravityScale = Gscale;
            MovementLeft();
        }

    }

    void ZeroGMovement()
    {
        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey("w"))
        {
            vertical += 1;
        }
        if (Input.GetKey("a"))
        {
            horizontal -= 1;
        }
        if (Input.GetKey("s"))
        {
            vertical -= 1;
        }
        if (Input.GetKey("d"))
        {
            horizontal += 1;
        }
        Vector2 movement = new Vector2(horizontal, vertical);

        rb.AddForce(movement.normalized * Gspd);

        float rot = 0;
        if (Input.GetKey("e"))
        {
            rot -= 1;
            //GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey("q"))
        {
            rot += 1;
            //GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        rb.AddTorque(rot *Rspd);
    }

    void MovementDown()
    {
        anime.SetBool("IsWalking", false);
        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey("a"))
        {
            anime.SetBool("IsWalking", true);
            horizontal -= 1;
        }
        if (Input.GetKey("d"))
        {
            anime.SetBool("IsWalking", true);
            horizontal += 1;
        }
        if (Input.GetKeyDown("space"))
        {
            vertical = JumpForce;
        }
        Vector2 velocity = new Vector2(horizontal * spd, rb.velocity.y + vertical);
        rb.velocity = velocity;
        rb.rotation = 0;
        flip(true, rb.velocity.x);
    }

    void MovementUp()
    {
        anime.SetBool("IsWalking", false);
        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey("a"))
        {
            anime.SetBool("IsWalking", true);
            horizontal -= 1;
        }
        if (Input.GetKey("d"))
        {
            anime.SetBool("IsWalking", true);
            horizontal += 1;
        }
        if(Input.GetKeyDown("space"))
        {
            vertical = -JumpForce;
        }
        Vector2 velocity = new Vector2(horizontal * spd, rb.velocity.y + vertical);
        rb.velocity = velocity;
        rb.rotation = 180;
        flip(false, rb.velocity.x);
    }

    void MovementRight()
    {
        anime.SetBool("IsWalking", false);
        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey("s"))
        {
            anime.SetBool("IsWalking", true);
            vertical -= 1;
        }
        if (Input.GetKey("w"))
        {
            anime.SetBool("IsWalking", true);
            vertical += 1;
        }
        if (Input.GetKeyDown("space"))
        {
            horizontal = -JumpForce;
        }
        Vector2 velocity = new Vector2(horizontal + rb.velocity.x, vertical * spd);
        rb.velocity = velocity;
        rb.rotation = 90;
        flip(true, rb.velocity.y);
    }

    void MovementLeft()
    {
        anime.SetBool("IsWalking", false);
        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey("s"))
        {
            anime.SetBool("IsWalking", true);
            vertical -= 1;
        }
        if (Input.GetKey("w"))
        {
            anime.SetBool("IsWalking", true);
            vertical += 1;
        }
        if (Input.GetKeyDown("space"))
        {
            horizontal = JumpForce;
        }
        Vector2 velocity = new Vector2(horizontal + rb.velocity.x, vertical * spd);
        rb.velocity = velocity;
        rb.rotation = 270;
        flip(false, rb.velocity.y);
    }

    void flip(bool f, float x)
    {
        if (x < 0 && f)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else if (x > 0 && f)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (x > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "down" || collision.tag == "zero" || collision.tag == "up" || collision.tag == "right" || collision.tag == "left")
        {
            gravity = collision.tag;
            Debug.Log(collision.tag);
        }
    }


}
