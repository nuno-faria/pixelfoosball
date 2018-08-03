using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Animator anim;
    public string direction;
    private Rigidbody2D playerRB;
    public KeyCode up;
    public KeyCode down;
    public float speed = 6;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("kick", false);
        playerRB = GetComponent<Rigidbody2D>();
    }

    public void Update() {
        if (Input.GetKey(up))
            playerRB.velocity = new Vector2(0, speed);

        else if (Input.GetKey(down))
            playerRB.velocity = new Vector2(0, -speed);

        else playerRB.velocity = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.name == "ball") {
            anim.SetBool("kick", true);
            Rigidbody2D ballRB = collision.GetComponent<Rigidbody2D>();
            float x = (2 + ballRB.velocity.x / 2) + (playerRB.velocity.y / 3f);
            float y = (2 + ballRB.velocity.y / 2) + (playerRB.velocity.y / 2f);
            if (direction == "right")
                ballRB.velocity = new Vector2(Math.Min(Math.Abs(x), 10),y);
            else ballRB.velocity = new Vector2(Math.Min(-1 * Math.Abs(x), -10), y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name == "ball") {
            anim.SetBool("kick", false);
        }
    }
}
