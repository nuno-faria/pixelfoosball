using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public Text winner;
    public Text score;
    public ParticleSystem ps;

	void Start () {
        gameOver();
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetAxis("Quitp1") == 1)
            SceneManager.LoadScene("MenuScene");
    }

    private void gameOver() {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        ParticleSystem.MainModule main = ps.main;
        ParticleSystem.EmissionModule emiss = ps.emission;
        if (GameManager.gm.p1Score > GameManager.gm.p2Score) {
            winner.text = "P1 Wins!";
            Color c = GameManager.gm.p1Color;
            main.startColor = new Color(c.r, c.g, c.b, 1);
        }
        else if (GameManager.gm.p2Score > GameManager.gm.p1Score) {
            winner.text = "P2 Wins!";
            Color c = GameManager.gm.p2Color;
            main.startColor = new Color(c.r, c.g, c.b, 1);
        }
        else {
            winner.text = "Draw";
            emiss.enabled = false;
        }

        score.text = "Final Score: " + GameManager.gm.p1Score + "-" + GameManager.gm.p2Score;
    }
}
