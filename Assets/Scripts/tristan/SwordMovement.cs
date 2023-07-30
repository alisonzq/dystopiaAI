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
    private bool isFlipped;
    private bool isGrounded;

  

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        swordAnim1 = sword.GetComponent<Animator>();
        swordAnim2 = swordReverse.GetComponent<Animator>();

      
    }

    // Update is called once per frame
    private void Update() {

        isFlipped = GetComponent<PlayerMovement>().flipped;
        isGrounded = GetComponent<PlayerMovement>().isGrounded;

        state = GetComponent<Animator>().GetInteger("state");
        if (!isFlipped) {
            swordReverse.SetActive(false);
            sword.SetActive(true);
            swordAnim1.SetInteger("state", state);
       

        } else if (isFlipped) {
            swordReverse.SetActive(true);
            sword.SetActive(false);
            swordAnim2.SetInteger("state", state);
           
            
        }
        
       

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector3 mousePos = Input.mousePosition;
            {
                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);
            }
            if ((!isFlipped && mousePos.x > transform.position.x + 900) || ( isFlipped && mousePos.x < transform.position.x + 800 )) {
                swordAnim1.SetInteger("state", 4);
                swordAnim2.SetInteger("state", 4);
            } else if (mousePos.y > transform.position.y + 600) {
                swordAnim1.SetInteger("state", 5);
                swordAnim2.SetInteger("state", 5);
            } else if (mousePos.y < transform.position.y + 400 && !isGrounded) {
                swordAnim1.SetInteger("state", 6);
                swordAnim2.SetInteger("state", 6);


            } 


         
            //y: >600 <400
            //x: >900 <800
         
        }


    }

} 
