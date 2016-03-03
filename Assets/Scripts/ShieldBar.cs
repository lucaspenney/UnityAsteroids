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
		this.GetComponentInChildren<Text> ().text = string.Format("{0:P2}", (float)s.health / (float)s.startHealth);
	}

	// Update is called once per frame
	void Update () {

	}
}
