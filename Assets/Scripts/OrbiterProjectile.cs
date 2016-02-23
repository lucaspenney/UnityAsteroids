using UnityEngine;
using System.Collections;

public class OrbiterProjectile : MonoBehaviour {

	public Rigidbody2D orbitTarget;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay2D(Collision2D collision) {
		if (collision.gameObject.GetComponent<Orbiter>() == null) {
			collision.gameObject.AddComponent<Orbiter>();
		}
		if (orbitTarget != null) {

			collision.gameObject.GetComponent<Orbiter>().disabled = false;
			collision.gameObject.GetComponent<Orbiter>().target = orbitTarget;
		}
		Destroy(this.gameObject);
	}
}
