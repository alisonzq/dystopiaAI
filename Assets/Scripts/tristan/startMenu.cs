using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{

    public GameObject mainMenuPage0;
    public GameObject player;


    private void start() {
    }

    // Start is called before the first frame update
    public void MainMenu()
    {
        player.GetComponent<InventoryTristan>().menuClose = true;

    }

    public void GoToCheckpoint() {
        player.GetComponent<PlayerDeath>().askedRespawn = true;
        player.GetComponent<InventoryTristan>().menuClose = true;
    }


}
