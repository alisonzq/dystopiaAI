using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;

    [SerializeField] private Text lifeText;

    [SerializeField] private int life;

    // Start is called before the first frame update
    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Hazard")) {
            life--;
            lifeText.text = "" + life;
            LoseLife();

            
        }
        if(life == 0) {
            Die();
        }
    }

    private void Respawn() {
        transform.position = new Vector3(-2.78f, 2.967f, 0f);
        anim.SetTrigger("respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
    }


    private void LoseLife() {
        anim.SetTrigger("loseLife");
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
        
       
    }


    private void Die() {
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
    }

    private void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
