using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    public GameObject ball;
    public Rigidbody2D ballRB;
    public Text score;
    public Text countdown;

    private int p1Score;
    private int p2Score;

    public float cooldownTime;
    public Dictionary<string, float> nextPower;

    public KeyCode powerP1;
    public KeyCode powerP2;

	void Start () {
        gm = this;
        ballRB = ball.GetComponent<Rigidbody2D>();
        nextPower = new Dictionary<string, float>();
        StartGame();
	}

    public void Update() {
        if (Input.GetKeyDown(powerP1) && Time.time > nextPower["p1"])
            nextPower["p1"] = Time.time + cooldownTime;
        else if (Input.GetKeyDown(powerP2) && Time.time > nextPower["p2"])
            nextPower["p2"] = Time.time + cooldownTime;
    }

    public void StartGame() {
        ball.transform.position = new Vector2(0, 0);
        p1Score = 0;
        p2Score = 0;
        nextPower["p1"] = 0;
        nextPower["p2"] = 0;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown() {
        countdown.enabled = true;
        for (int i=3; i>0; i--) {
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdown.enabled = false;
        NewRound(0, 0);
    }

    public void NewRound(int player, float time) {
        if (player == 1)
            p1Score++;
        else if (player == 2)
            p2Score++;

        score.text = "<color=#FF2526>" + p1Score + "</color> / " + "<color=#004DFF>" + p2Score + "</color>";

        ball.transform.position = new Vector2(0f, 0f);
        ballRB.velocity = new Vector2(0f, 0f);
        Invoke("GoBall", time);
    }

    public void GoBall() {
        float x = Random.Range(0f, 1f);
        if (x <= 0.5f)
            ballRB.AddForce(new Vector2(13f, 6f));
        else ballRB.AddForce(new Vector2(-13f, -6f));
    }
}
