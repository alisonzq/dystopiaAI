using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;

    [SerializeField] private GameObject[] Heart;    //all the hearts
    private int currentHeartIndex;     //current life
    [SerializeField] private int life; //max life
    [SerializeField] private int invincibilityTime; //invincibility frames duration
    [SerializeField] private float invincibilityDelay; //invincibility frames delay

    float timer;

    private GameObject checkpoint;

    private bool hit = false; //to prevent multi-hit

    // Start is called before the first frame update
    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHeartIndex = (Heart.Length) - 1;
    }

    //Hazard collide and health gain
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("GroundHazard") && !hit) {
            hit = !hit;
            if (currentHeartIndex != 0) {
                LoseLifeRespawn();
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                currentHeartIndex--;
            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                Die();
            }


        } else if (collision.gameObject.CompareTag("Hazard") && !hit) {
            hit = !hit;
            if (currentHeartIndex != 0) {
                InvincibilityFrames();
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                currentHeartIndex--;
            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                Die();
            }

            //health gain
        } else if (collision.gameObject.CompareTag("HealthPickup")) {
            Heart[currentHeartIndex + 1].GetComponent<SpriteRenderer>().enabled = true;
            currentHeartIndex++;
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());
            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
        }
    }

    //Checkpoint
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("CheckPoint") && life != 0) {
            if (checkpoint != collision.gameObject) {
                checkpoint = collision.gameObject;
                collision.gameObject.GetComponent<Animator>().SetTrigger("deployCheckPoint");
            }
        }
    }

    //Dying
    private void LoseLifeRespawn() {
        anim.SetTrigger("loseLife");
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
    }
    private void Respawn() {
        transform.position = checkpoint.transform.position;
        anim.SetTrigger("respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
        hit = !hit;
    }


    //definitive death
    private void Die() {
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
    }
    private void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //taking a hit
    private void InvincibilityFrames() {
        int invincibilityCount = 0;
        timer += Time.deltaTime;
        while (invincibilityCount < invincibilityTime) {
            if (timer > invincibilityDelay / 2) {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            if (timer > invincibilityDelay) {
                GetComponent<SpriteRenderer>().enabled = true;
                timer -= invincibilityDelay;
                invincibilityCount++;
            }
        }
            hit = !hit;
            invincibilityCount = 0;
            timer = 0f;
        
    }


}
