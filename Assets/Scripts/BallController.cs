using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public ParticleSystem ps;
    public Color defaultColor;
    public Color p1;
    public Color p2;

    public AudioSource audio;
    //public AudioClip goal;
    public AudioClip hit;

    void Start () {
        ps = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        audio.clip = hit;
        p1 = GameManager.gm.p1Color;
        p2 = GameManager.gm.p2Color;
        setColor();
	}

    private void setColor() {
        float h1, s1, v1, h2, s2, v2;
        Color.RGBToHSV(p1, out h1, out s1, out v1);
        Color.RGBToHSV(p2, out h2, out s2, out v2);
        s1 -= s1 / 5f;
        s2 -= s2 / 5f;

        p1 = Color.HSVToRGB(h1, s1, v1);
        p2 = Color.HSVToRGB(h2, s2, v2);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        ParticleSystem.MainModule main =  ps.main;
        if (collider.tag == "p1") {
            main.startColor = p1;
            audio.Play();
        }
        else if (collider.tag == "p2") {
            main.startColor = p2;
            audio.Play();
        }
        else if (collider.tag == "goal")
            main.startColor = defaultColor;
        else if (collider.tag == "slowMoArea") {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        //if (collider.tag == "goal")
            //audio.clip = goal;
        //else audio.clip = hit;
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "slowMoArea") {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        audio.clip = hit;
        audio.Play();
    }
}
