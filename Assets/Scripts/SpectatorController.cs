using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorController : MonoBehaviour {

    public string player;
    private Rigidbody2D rb;

    public Shader shader;
    private Matrix4x4 colorMatrix {
        get {
            Matrix4x4 mat = new Matrix4x4();
            mat.SetRow(0, new Vector4(0.16f, 0.16f, 0.16f, 1f));
            Color c = GameManager.gm.getColor(player);
            float r = Random.Range(-0.4f, 0.4f);
            float g = Random.Range(-0.4f, 0.4f);
            float b = Random.Range(-0.4f, 0.4f);
            mat.SetRow(2, new Vector4(c.r + r, c.g + g, c.b + b, c.a));
            mat.SetRow(1, new Vector4(0.51f, 0.27f, 0f, 1f));
            mat.SetRow(3, new Vector4(1f, 0.76f, 0.56f, 1f));
            return mat;
        }
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        setUpMaterial();
    }

    private void setUpMaterial() {
        Material mat = new Material(shader);
        mat.SetMatrix("_ColorMatrix", colorMatrix);
        GetComponent<SpriteRenderer>().material = mat;
    }

    public void celebrate() {
        float force = Random.Range(-12f, 12f);
        if (System.Math.Abs(force) < 3f)
            force = 3 * System.Math.Sign(force);
        rb.AddForce(new Vector2(force, 0));
    }
}
