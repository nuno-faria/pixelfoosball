using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    [Header("Scene Atributtes")]
    public GameObject ball;
    public Rigidbody2D ballRB;
    public Text score;
    public Text countdown;
    public Text timeText;
    public AudioSource audio;
    public AudioClip powerClip;
    public AudioClip countdownClip;
    public AudioClip countdownFinalClip;

    //theres a glitch with the particles when the ball translates
    //this is here to disable the particles when there is a goal to stop the glitch
    private ParticleSystem.EmissionModule ballParticleEmission;

    [Header("Control Vars")]
    public int p1Score;
    public int p2Score;
    private int goalLimit;
    private int timeLimit;
    private float beginTime;
    private float currentTime;
    public bool ai;
    public float cooldownTime;
    public Dictionary<string, float> nextPower;
    public KeyCode powerP1;
    public KeyCode powerP2;
    public Color p1Color;
    public Color p2Color;
    public bool paused;
    public int difficulty;

    private void Awake() {
        gm = this;
        p1Color = MenuManager.mm.p1Color;
        p2Color = MenuManager.mm.p2Color;
        goalLimit = MenuManager.mm.goalLimitValue;
        timeLimit = MenuManager.mm.timeLimitValue;
        ai = MenuManager.mm.ai;
        difficulty = MenuManager.mm.difficulty;
        timeText.enabled = false;
        paused = false;
        beginTime = Mathf.Infinity;
        ballParticleEmission = ball.GetComponent<ParticleSystem>().emission;
        ballRB = ball.GetComponent<Rigidbody2D>();
        nextPower = new Dictionary<string, float>();
        audio = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        StartGame();
    }

    public void Update() {
        //inputs
        if ((Input.GetKeyDown(powerP1) || Input.GetAxis("Dashp1") == 1) && Time.time > nextPower["p1"] && !paused) {
            nextPower["p1"] = Time.time + cooldownTime;
            audio.clip = powerClip;
            audio.Play();
        }
        else if (!ai && (Input.GetKeyDown(powerP2) || Input.GetAxis("Dashp2") == 1) && Time.time > nextPower["p2"] && !paused) {
            nextPower["p2"] = Time.time + cooldownTime;
            audio.clip = powerClip;
            audio.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetAxis("Quitp1") == 1)
            SceneManager.LoadScene("menuScene");
        else if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Pausep1"))
            pauseUnpauseGame();

        //update time
        timeText.text = System.Math.Floor(Time.time - beginTime).ToString();
        currentTime = (float) System.Math.Floor(Time.time - beginTime);

        //ai
        if (ai)
            AIManager.processAI();

        //check if game is over
        checkGameOver();
    }

    private void checkGameOver() {
        if ((timeLimit != 0 && currentTime >= timeLimit) || (goalLimit != 0 && (p1Score == goalLimit || p2Score == goalLimit)))
            SceneManager.LoadScene("gameOverScene");
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
        beginTime = Time.time;
        timeText.enabled = true;
        NewRound(0, 0);
    }

    public void NewRound(int player, float time) {
        ballParticleEmission.enabled = false;

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

        ballParticleEmission.enabled = true;
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
