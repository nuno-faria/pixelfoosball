using UnityEngine;

public class ColorIndicator : MonoBehaviour {

    public Color startColor;
    public string player;
	HSBColor color;

	void Start() {
		color = HSBColor.FromColor(startColor);
        GetComponent<Renderer>().material.SetColor("_Color", color.ToColor());
        transform.parent.BroadcastMessage("SetColor", color);
        setPlayerColor();
	}

	void ApplyColor ()
	{
		GetComponent<Renderer>().material.SetColor ("_Color", color.ToColor());
		transform.parent.BroadcastMessage("OnColorChange", color, SendMessageOptions.DontRequireReceiver);
        setPlayerColor();
    }

	void SetHue(float hue)
	{
		color.h = hue;
		ApplyColor();
    }	

	void SetSaturationBrightness(Vector2 sb) {
		color.s = sb.x;
		color.b = sb.y;
		ApplyColor();
	}

    private void setPlayerColor() {
        if (player == "p1")
            MenuManager.mm.p1Color = color.ToColor();
        else MenuManager.mm.p2Color = color.ToColor();
    }
}
