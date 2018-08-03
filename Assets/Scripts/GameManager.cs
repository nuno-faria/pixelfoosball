using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    public GameObject ball;
    public Rigidbody2D ballRB;

	void Start () {
        gm = this;
        ballRB = ball.GetComponent<Rigidbody2D>();
        NewRound();
	}

    public void NewRound() {
        ball.transform.position = new Vector2(0f, 0f);
        ballRB.velocity = new Vector2(0f, 0f);
        Invoke("GoBall", 0.8f);
    }

    public void GoBall() {
        float x = Random.Range(0f, 1f);
        if (x <= 0.5)
            ballRB.AddForce(new Vector2(12f, 6f));
        else ballRB.AddForce(new Vector2(-12f, 6f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
