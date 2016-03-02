using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public int health = 100;
	public int damage = 0;

	public AudioClip destroySound;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 vel = this.GetComponent<Rigidbody2D>().velocity;
		if (damage >= 500 && health == 500) {
			for (int i=0;i<3;i++) {
				createAsteroid(this.transform.position, vel, 100, "medium");
			}
			this.destroy();
		}
		else if (damage >= 100 && health == 100) {
			for (int i=0;i<5;i++) {
				vel *= 0.5f;
				createAsteroid(this.transform.position, vel, 20, "small");
			}
			this.destroy();
		}
		else if (damage >= 20 && health == 20) {
			this.destroy();
		}

	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.relativeVelocity.magnitude > 0) {
			Asteroid a = collision.gameObject.GetComponent<Asteroid>();
			if (a != null) {
				float damage = collision.relativeVelocity.magnitude * (a.GetComponent<Rigidbody2D>().mass + this.GetComponent<Rigidbody2D>().mass);
				damage *= Random.value;
				a.takeDamage((int)damage);
			}
		}
	}

	public static Asteroid createAsteroid(Vector2 pos, Vector2 vel, int health, string size) {
		Asteroid prefab = (Asteroid)Resources.Load ("Prefabs/Asteroid", typeof(Asteroid));
		Asteroid a = (Asteroid)Instantiate(prefab, pos, Quaternion.identity);
		Object[] textures = Resources.LoadAll("images/Meteors/" + size + "/", typeof(Sprite));
		Sprite texture = (Sprite)textures[Random.Range(0, textures.Length)];
		a.health = health;
		a.damage = 0;
		a.GetComponent<SpriteRenderer>().sprite = texture;
		a.gameObject.AddComponent<PolygonCollider2D>();
		float scale = 4.5f;
		if (size == "medium") scale = 1f;
		else scale = 0.8f;
		scale *= 2;
		a.gameObject.transform.localScale = new Vector3(Random.value + scale,Random.value + scale,Random.value + scale);
		PolygonCollider2D collider = a.gameObject.GetComponent<PolygonCollider2D>();
		a.gameObject.GetComponent<Rigidbody2D>().mass = collider.bounds.size.magnitude * 1;
		a.gameObject.GetComponent<Rigidbody2D>().AddForce(vel, ForceMode2D.Impulse);
		a.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range (-1,1), Random.Range (-1,1)), ForceMode2D.Impulse);
		return a;
	}
	
	public void takeDamage(int damage) {
		this.damage += damage;
	}

	public void destroy() {
		if (this.health == 20) {
			Explosion prefab = (Explosion)Resources.Load ("Prefabs/Explosion", typeof(Explosion));
			Explosion a = (Explosion)Instantiate(prefab, transform.position, Quaternion.identity);
		}
		AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("sounds/hurt", typeof(AudioClip)), Camera.main.transform.position);
		Destroy (this.gameObject);
	}
}
