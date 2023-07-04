using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    private int fruits = 0;

    [SerializeField] private Text fruitsText;

    [SerializeField] private AudioSource pickupSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Banana")) {

            fruits= fruits + 1;
            fruitsText.text = "" + fruits;

            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());

            pickupSoundEffect.Play();

        }
        else if (collision.gameObject.CompareTag("Apple")) {

            fruits = fruits + 2;
            fruitsText.text = "" + fruits;

            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());

            pickupSoundEffect.Play();

        } 
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Banana")) {

            fruits = fruits + 1;
            fruitsText.text = "" + fruits;

            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());

            pickupSoundEffect.Play();

        } else if (collision.gameObject.CompareTag("Apple")) {

            fruits = fruits + 2;
            fruitsText.text = "" + fruits;

            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());

            pickupSoundEffect.Play();

        } 
    }
}
