using System;
using UnityEngine;

public class PowerUp : MonoBehaviour, IPowerUp
{
	public GameObject recipient;

	public float decayTime = 5;
	protected float spawnTime = 0;

	void Start() {
		spawnTime = Time.time;
	}

	void Update() {
		Rigidbody2D r = this.GetComponent<Rigidbody2D>();

		ControllableShip[] players = (ControllableShip[])GameObject.FindObjectsOfType (typeof(ControllableShip));
		if (players.Length > 0) {
			Vector2 dist = (Vector2)(players[0].gameObject.transform.position - this.gameObject.transform.position);
			if (dist.magnitude < 5) {
				Vector2 vel = (Vector2) players[0].gameObject.GetComponent<Rigidbody2D>().velocity;
				r.velocity = ((dist * 2) + vel);
			}
			else {
				dist.Normalize();
				r.AddForce(dist * 2, ForceMode2D.Impulse);
			}
		}

		if (Time.time - spawnTime > decayTime - 1) {
			SpriteRenderer re = this.GetComponent<SpriteRenderer>();
			re.color = new Color(255, 255, 255, (re.color.a * 0.95f));
			if (re.color.a < 0.05) {
				Destroy(this.gameObject);
			}
		}
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.GetComponent<ControllableShip>() != null) {	
			receivePowerUp(collision.gameObject);
			Destroy(this.gameObject);
		}
	}

	public virtual void receivePowerUp(GameObject obj) {

	}
}

