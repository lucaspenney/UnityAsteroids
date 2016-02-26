using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Asteroid impacted;
		if (impacted = collision.gameObject.GetComponent<Asteroid>()) {
			impacted.takeDamage(20);
		}
		Destroy(this.gameObject);
	}
	
}
