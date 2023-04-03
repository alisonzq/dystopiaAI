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

    private GameObject checkpoint;

    private bool hit = false; //to prevent multi-hit from ground hazards
    private bool hit2 = false; //to prevent multi-hit from hazards

    // Start is called before the first frame update
    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHeartIndex = (Heart.Length) - 1;
    }

    //Colliders
    private void OnCollisionEnter2D(Collision2D collision) {
        //ground hazard collider
        if (collision.gameObject.CompareTag("GroundHazard") && !hit) {
            hit = true;
            if (currentHeartIndex != 0) {
                LoseLifeRespawn();
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                currentHeartIndex--;
            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                Die();
            }
        } 
        //hazard collider
        else if (collision.gameObject.CompareTag("Hazard") && !hit2) {
            hit2 = true;
            if (currentHeartIndex != 0) {
                anim.SetTrigger("invincibility");
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                currentHeartIndex--;
            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                Die();
            }
            //health gain collider
        } else if (collision.gameObject.CompareTag("HealthPickup")) {
            Heart[currentHeartIndex + 1].GetComponent<SpriteRenderer>().enabled = true;
            currentHeartIndex++;
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());
            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
        }
    }

    //Triggers
    private void OnTriggerEnter2D(Collider2D collision) {
        //checkpoint
        if (collision.gameObject.CompareTag("CheckPoint") && life != 0) {
            if (checkpoint != collision.gameObject) {
                checkpoint = collision.gameObject;
                collision.gameObject.GetComponent<Animator>().SetTrigger("deployCheckPoint");
            }
        }
        //ground hazard trigger
        else if (collision.gameObject.CompareTag("GroundHazard") && !hit) {
            hit = true;
            if (currentHeartIndex != 0) {
                LoseLifeRespawn();
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                currentHeartIndex--;
            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                Die();
            }
        } 
        //hazard trigger
        else if (collision.gameObject.CompareTag("Hazard") && !hit2) {
            hit2 = true;
            if (currentHeartIndex != 0) {
                anim.SetTrigger("invincibility");
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                currentHeartIndex--;
            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<SpriteRenderer>().enabled = false;
                Die();
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
        hit = false;
        hit2 = false;
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

    //removing invincibility
    private void AfterGetHit() {
        hit2 = false;
        anim.SetTrigger("hitable");
    }
}
