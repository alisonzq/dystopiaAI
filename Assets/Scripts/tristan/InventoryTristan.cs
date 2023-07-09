using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTristan : MonoBehaviour {

    public GameObject[] inventoryBox;
    private int inventoryBoxLength;
    public Text inventoryText;
    public string inventoryTextString;

    public GameObject[] itemsToRemove;
    private int itemsToRemoveLength;

    public Text[] materialTypes;
    public Text[] materialTypesText;
    private int materialTypesTextLength;


    // Start is called before the first frame update
    void Start() {
        itemsToRemoveLength = itemsToRemove.Length;
        materialTypesTextLength= materialTypesText.Length;
        inventoryBoxLength = inventoryBox.Length;
        for (int p = 0; p < inventoryBoxLength; p++) {


            inventoryBox[p].SetActive(false);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        }   
        
    }

    // Update is called once per frame
    void Update() {

       

        if (Input.GetKeyDown(KeyCode.Escape)) {
            //present ui items dissapear
            for (int i = 0; i < itemsToRemoveLength; i++) {
                if (inventoryBox[0].activeInHierarchy) {

                    for (int k = 0; k < materialTypesTextLength; k++) {
                        materialTypes[k].text = materialTypesText[k].text;
                    }
                } else {

                    for (int j = 0; j < materialTypesTextLength; j++) {
                        materialTypesText[j].text = materialTypes[j].text;
                    }
                }
                if (itemsToRemove[i].activeInHierarchy) {
                    itemsToRemove[i].SetActive(false);
                } else {
                    itemsToRemove[i].SetActive(true);
                }
            }
            //inventory dissapear
            for (int m = 0; m < inventoryBoxLength; m++) {
               
                if (inventoryBox[m].activeInHierarchy) {
                    inventoryBox[m].SetActive(false);
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                } else {
                    inventoryBox[m].SetActive(true);
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;   
                }
            }
        }
        //inventory box switch
        if (Input.GetKeyDown(KeyCode.D) && inventoryBox[0].activeInHierarchy) {
            for (int n = 0; n < inventoryBoxLength; n++) {

                inventoryBox[n].GetComponent<RectTransform>().anchoredPosition = inventoryBox[n].GetComponent<RectTransform>().anchoredPosition + new Vector2(2000, 0f);
                if (inventoryBox[n].GetComponent<RectTransform>().anchoredPosition == new Vector2(2000f * (inventoryBoxLength - 1), 0f)) {
                    inventoryBox[n].GetComponent<RectTransform>().anchoredPosition = new Vector2(-2000f, 0f);
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.A)) {
            for (int o = 0; o < inventoryBoxLength; o++) {

                inventoryBox[o].GetComponent<RectTransform>().anchoredPosition = inventoryBox[o].GetComponent<RectTransform>().anchoredPosition - new Vector2(2000, 0f);
                if (inventoryBox[o].GetComponent<RectTransform>().anchoredPosition == new Vector2(-4000f, 0f)) {
                    inventoryBox[o].GetComponent<RectTransform>().anchoredPosition = new Vector2(2000f * (inventoryBoxLength - 2), 0f);
                }
            }
        }
    }
}
