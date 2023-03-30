using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update() 
        {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButton("Jump") & GetComponent<Rigidbody2D>().velocity.y <= 0.01 & GetComponent<Rigidbody2D>().velocity.y >= -0.01) 
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        UpdateAnimationStatus();

        }
    private void UpdateAnimationStatus() 
    {
        if (dirX > 0f) {
            anim.SetBool("running", true);
            sprite.flipX = false;
        } else if (dirX < 0f) {
            anim.SetBool("running", true);
            sprite.flipX = true;
        } else {
            anim.SetBool("running", false);
        }
    }
}
