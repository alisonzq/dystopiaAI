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

    private bool groundHazardHit = false;
    private bool hazardHit = false;

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
            groundHazardHit = true;
        } 
        //hazard collider
        else if (collision.gameObject.CompareTag("Hazard") && !hit2) {
            hit2 = true;
            hazardHit = true;
            //health gain collider
        } else if (collision.gameObject.CompareTag("HealthPickup")) {
            Heart[currentHeartIndex + 1].GetComponent<Animator>().SetTrigger("gainLife");
            currentHeartIndex++;
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());
            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
        }
    }

    //Colliders exit
    private void OnCollisionExit2D(Collision2D collision) {
        //ground hazard collider
        if (collision.gameObject.CompareTag("GroundHazard")) {
            groundHazardHit = false;
        }
        //hazard collider
        else if (collision.gameObject.CompareTag("Hazard")) {
            hazardHit = false;
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
            groundHazardHit = true;
        } 
        //hazard trigger
        else if (collision.gameObject.CompareTag("Hazard") && !hit2) {
            hit2 = true;
            hazardHit = true;
        }
    }

    //Triggers exit
    private void OnTriggerExit2D(Collider2D collision) {
        //ground hazard trigger
        if (collision.gameObject.CompareTag("GroundHazard")) {
            groundHazardHit = false;
        }
        //hazard trigger
        else if (collision.gameObject.CompareTag("Hazard")) {
            hazardHit = false;
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
        hazardHit;
        anim.SetTrigger("hitable");
        
    }

    private void Update() {
        if (groundHazardHit == true) {
            groundHazardHit = false;
            if (currentHeartIndex != 0) {
                LoseLifeRespawn();
                Heart[currentHeartIndex].GetComponent<Animator>().SetTrigger("loseLife");
                currentHeartIndex--;
            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<Animator>().SetTrigger("loseLife");
                Die();
            }
        }

        if (hazardHit == true && hit2 == true) {
            hit2 == false
            if (currentHeartIndex != 0) {
                anim.SetTrigger("invincibility");
                Heart[currentHeartIndex].GetComponent<Animator>().SetTrigger("loseLife");
                currentHeartIndex--;

            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<Animator>().SetTrigger("loseLife");
                Die();
            }
        }

    }

}
