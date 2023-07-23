using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTristan : MonoBehaviour {

    public GameObject[] inventoryBox;
    private int inventoryBoxLength;
    public Text inventoryText;
    public string inventoryTextString;
    private int inventoryIndex = 0;
    private int lastInventoryIndex;

    public GameObject[] itemsToRemove;
    private int itemsToRemoveLength;

    public Text[] materialTypesOut;
    public Text[] materialTypesInventory;
    private int materialTypesOutLength;

    public bool menuClose;


    // Start is called before the first frame update
    void Start() {
        itemsToRemoveLength = itemsToRemove.Length;
        materialTypesOutLength= materialTypesOut.Length;
        inventoryBoxLength = inventoryBox.Length;
        for (int p = 0; p < inventoryBoxLength; p++) {
            inventoryBox[p].SetActive(false);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }   
    }

    // Update is called once per frame
    void Update() {

        if (inventoryBox[lastInventoryIndex].activeInHierarchy) {
            inventoryBox[lastInventoryIndex].SetActive(false);
            inventoryBox[inventoryIndex].SetActive(true);
        }



        if ((Input.GetKeyDown(KeyCode.Escape) && GetComponent<PlayerMovement>().isGrounded == true) || menuClose == true) {
            menuClose = false;
            //ingame ui items dissapear or appear
            for (int i = 0; i < itemsToRemoveLength; i++) {
               
                if (itemsToRemove[i].activeInHierarchy) {
                    itemsToRemove[i].SetActive(false);
                    GetComponent<PlayerMovement>().enabled = false;
                } else {
                    itemsToRemove[i].SetActive(true);
                    GetComponent<PlayerMovement>().enabled = true;
                }
            }
            //inventory dissapear or appear
  
                if (inventoryBox[inventoryIndex].activeInHierarchy) {
                    inventoryBox[inventoryIndex].SetActive(false);

                for (int aa = 0; aa < materialTypesOutLength; aa++) {
                    materialTypesOut[aa].text = materialTypesInventory[aa].text;
                }

                } else {
                    inventoryBox[inventoryIndex].SetActive(true);

                for (int bb = 0; bb < materialTypesOutLength; bb++) {
                    materialTypesInventory[bb].text = materialTypesOut[bb].text;
                }
                }
        }

        //inventory box switch
        if (Input.GetKeyDown(KeyCode.D) && inventoryBox[inventoryIndex].activeInHierarchy) {
            lastInventoryIndex = inventoryIndex;
            inventoryIndex++;
            if(inventoryIndex == inventoryBoxLength) {
                inventoryIndex = 0;
            }

        }
        else if (Input.GetKeyDown(KeyCode.A) && inventoryBox[inventoryIndex].activeInHierarchy) {
            lastInventoryIndex = inventoryIndex;
            if (inventoryIndex == 0) {
                inventoryIndex = inventoryBoxLength;
            }
            inventoryIndex--;


        }
    }
}
