using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private Animator swordAnim1;
    private Animator swordAnim2;

    public GameObject sword;
    public GameObject swordReverse;
    private int state;
    private bool isFlipped = false;
  

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        swordAnim1 = sword.GetComponent<Animator>();
        swordAnim2 = swordReverse.GetComponent<Animator>();

        anim = GetComponent<Animator>();

       
    }

    // Update is called once per frame
    private void Update() {

        isFlipped = GetComponent<PlayerMovement>().flipped;

        state = anim.GetInteger("state");

        if (!isFlipped) {
            swordReverse.SetActive(false);
            sword.SetActive(true);
            swordAnim1.SetInteger("state", state);
            if (Input.GetKeyDown(KeyCode.Mouse0)) {

            }
        } else if (isFlipped) {
            swordReverse.SetActive(true);
            sword.SetActive(false);
            swordAnim2.SetInteger("state", state);
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                swordAnim2.SetInteger("state", 3);

            }
        }

       

    }

} 
