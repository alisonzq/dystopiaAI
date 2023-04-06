using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class FinishLevel : MonoBehaviour {

    [SerializeField] private int level;
    [SerializeField] private int entry;
    private AudioSource finishSound;

    private bool teleported = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        finishSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (teleported == false) {
            finishSound.Play();
            teleported = true;
            collision.transform.position = GameObject.Find("Level" + level + " Entry" + entry).transform.position;
            GameObject.Find("Level" + level + " Entry" + entry).GetComponent<FinishLevel>().AcceptTeleportBetweenEntries();
        }
    }

    private void OnCollisionExit2D(Collision2D collide) {
    
            teleported = false;
        
    }

    private void AcceptTeleportBetweenEntries() {
        teleported = true;
    }
}
