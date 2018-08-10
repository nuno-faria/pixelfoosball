using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    public int player;
    public AudioSource audio;
    public AudioClip goal;
     
    public List<GameObject> spectators;
    private List<SpectatorController> spectatorsControllers;

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.clip = goal;
        spectatorsControllers = new List<SpectatorController>();
        spectators.ForEach(x => spectatorsControllers.Add(x.GetComponent<SpectatorController>()));
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "ball") {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f;
            audio.Play();
            spectatorsControllers.ForEach(x => x.celebrate());
            GameManager.gm.NewRound(player, 0.65f);
        }
    }
}
