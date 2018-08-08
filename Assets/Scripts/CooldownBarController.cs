using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBarController : MonoBehaviour {

    public string player;
    private Image image;
    private bool ai;

    void Start() {
        image = GetComponent<Image>();
        image.color = GameManager.gm.getColor(player);
        ai = GameManager.gm.ai;
        if (ai && player == "p2")
            image.enabled = false;
        else image.enabled = true;
    }

    public void Update() {
        float delta = (GameManager.gm.nextPower[player] - Time.time) / GameManager.gm.cooldownTime;
        if (delta < 0) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            delta = 0;
        }
        else
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);

        image.fillAmount = 1 - delta;
    }
}
