using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public ParticleSystem ps;
    public Color defaultColor;
    public Color p1;
    public Color p2;

    void Start () {
        ps = GetComponent<ParticleSystem>();	
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        ParticleSystem.MainModule main =  ps.main;
        if (collision.tag == "p1")
            main.startColor = p1;
        else if (collision.tag == "p2")
            main.startColor = p2;
        else if (collision.tag == "goal")
            main.startColor = defaultColor;
    }
}
