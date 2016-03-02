﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((gameObject.transform.position - Camera.main.transform.position).magnitude > 100) {
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Rigidbody2D myRb = this.GetComponent<Rigidbody2D>();
		Rigidbody2D theirRb = collision.gameObject.GetComponent<Rigidbody2D>();
		Vector2 velDiff = myRb.velocity - theirRb.velocity;

		if (collision.gameObject.GetComponent<FixedJoint2D> () == null) {
			theirRb.AddForce(velDiff * 0.6f, ForceMode2D.Impulse);
		}


		IDamageable impacted = collision.gameObject.GetComponent<IDamageable>();
		if (impacted != null) {
			impacted.takeDamage(250);
		}
		Destroy(this.gameObject);
	}
	
}
