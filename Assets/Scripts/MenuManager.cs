using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public static MenuManager mm;

    public Color p1Color;
    public Color p2Color;

    void Awake() {
        mm = this;
    }

    public void StartGame() {
        SceneManager.LoadScene("gameScene");
    }
}
