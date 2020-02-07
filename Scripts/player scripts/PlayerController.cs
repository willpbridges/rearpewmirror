using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float gravity;
    public float jumpForce;
    public bool canMove;
    private bool onObj;
    private Vector3 velocity;
    private Rigidbody rb;
    private CapsuleCollider coll;
    private float distToGround;
    private bool isJumping = false;

    public DeathScreen deathScreen;

    public float startingHealth = 3;
    public HealthBar healthBar;
    private float health;

    private Vector3 startingPos;
    private Quaternion startingRot;

    private float respawnTime = 1;
    private bool respawning = false;

    private AudioSource audioSource;
    public AudioClip[] grunts;
    public float stepSoundInterval = 0.3f;
    private float stepSoundTime = 0f;
    public AudioClip[] steps;
    public AudioClip jump;
    public AudioClip land;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        velocity = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        distToGround = coll.bounds.extents.y / 2;

        health = startingHealth;
        healthBar.SetMaxHealth(startingHealth);
        healthBar.UpdateCurrentHealth(health);

        startingPos = this.transform.position;
        startingRot = this.transform.rotation;

        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (IsGrounded() && velocity.y <= 0f)
            {
                if (isJumping)
                {
                    isJumping = false;
                    audioSource.PlayOneShot(land);
                }
                velocity.y = 0;
            }
            else
            {
                velocity.y -= gravity * Time.deltaTime;
            }

            rb.MovePosition(transform.position + velocity);
        }
    }

    public void Move(float moveX, float moveZ)
    {
        if (canMove)
        {
            if (stepSoundTime >= stepSoundInterval && IsGrounded() && (moveX != 0f || moveZ != 0f))
            {
                audioSource.PlayOneShot(steps[Random.Range(0, steps.Length)], 0.8f);
                stepSoundTime = 0f;
            }

            float y = velocity.y;
            velocity = (transform.forward * moveZ * speed * Time.deltaTime) + (transform.right * moveX * speed * Time.deltaTime);
            velocity.y = y;
        }
    }

    public void Jump()
    {
        if (canMove)
        {
            if (IsGrounded())
            {
                audioSource.PlayOneShot(jump);
                isJumping = true;
                velocity.y = jumpForce * Time.deltaTime;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        stepSoundTime += Time.deltaTime;

        if (health == 0 && !respawning)
        {
            StartCoroutine(Respawn());
            if (onObj)
            {
                if ((int)GetComponent<InputManager>().playerNumber % 2 == 0)
                {
                    GameObject.FindGameObjectWithTag("Objective").GetComponent<Objective>().blueOnObj -= 1;
                    GameObject.FindGameObjectWithTag("Objective").GetComponent<Objective>().check();
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Objective").GetComponent<Objective>().orangeOnObj -= 1;
                    GameObject.FindGameObjectWithTag("Objective").GetComponent<Objective>().check();
                }
            }
        }
    }

    // Evaluates whether the player is on the ground via a Raycast
    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits = Physics.SphereCastAll(ray, coll.radius, distToGround + 0.1f);
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider.isTrigger && hit.distance > 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            audioSource.PlayOneShot(grunts[Random.Range(0, grunts.Length)]);
            health--;
            healthBar.UpdateCurrentHealth(health);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Objective")
        {
            onObj = true;

        }
        else
        {
            onObj = false;
        }
    }

    //upon entering killbox, kill player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillBox")
        {
            health = 0;
        }
    }

    IEnumerator Respawn()
    {
        respawning = true;
        canMove = false;

        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        Collider[] colls = GetComponentsInChildren<Collider>();
        InputManager input = GetComponent<InputManager>();
        input.enabled = false;

        deathScreen.FadeIn();
        yield return new WaitForSecondsRealtime(deathScreen.fadeInTime);

        foreach (MeshRenderer m in meshes)
        {
            m.enabled = false;
        }

        foreach (Collider c in colls)
        {
            c.enabled = false;
        }

        yield return new WaitForSecondsRealtime(respawnTime);

        transform.position = startingPos;
        transform.rotation = startingRot;
        health = startingHealth;
        healthBar.UpdateCurrentHealth(health);

        foreach (MeshRenderer m in meshes)
        {
            m.enabled = true;
        }

        foreach (Collider c in colls)
        {
            c.enabled = true;
        }

        GetComponentInChildren<Gun>().RefillAmmoInstantly();

        deathScreen.FadeOut();
        yield return new WaitForSecondsRealtime(deathScreen.fadeOutTime);

        velocity = Vector3.zero;

        input.enabled = true;
        respawning = false;
        canMove = true;
    }
}
