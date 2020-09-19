using System.Collections;
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
    private bool IsZeroG = true;

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
        if(IsZeroG)
        {
            anime.SetBool("IsWalking", false);
            ZeroGMovement();
        }
        else
        {

            RegularMovement();
            if(rb.velocity.x > 0.001 || rb.velocity.x < -0.001)
                anime.SetBool("IsWalking", true);
            else
                anime.SetBool("IsWalking", false);
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

        if (Input.GetKeyDown("space"))
        {
            rb.gravityScale = Gscale;
            cc.enabled = false;
            bc.enabled = true;
            IsZeroG = false;
        }
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

    void RegularMovement()
    {
        float horizontal = 0;
        if (Input.GetKey("a"))
        {
            horizontal -= 1;
        }
        if (Input.GetKey("d"))
        {
            horizontal += 1;
        }
        Vector2 velocity = new Vector2(horizontal * spd, rb.velocity.y);

        if (Input.GetKeyDown("space"))
        {
            rb.gravityScale = 0.0f;
            cc.enabled = true;
            bc.enabled = false;
            velocity.y += JumpForce;
            IsZeroG = true;
        }

        rb.velocity = velocity;
        rb.rotation = 0;
        flip();
    }

    void flip()
    {
        if (rb.velocity.x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }
}
