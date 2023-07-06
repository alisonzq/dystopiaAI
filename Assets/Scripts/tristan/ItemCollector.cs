using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    private int fruits = 0;

    private GameObject pickup;

    private bool destroyPickup = false;

    [SerializeField] private Text fruitsText;

    public AudioSource pickupSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) {
   
        if (collision.gameObject.CompareTag("Banana")) {
            pickup = collision.gameObject;
            destroyPickup = true;
            fruits= fruits + 1;
            fruitsText.text = "" + fruits;

            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());

            pickupSoundEffect.Play();
            StartCoroutine(DestroyPickup());
        }
        else if (collision.gameObject.CompareTag("Apple")) {
            pickup = collision.gameObject;
            fruits = fruits + 2;
            fruitsText.text = "" + fruits;

            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());

            pickupSoundEffect.Play();
            StartCoroutine(DestroyPickup());
        } 
    }
   

    private void Update() {
        if (destroyPickup) {
            pickup.gameObject.SetActive(false);
            destroyPickup = false;
        }
    }

    private IEnumerator DestroyPickup() {
        yield return new WaitForSeconds(1f);
        destroyPickup = true;
    }
}
