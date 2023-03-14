using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;

    public float sqSpeed = 2;
    public float jumpForce = 2;
    public float raycastLength = 2;

    public bool isGrounded;
    public LayerMask groundLayerMask;
    public Transform respawnPoint;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint.position = transform.position;
    }

    // Update is called once per frame

    void Update()
    {
        // To make player move
        rb.velocity = new Vector2(horizontal * sqSpeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");

        // To make player jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // To animate the movement
        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        // To flip the sprite
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        // To chech whether the player is on the Ground
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * raycastLength, Color.green);

        // To animate Jumping
        anim.SetBool("isGrounded", isGrounded);
    }

    //To collect coins
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Respawn")
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint.position;
    }
}
