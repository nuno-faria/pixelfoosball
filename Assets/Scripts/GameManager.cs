using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    public GameObject ball;
    public Rigidbody2D ballRB;
    public Text score;

    private int p1Score;
    private int p2Score;

	void Start () {
        gm = this;
        ballRB = ball.GetComponent<Rigidbody2D>();
        p1Score = 0;
        p2Score = 0;
        NewRound(0);
	}

    public void NewRound(int player) {
        if (player == 1)
            p1Score++;
        else if (player == 2)
            p2Score++;

        score.text = "<color=#FF2526>" + p1Score + "</color> / " + "<color=#004DFF>" + p2Score + "</color>";

        ball.transform.position = new Vector2(0f, 0f);
        ballRB.velocity = new Vector2(0f, 0f);
        Invoke("GoBall", 0.6f);
    }

    public void GoBall() {
        float x = Random.Range(0f, 1f);
        if (x <= 0.5f)
            ballRB.AddForce(new Vector2(12f, 6f));
        else ballRB.AddForce(new Vector2(-12f, -6f));
    }
}
