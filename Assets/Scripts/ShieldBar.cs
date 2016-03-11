using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour {

	public ControllableShip ship;

	private Vector2 barSize = new Vector2(-1, -1);

	// Use this for initialization
	void Start() {
		Game.eventManager.addListener("SHIELD_TAKE_DAMAGE", onShieldDamage);
		onShieldDamage(ship.GetComponentInChildren<Shield>());
	}

	void onShieldDamage(object e) {
		if (barSize.x == -1) {
			barSize = this.transform.Find("Bar").gameObject.GetComponent<RectTransform>().rect.size;
		}
		Shield s = (Shield)e;
		float val = Mathf.Clamp((float)s.health / (float)s.startHealth, 0, 100);
		this.GetComponentInChildren<Text>().text = string.Format("{0:P0}", val);
		this.transform.Find("Bar").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(barSize.x * val, barSize.y);
	}

	// Update is called once per frame
	void Update() {

	}
}
