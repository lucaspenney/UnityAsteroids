using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Game.eventManager.addListener ("PLAYER_TAKE_DAMAGE", onPlayerdamage);
	}

	void onPlayerdamage(object e) {
		ControllableShip p = (ControllableShip)e;
		this.GetComponentInChildren<Text> ().text = string.Format("{0:P2}", (float)p.health / (float)p.startHealth);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
