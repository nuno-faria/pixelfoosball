using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("kick", false);
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        Debug.Log("asdasd");
        if (collision.name == "ball") {
            anim.SetBool("kick", true);
            //TODO change
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name == "ball") {
            anim.SetBool("kick", false);
        }
    }
}
