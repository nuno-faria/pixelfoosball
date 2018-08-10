using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Animator anim;
    private Rigidbody2D playerRB;
    private ParticleSystem ps;
    public string player;

    public KeyCode up;
    public KeyCode down;
    public KeyCode power;

    public float speed;
    private float nextPower;
    private bool powerActivated;

    public Shader shader;
    private Matrix4x4 colorMatrix {
        get {
            Matrix4x4 mat = new Matrix4x4();
            mat.SetRow(0, new Vector4(0.16f, 0.16f, 0.16f, 1f));
            Color c = GameManager.gm.getColor(player);
            mat.SetRow(2, new Vector4(c.r, c.g, c.b, c.a));
            mat.SetRow(1, new Vector4(0.51f, 0.27f, 0f, 1f));
            mat.SetRow(3, new Vector4(1f, 0.76f, 0.56f, 1f));
            return mat;
        }
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("kick", false);
        playerRB = GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();
        powerActivated = false;

        ParticleSystem.EmissionModule emiss = ps.emission;
        emiss.enabled = false;

        setUpMaterial();
    }

    private void setUpMaterial() {
        Material mat = new Material(shader);
        mat.SetMatrix("_ColorMatrix", colorMatrix);
        GetComponent<SpriteRenderer>().material = mat;
    }

    public void Update() {

        if (!(GameManager.gm.ai && player == "p2")) { //if ai is playing, ignore p2
            //movement
            if (Input.GetKey(up) || Input.GetAxis("Vertical" + player) == -1)
                playerRB.velocity = new Vector2(0, speed);

            else if (Input.GetKey(down) || Input.GetAxis("Vertical" + player) == 1)
                playerRB.velocity = new Vector2(0, -speed);

            else playerRB.velocity = new Vector2(0, 0);
        }

        //power speed
        if ((Input.GetKeyDown(power) || Input.GetAxis("Dash" + player) == 1) && Time.time > nextPower && !GameManager.gm.paused) {
            nextPower = Time.time + GameManager.gm.cooldownTime;
            ParticleSystem.EmissionModule emiss = ps.emission;
            emiss.enabled = true;
            StartCoroutine(powerSpeed());
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
    }

    private IEnumerator powerSpeed() {
        speed *= 2.5f;
        powerActivated = true;
        yield return new WaitForSeconds(0.125f);
        speed /= 2.5f;
        powerActivated = false;
        ParticleSystem.EmissionModule emiss = ps.emission;
        emiss.enabled = false;
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.name == "ball") {
            anim.SetBool("kick", true);
            Rigidbody2D ballRB = collision.GetComponent<Rigidbody2D>();
            float x = (3.5f + ballRB.velocity.x / 1.5f) + (playerRB.velocity.y / 1.85f) + (powerActivated ? 5 : 0);
            float y = (ballRB.velocity.y / 1.7f) + (playerRB.velocity.y / 1.1f);
            if (player == "p1")
                ballRB.velocity = new Vector2(Math.Max(Math.Abs(x), 8.5f),y);
            else ballRB.velocity = new Vector2(Math.Min(-1 * Math.Abs(x), -8.5f), y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name == "ball") {
            anim.SetBool("kick", false);
        }
    }
}
