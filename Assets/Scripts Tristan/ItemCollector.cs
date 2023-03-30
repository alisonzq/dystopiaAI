using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    private int bananas = 0;

    [SerializeField] private Text bananasText;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Banana")) {

            bananas++;
            bananasText.text = "" + bananas;

            collision.gameObject.GetComponent<Animator>().SetBool("picked", true);
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());

        } else collision.gameObject.GetComponent<Animator>().SetBool("picked", false);
    }

}
