using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [SerializeField] private GameObject destination;

    void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Player")) {
            col.transform.position = destination.transform.position;
            //Handheld.Vibrate();
        }
    }

}