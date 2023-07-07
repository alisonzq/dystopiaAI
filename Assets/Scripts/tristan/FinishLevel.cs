using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour {

    [SerializeField] private Vector2 cameraChange;
    [SerializeField] private Vector3 playerChange;
    [SerializeField] private Vector2 cameraBoundsEnlargementMax;
    [SerializeField] private Vector2 cameraBoundsEnlargementMin;
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
            cam.minPosition += cameraBoundsEnlargementMin;
            cam.maxPosition += cameraChange;
            cam.maxPosition += cameraBoundsEnlargementMax;
            other.transform.position += playerChange;
            if(needText) {
                StartCoroutine(placeNameCo());
            }
        }
        else {
            other.transform.position += playerChange;

            
        }
    }

    private IEnumerator placeNameCo() {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
