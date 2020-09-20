using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMOD.Studio;

public class Movement : MonoBehaviour
{
    [SerializeField] private float Gspd = 1.0f;
    [SerializeField] private float spd = 10.0f;
    [SerializeField] private float Rspd = 1.0f;
    [SerializeField] private float JumpForce = 30.0f;
    [SerializeField] private float Gscale = 1.0f;
    [SerializeField] private int Maxfuel = 100;
    [SerializeField] private int fuel = 100;

    public bool canJump = false;
    public Text textUI = null;
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public ParticleSystem ps3;
    public ParticleSystem ps4;
    public string gravity = "zero";
    public Text deathText;

    public Transform t;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private CircleCollider2D cc;
    private Animator anime;
    private bool IsDead = false;

    FMOD.Studio.EventInstance Refil;

    FMOD.Studio.EventInstance Footsteps;
    FMOD.Studio.EventDescription Surface;
    FMOD.Studio.PARAMETER_DESCRIPTION sur;
    FMOD.Studio.PARAMETER_ID SID;

    FMOD.Studio.EventInstance Booster;
    FMOD.Studio.EventDescription State;
    FMOD.Studio.EventDescription Gas;
    FMOD.Studio.PARAMETER_DESCRIPTION OnOff;
    FMOD.Studio.PARAMETER_DESCRIPTION Levl;
    FMOD.Studio.PARAMETER_ID IO;
    FMOD.Studio.PARAMETER_ID Lev;

    FMOD.Studio.EventInstance Jump;
    FMOD.Studio.EventDescription Up;
    FMOD.Studio.PARAMETER_DESCRIPTION UpDown;
    FMOD.Studio.PARAMETER_ID UD;

    FMOD.Studio.PLAYBACK_STATE pbs;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        cc = GetComponent<CircleCollider2D>();
        anime = GetComponentInChildren<Animator>();
        Footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Characters/Space Cat/Walking");
        Booster = FMODUnity.RuntimeManager.CreateInstance("event:/Characters/Space Cat/Booster");
        Jump = FMODUnity.RuntimeManager.CreateInstance("event:/Characters/Space Cat/Jumping");
        Refil = FMODUnity.RuntimeManager.CreateInstance("event:/Characters/Space Cat/Oxygen");

        Surface = FMODUnity.RuntimeManager.GetEventDescription("event:/Characters/Space Cat/Walking");
        Surface.getParameterDescriptionByName("Surface", out sur);
        SID = sur.id;

        State = FMODUnity.RuntimeManager.GetEventDescription("event:/Characters/Space Cat/Booster");
        State.getParameterDescriptionByName("Booster", out OnOff);
        IO = OnOff.id;

        Gas = FMODUnity.RuntimeManager.GetEventDescription("event:/Characters/Space Cat/Booster");
        Gas.getParameterDescriptionByName("Fuel", out Levl);
        Lev = Levl.id;

        Up = FMODUnity.RuntimeManager.GetEventDescription("event:/Characters/Space Cat/Jumping");
        Up.getParameterDescriptionByName("UpDown", out UpDown);
        UD = UpDown.id;

        Footsteps.setParameterByID(SID, 0);
    }

    // Update is called once per frame
    void Update()
    {
        checkGround();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footsteps, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Booster, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Jump, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Refil, GetComponent<Transform>(), GetComponent<Rigidbody>());
        Playsound();
        Jump.setParameterByID(UD, 0);
        ps1.enableEmission = false;
        ps2.enableEmission = false;
        ps3.enableEmission = false;
        ps4.enableEmission = false;
        anime.SetBool("IsZero", false);
        if (gravity == "dead" || IsDead)
        {
            deathText.text = "You Died! Press R to restart level";
            IsDead = true;
        }
        else if (gravity == "zero")
        {
            rb.freezeRotation = false;
            anime.SetBool("IsZero", true);
            anime.SetBool("IsFalling", false);
            rb.gravityScale = 0;
            anime.SetBool("IsWalking", false);
            ZeroGMovement();
        }
        else if (gravity == "down")
        {
            rb.freezeRotation = true;
            Physics2D.gravity = new Vector2(0, -9.81f);
            rb.gravityScale = Gscale;
            MovementDown();
        }
        else if (gravity == "up")
        {
            rb.freezeRotation = true;
            Physics2D.gravity = new Vector2(0, -9.81f);
            rb.gravityScale = -Gscale;
            MovementUp();
        }
        else if (gravity == "right")
        {
            rb.freezeRotation = true;
            Physics2D.gravity = new Vector2(-9.81f, 0);
            rb.gravityScale = -Gscale;
            MovementRight();
        }
        else if (gravity == "left")
        {
            rb.freezeRotation = true;
            Physics2D.gravity = new Vector2(-9.81f, 0);
            rb.gravityScale = Gscale;
            MovementLeft();
        }
        updateFuelUI();
        if(Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void checkGround()
    {
        if(canJump)
            anime.SetBool("IsFalling", false);
        else
            anime.SetBool("IsFalling", true);
    }

    void updateFuelUI()
    {
        if(textUI != null)
        {
            float percent = (((float)fuel / (float)Maxfuel) * 100f);
            textUI.text = "Fuel " + (int)percent + "%";
        }
    }

    void ZeroGMovement()
    {
        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey("w") && fuel > 0)
        {
            vertical += 1;
            fuel--;
            ps2.enableEmission = true;
        }
        if (Input.GetKey("a") && fuel > 0)
        {
            horizontal -= 1;
            fuel--;
            ps4.enableEmission = true;
        }
        if (Input.GetKey("s") && fuel > 0)
        {
            vertical -= 1;
            fuel--;
            ps1.enableEmission = true;
        }
        if (Input.GetKey("d") && fuel > 0)
        {
            horizontal += 1;
            fuel--;
            ps3.enableEmission = true;
        }
        Vector2 movement = new Vector2(horizontal, vertical);

        rb.AddForce(movement.normalized * Gspd * Time.deltaTime);

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
        rb.AddTorque(rot * Rspd * Time.deltaTime);
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
        if (Input.GetKeyDown("space") && canJump)
        {
            anime.SetBool("IsJumping", true);
            canJump = false;
            vertical = JumpForce;
            Jump.start();
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
        if(Input.GetKeyDown("space") && canJump)
        {
            anime.SetBool("IsJumping", true);
            canJump = false;
            vertical = -JumpForce;
            Jump.start();
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
        if (Input.GetKeyDown("space") && canJump)
        {
            anime.SetBool("IsJumping", true);
            canJump = false;
            horizontal = -JumpForce;
            Jump.start();
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
        if (Input.GetKeyDown("space") && canJump)
        {
            anime.SetBool("IsJumping", true);
            canJump = false;
            horizontal = JumpForce;
            Jump.start();
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
            t.localPosition = new Vector3(0.125f, 0, 0);

        }
        else if (x > 0 && f)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            t.localPosition = new Vector3(0, 0, 0);
        }
        else if (x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            t.localPosition = new Vector3(0, 0, 0);
        }
        else if (x > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            t.localPosition = new Vector3(0.125f, 0, 0);

        }
    }
  
    public void stopJump()
    {
        anime.SetBool("IsJumping", false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "fuel")
        {
            fuel = Maxfuel;
            Refil.getPlaybackState(out pbs);
            if(pbs != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                Refil.start();
            }
        }
    }
    public void Playsound()
    {
        Booster.setParameterByID(Lev, (float)fuel);
        if (gravity == "zero" && fuel > 0)
        {
            Footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Booster.getPlaybackState(out pbs);
            if (Input.GetKey("a") | Input.GetKey("w") | Input.GetKey("d") | Input.GetKey("s"))
            {
                if (pbs != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    Booster.setParameterByID(IO, 1);
                    Booster.start();
                }
            }
            if (Input.GetKeyUp("a") | Input.GetKeyUp("d") | Input.GetKeyUp("s") | Input.GetKeyUp("w"))
            {
                Booster.setParameterByID(IO, 0);
                Booster.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        else if(gravity == "zero" && fuel <= 0)
        {
            Booster.getPlaybackState(out pbs);
            Booster.setParameterByID(IO, 0);
            Booster.setParameterByID(Lev, 0f);
            Booster.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            
            if (Input.GetKeyDown("a") | Input.GetKeyDown("w") | Input.GetKeyDown("d") | Input.GetKeyDown("s"))
            {
                if (pbs != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    Booster.start();
                }
                Booster.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        else if (gravity != "zero")
        {
            Booster.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Footsteps.getPlaybackState(out pbs);
            if (gravity == "down")
            {
                if ((Input.GetKey("a") | Input.GetKey("d")) && canJump)
                {
                    if (pbs != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        Footsteps.start();
                    }
                }
                else
                {
                    Footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }
            else if (gravity == "up")
            {
                if ((Input.GetKey("d") | Input.GetKey("a")) && canJump)
                {
                    if (pbs != FMOD.Studio.PLAYBACK_STATE.PLAYING && canJump)
                    {
                        Footsteps.start();
                    }
                }
                else
                {
                    Footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }
            else if (gravity == "right")
            {
                if ((Input.GetKey("w") | Input.GetKey("s")) && canJump)
                {
                    if (pbs != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        Footsteps.start();
                    }
                }
                else
                {
                    Footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }
            else if (gravity == "left")
            {
                if ((Input.GetKey("s") | Input.GetKey("w")) && canJump)
                {
                    if (pbs != FMOD.Studio.PLAYBACK_STATE.PLAYING && canJump)
                    {
                        Footsteps.start();
                    }
                }
                else
                {
                    Footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }
        }
           
    }
}
