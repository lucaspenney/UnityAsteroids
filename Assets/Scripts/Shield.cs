using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour, IDamageable {

	public int startHealth = 5000;
	public int health;

	public Sprite fullSprite;
	public Sprite halfSprite;
	public Sprite lowSprite;

	// Use this for initialization
	void Start() {
		health = startHealth;
		this.GetComponent<SpriteRenderer>().sprite = fullSprite;
		Physics2D.IgnoreCollision(this.GetComponentInParent<Collider2D>(), this.GetComponent<CircleCollider2D>());
	}
	
	// Update is called once per frame
	void Update() {
		SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

		if (health / startHealth > 0.7) {
			renderer.sprite = fullSprite;
		} else if (health / startHealth > 0.4) {
			renderer.sprite = halfSprite;
			transform.localPosition = new Vector2(0, -0.01f);
		} else if (health > 0) {
			renderer.sprite = lowSprite;
			transform.localPosition = new Vector2(0, -0.01f);
		} else {
			renderer.sprite = null;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.relativeVelocity.magnitude > 5) {
			this.takeDamage((int)collision.relativeVelocity.magnitude * 2);
		}
	}

	public void takeDamage(int damage) {
		Game.eventManager.dispatch("SHIELD_TAKE_DAMAGE", this);
		this.health -= damage;
		//Actually disabling the collider seems to cause rigidbody problems...so lets just make it too small to matter
		if (this.health <= 0) {
			this.GetComponent<CircleCollider2D>().radius = 0.01f;
		} else {
			this.GetComponent<CircleCollider2D>().radius = 0.6f;
		}
	}
}
