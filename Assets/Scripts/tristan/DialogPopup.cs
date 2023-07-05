using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPopup : MonoBehaviour
{

    public GameObject textBox;
    public Text dialogueText;
    public string dialog;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1) && playerInRange) {
            if (textBox.activeInHierarchy) {
                textBox.SetActive(false);
            } else {
                textBox.SetActive(true);
                dialogueText.text = dialog;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            textBox.SetActive(false);
        }
    }


}
