using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    public string[] pickupTag; //tags of each pickup to run for all tags present
    public int[] pickupValue; //amount of the type the tagged pickup gives
    public int[] pickupType; //type of pickup for text showing
    public int[] pickupTypeAmount; //amount of the type the player has
    private int numberOfPickupTags; //to for loop for all tags
    public Text[] pickupText; //text shown for a type
    public Text[] inventoryPickupText;

    private GameObject pickup; //the picked item

    public AudioSource pickupSoundEffect;

    private void Start() {
         numberOfPickupTags = (pickupTag.Length);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        for (int tagIndex = 0; tagIndex < numberOfPickupTags; tagIndex++) {

            if (collision.gameObject.CompareTag(pickupTag[tagIndex])) {
                pickup = collision.gameObject;

                //physical dissapearance
                pickupSoundEffect.Play();
                pickup.GetComponent<Animator>().SetBool("picked", true);
                pickup.GetComponent<CircleCollider2D>().enabled = false;
                if (pickup.GetComponent<CapsuleCollider2D>() != null) { 
                    pickup.GetComponent<CapsuleCollider2D>().enabled = false;
                }
                if (pickup.GetComponent<Rigidbody2D>() != null) { 
                    pickup.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                }

                //text number increase


                inventoryPickupText[pickupType[tagIndex]] = pickupText[pickupType[tagIndex]];
                pickupText[pickupType[tagIndex]] = inventoryPickupText[pickupType[tagIndex]];
                pickupTypeAmount[pickupType[tagIndex]] = int.Parse(inventoryPickupText[pickupType[tagIndex]].text);

                pickupTypeAmount[pickupType[tagIndex]] = pickupTypeAmount[pickupType[tagIndex]] + pickupValue[tagIndex]; //increase by value       
                pickupText[pickupType[tagIndex]].text = "" + pickupTypeAmount[pickupType[tagIndex]]; //show by text on screen
            }
        }

    }
   
}
