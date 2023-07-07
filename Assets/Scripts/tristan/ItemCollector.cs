using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    private int fruits = 0;

    public string[] fruitType;
    public int[] fruitValue;
    private int numberOfFruitTypes;
    private int fruitTypeIndex;

    private GameObject pickup;

    [SerializeField] private Text fruitsText;

    public AudioSource pickupSoundEffect;

    private void Start() {
         numberOfFruitTypes = (fruitType.Length);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        for (int i = 0; i < numberOfFruitTypes; i++) {
            if (collision.gameObject.CompareTag(fruitType[i])) {
                pickup = collision.gameObject;
                fruits = fruits + fruitValue[i];
                fruitsText.text = "" + fruits;

                pickupSoundEffect.Play();

                pickup.GetComponent<Animator>().SetBool("picked", true);
                pickup.GetComponent<CircleCollider2D>().enabled = false;
                pickup.GetComponent<CapsuleCollider2D>().enabled = false;

                pickup.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }

    }
   
}
