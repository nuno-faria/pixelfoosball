using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class MenuManager : MonoBehaviour {

	public static MenuManager mm;

    public Color p1Color;
    public Color p2Color;

    public InputField goalLimit;
    public InputField timeLimit;

    public int goalLimitValue;
    public int timeLimitValue;

    public bool ai;

    public int difficulty;

    public GameObject p1Keyboard;
    public GameObject p2Keyboard;
    public GameObject p1Joystick;
    public GameObject p2Joystick;

    void Awake() {
        mm = this;
        goalLimitValue = 0;
        timeLimitValue = 0;
    }


    void Update() {
        string[] controllers = Input.GetJoystickNames();
        int nControllers = controllers.Length;
        if (nControllers == 0 || controllers[0] == "") {
            p1Keyboard.SetActive(true);
            p1Joystick.SetActive(false);
            p2Keyboard.SetActive(true);
            p2Joystick.SetActive(false);
        }
        if (nControllers >= 1 && controllers[0] != "") {
            p1Keyboard.SetActive(false);
            p1Joystick.SetActive(true);
        }
        if (nControllers >= 2 && controllers[1] != "") {
            p2Keyboard.SetActive(false);
            p2Joystick.SetActive(true);
        }
        else {
            p2Keyboard.SetActive(true);
            p2Joystick.SetActive(false);
        }
        
    }

    public void StartGame1PEasy() {
        applyLimits();
        ai = true;
        difficulty = 0;
        SceneManager.LoadScene("gameScene");
    }

    public void StartGame1PNormal() {
        applyLimits();
        ai = true;
        difficulty = 1;
        SceneManager.LoadScene("gameScene");
    }

    public void StartGame1PHard() {
        applyLimits();
        ai = true;
        difficulty = 2;
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
