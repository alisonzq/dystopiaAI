using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() 
        {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

        if (Input.GetButton("Jump") & GetComponent<Rigidbody2D>().velocity.y <= 0.01 & GetComponent<Rigidbody2D>().velocity.y >= -0.01) {
            rb.velocity = new Vector3(rb.velocity.x, 7f);
        }
     
    }
}
