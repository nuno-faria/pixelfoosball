using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public static MenuManager mm;

    public Color p1Color;
    public Color p2Color;

    public InputField goalLimit;
    public InputField timeLimit;

    public int goalLimitValue;
    public int timeLimitValue;

    public bool ai;

    void Awake() {
        mm = this;
        goalLimitValue = 0;
        timeLimitValue = 0;
    }

    public void StartGame1P() {
        applyLimits();
        ai = true;
        SceneManager.LoadScene("gameScene");
    }

    public void StartGame2P() {
        applyLimits();
        ai = false;
        SceneManager.LoadScene("gameScene");
    }

    private void applyLimits() {
        try {
            if (goalLimit.text != null)
                goalLimitValue = int.Parse(goalLimit.text);

            if (timeLimit.text != null)
                timeLimitValue = int.Parse(timeLimit.text);
        }
        catch (System.Exception) {
            ;
        }
    }
}
