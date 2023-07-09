using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask Hazard;
    public GameObject[] objectToKeep;
    private int objectToKeepLength;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float gravity = 14f;
    [SerializeField] private float longJump = 14f;

    private enum MovementState { idle, running, jumping, falling, dead, idleInvincible, runningInvincible, jumpingInvincible, fallingInvincible}

    [SerializeField] private AudioSource jumpSoundEffect; 

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        objectToKeepLength = objectToKeep.Length;

        anim.SetTrigger("respawn");
        for (int aa = 0; aa < objectToKeepLength; aa++) {
            DontDestroyOnLoad(objectToKeep[aa]);
        }

    }

    // Update is called once per frame
    private void Update() {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        } else if (Input.GetButton("Jump") && !IsGrounded() && rb.velocity.y > 1f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravity/longJump);
        } else if (!IsGrounded() && rb.velocity.y > .1f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravity);
        }  else if (rb.velocity.y < -.1f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravity);
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState() {
        MovementState state;

            if (dirX > 0f) {
                state = MovementState.running;
                sprite.flipX = false;
            } else if (dirX < 0f) {
                state = MovementState.running;
                sprite.flipX = true;
            } else {
                state = MovementState.idle;
            }

            if (rb.velocity.y > .1f) {
                state = MovementState.jumping;
            } else if (rb.velocity.y < -.1f) {
                state = MovementState.falling;
            }
        
        
    

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

} 
