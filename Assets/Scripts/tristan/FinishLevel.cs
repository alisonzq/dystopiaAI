using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour {

    [SerializeField] private Vector2 cameraChange;
    [SerializeField] private Vector3 playerChange;
    private CameraController cam;
    private AudioSource finishSound;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if(needText) {
                StartCoroutine(placeNameCo());
            }
        }
            
    }

    private IEnumerator placeNameCo() {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
