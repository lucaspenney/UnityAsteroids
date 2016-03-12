using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
		RawImage image = this.GetComponent<UnityEngine.UI.RawImage>();
		Color color = image.color;
		if (color.a < 0.05f) {
			Destroy(this.gameObject);
			return;
		}

		image.color = new Color(color.r, color.b, color.g, color.a *= 0.99f);
	}
}
