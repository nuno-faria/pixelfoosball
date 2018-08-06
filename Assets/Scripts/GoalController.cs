using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    public int player;
    public AudioSource audio;
    public AudioClip goal;

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.clip = goal;    
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "ball") {
            audio.Play();
            GameManager.gm.NewRound(player, 0.6f);
        }
    }

}
