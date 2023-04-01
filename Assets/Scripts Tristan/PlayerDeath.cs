using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;

    [SerializeField] private GameObject[] Heart;
    private int currentHeartIndex;

    [SerializeField] private int life;

    private GameObject checkpoint;

    private bool hit = false;

    // Start is called before the first frame update
    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHeartIndex = (Heart.Length)-1;
    }

    //Hazard collide
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Hazard") && !hit) {
            if (currentHeartIndex != 0) {
                LoseLife();
                Destroy(Heart[currentHeartIndex]);
                currentHeartIndex--;
            } else if (currentHeartIndex <= 0) {
                Destroy(Heart[currentHeartIndex]);
                Die();
            }
            hit = !hit;

        }
    }

    //Checkpoint set
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("CheckPoint") && life !=0) {
            if (checkpoint != collision.gameObject) {
                checkpoint = collision.gameObject;
                collision.gameObject.GetComponent<Animator>().SetTrigger("deployCheckPoint");
            }
        }
    }

   

    //Dying
    private void LoseLife() {
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

}
