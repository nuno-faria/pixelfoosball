using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    public int player;

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "ball")
            GameManager.gm.NewRound(player, 0.6f);
    }

}
