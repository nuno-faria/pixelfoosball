using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Color p1Color;
    public Color p2Color;

    public AudioSource audio;
    public AudioClip powerClip;
    public AudioClip countdownClip;
    public AudioClip countdownFinalClip;

    public bool paused;

    private void Awake() {
        gm = this;
        p1Color = MenuManager.mm.p1Color;
        p2Color = MenuManager.mm.p2Color;
        paused = false;
    }

    void Start () {
        ballRB = ball.GetComponent<Rigidbody2D>();
        nextPower = new Dictionary<string, float>();
        audio = GetComponent<AudioSource>();
        StartGame();
	}

    public void Update() {
        if (Input.GetKeyDown(powerP1) && Time.time > nextPower["p1"] && !paused) {
            nextPower["p1"] = Time.time + cooldownTime;
            audio.clip = powerClip;
            audio.Play();
        }
        else if (Input.GetKeyDown(powerP2) && Time.time > nextPower["p2"] && !paused) {
            nextPower["p2"] = Time.time + cooldownTime;
            audio.clip = powerClip;
            audio.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("menuScene");
        else if (Input.GetKeyDown(KeyCode.P))
            pauseUnpauseGame();
    }

    public void StartGame() {
        score.enabled = false;
        ball.transform.position = new Vector2(0, -0.24f);
        p1Score = 0;
        p2Score = 0;
        nextPower["p1"] = 0;
        nextPower["p2"] = 0;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown() {
        countdown.enabled = true;
        audio.volume = 0.3f;
        audio.clip = countdownClip;
        for (int i=3; i>0; i--) {
            countdown.text = i.ToString();
            audio.Play();
            yield return new WaitForSeconds(1f);
        }
        countdown.enabled = false;
        score.enabled = true;
        audio.clip = countdownFinalClip;
        audio.Play();
        audio.volume = 1f;
        NewRound(0, 0);
    }

    public void NewRound(int player, float time) {
        if (player == 1)
            p1Score++;
        else if (player == 2)
            p2Score++;

        string colorP1, colorP2;
        colorP1 = ColorUtility.ToHtmlStringRGB(p1Color);
        colorP2 = ColorUtility.ToHtmlStringRGB(p2Color);
        score.text = "<color=#" + colorP1 + ">" + p1Score + "</color> / " + "<color=#" + colorP2 + ">" + p2Score + "</color>";

        ball.transform.position = new Vector2(0f, -0.24f);
        ballRB.velocity = new Vector2(0f, 0f);
        Invoke("GoBall", time);
    }

    public void GoBall() {
        float x = Random.Range(0f, 1f);
        if (x <= 0.5f)
            ballRB.AddForce(new Vector2(13f, 6f));
        else ballRB.AddForce(new Vector2(-13f, -6f));
    }

    public Color getColor(string player) {
        if (player == "p1")
            return p1Color;
        else return p2Color;
    }

    private void pauseUnpauseGame() {
        if (!paused)
            Time.timeScale = 0;
        else 
            Time.timeScale = 1;

        paused = !paused;
    }
}
