using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftCode : MonoBehaviour
{
    public Text craftText;
    public Text[] materialCostType;
    private int materialCostLength;
    public int[] materialCost;
    private bool craftable;

    private void Start() {
        materialCostLength = materialCost.Length;
    }

    // Start is called before the first frame update
    public void Craft() {
        for (int i = 0; i < materialCostLength; i++) {
            if (!(int.Parse(materialCostType[i].text) >= materialCost[i])) {
                craftable = false;
                Debug.Log("aaa");
            }
        }
        if (craftable) {
            craftText.text = "" + (int.Parse(craftText.text) + 1);
            Debug.Log("eeee");
            for (int j = 0; j < materialCostLength; j++) {
                materialCostType[j].text = "" + (int.Parse(materialCostType[j].text) - materialCost[j]);
            }
        }

        craftable = true;
    }
}
