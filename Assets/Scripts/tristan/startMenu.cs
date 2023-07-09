using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{

    private string lastLoadedScene;
    public GameObject[] inventoryBox;
    private int inventoryBoxLength;


    private void start() {
        lastLoadedScene = PlayerPrefs.GetString("LastLoadedScene");
        inventoryBoxLength = inventoryBox.Length;
    }

    // Start is called before the first frame update
    public void MainMenu()
    {
        for (int p = 0; p < inventoryBoxLength; p++) {


            inventoryBox[p].SetActive(false);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        }
        SceneManager.LoadScene(0);
    }

    public void Restart() {
        for (int q = 0; q < inventoryBoxLength; q++) {


            inventoryBox[q].SetActive(false);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        }
        SceneManager.LoadScene(1);
     
    }


}
