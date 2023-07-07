using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource pickupSoundEffect;
    [SerializeField] private AudioSource sawSoundEffect;

    [SerializeField] private GameObject[] Heart;    //all the hearts
    private int currentHeartIndex;     //current life
    [SerializeField] private int life; //max life

    private GameObject checkpoint;
    private GameObject healthPickup;

    private bool respawning = false; //to prevent multi-hit from ground hazards
    public bool invincible = false; //to prevent multi-hit from hazards
    
    private bool groundHazardTouching = false;
    private bool hazardTouching = false;

    // Start is called before the first frame update
    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHeartIndex = (Heart.Length) - 1;
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

        //life pickup
        if (collision.gameObject.CompareTag("HealthPickup") && currentHeartIndex+1 != life) {

            healthPickup = collision.gameObject;

            healthPickup.GetComponent<Animator>().SetTrigger("picked");

            healthPickup.GetComponent<CircleCollider2D>().enabled = false;
            healthPickup.GetComponent<CapsuleCollider2D>().enabled = false;

            healthPickup.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            Heart[currentHeartIndex + 1].GetComponent<Animator>().SetTrigger("gainLife");
            currentHeartIndex++;

            pickupSoundEffect.Play();
        }

     

       //ground hazard trigger
       else if (collision.gameObject.CompareTag("GroundHazard") && !respawning) {
            groundHazardTouching = true;
        } 
        //hazard trigger
        else if (collision.gameObject.CompareTag("Hazard")) {
            hazardTouching = true;
        }
    }

    //Triggers exit
    private void OnTriggerExit2D(Collider2D collision) {
        //ground hazard trigger
        if (collision.gameObject.CompareTag("GroundHazard")) {
            groundHazardTouching = false;
        }
        //hazard trigger
        else if (collision.gameObject.CompareTag("Hazard")) {
            hazardTouching = false;
        }
    }


    //definitive death
    private void DefinitiveDeath() {
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
        StartCoroutine(RestartLevel());
    }

    private void Update() {
      
        if (groundHazardTouching == true && respawning == false) {
            groundHazardTouching = false;
            if (currentHeartIndex != 0) {
                Heart[currentHeartIndex].GetComponent<Animator>().SetTrigger("loseLife");
                currentHeartIndex--;
                Die();
            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<Animator>().SetTrigger("loseLife");
                DefinitiveDeath();
            }
        }

        if (hazardTouching == true && invincible == false) {
            if (currentHeartIndex != 0) {
                Heart[currentHeartIndex].GetComponent<Animator>().SetTrigger("loseLife");
                currentHeartIndex--;
                sawSoundEffect.Play();
                StartCoroutine(invincibilityFrames());
            } else if (currentHeartIndex <= 0) {
                Heart[currentHeartIndex].GetComponent<Animator>().SetTrigger("loseLife");
                DefinitiveDeath();
            }
        }

    }

    private IEnumerator invincibilityFrames() {
        anim.SetTrigger("invincibility");
        invincible = true;
        yield return new WaitForSeconds(5f);
        invincible = false;
        anim.SetTrigger("hitable");
    }

    private void Die() {
        anim.SetTrigger("loseLife");
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn() {
        respawning = true;
        yield return new WaitForSeconds(0.4f);
        transform.position = checkpoint.transform.position; 
        anim.SetTrigger("respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
        respawning = false;
        anim.SetTrigger("hitable");
    }

    private IEnumerator RestartLevel() {
        respawning = true;
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("respawn");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        respawning = false;
        anim.SetTrigger("hitable");
    }

   
    
}
