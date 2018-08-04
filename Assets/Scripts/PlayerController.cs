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
    public KeyCode power;
    public float speed;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("kick", false);
        playerRB = GetComponent<Rigidbody2D>();
    }

    public void Update() {

        //movement
        if (Input.GetKey(up))
            playerRB.velocity = new Vector2(0, speed);

        else if (Input.GetKey(down))
            playerRB.velocity = new Vector2(0, -speed);

        else playerRB.velocity = new Vector2(0, 0);


        //power speed
        if (Input.GetKeyDown(power))
            StartCoroutine(powerSpeed());
    }

    private IEnumerator powerSpeed() {
        speed *= 2.5f;
        yield return new WaitForSeconds(0.1f);
        speed /= 2.5f;
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.name == "ball") {
            anim.SetBool("kick", true);
            Rigidbody2D ballRB = collision.GetComponent<Rigidbody2D>();
            float x = (4 + ballRB.velocity.x / 1.6f) + (playerRB.velocity.y / 2f);
            float y = (ballRB.velocity.y / 1.7f) + playerRB.velocity.y;
            if (direction == "right")
                ballRB.velocity = new Vector2(Math.Max(Math.Abs(x), 8),y);
            else ballRB.velocity = new Vector2(Math.Min(-1 * Math.Abs(x), -8), y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name == "ball") {
            anim.SetBool("kick", false);
        }
    }
}
