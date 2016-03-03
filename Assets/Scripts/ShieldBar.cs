using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour {

	public ControllableShip ship;

	// Use this for initialization
	void Start () {
		Game.eventManager.addListener("SHIELD_TAKE_DAMAGE", onShieldDamage);
		onShieldDamage (ship.GetComponentInChildren<Shield>());
	}

	void onShieldDamage(object e) {
		Shield s = (Shield)e;
		float val = Mathf.Clamp((float)s.health / (float)s.startHealth, 0 , 100);
		this.GetComponentInChildren<Text> ().text = string.Format("{0:P2}", val);
	}

	// Update is called once per frame
	void Update () {

	}
}
