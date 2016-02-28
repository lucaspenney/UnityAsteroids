using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Rigidbody2D parent;
	public Projectile bullet;

	public float projectileSpeed = 1000f;
	public float fireRate = 0.5f;

	private float lastFire = 0f;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void aimAt(Vector2 pos) {
		Vector2 dir = new Vector2(transform.position.x, transform.position.y) - pos;
		this.gameObject.transform.rotation = Quaternion.Euler(0f,0f,Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 90);
	}

	public void shoot() {
		lastFire = Time.time;
		Projectile b = (Projectile)Instantiate (bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
		b.transform.rotation = transform.rotation;
		b.transform.Rotate(new Vector3(0f,0f,0f)); //Fix rotation
		Rigidbody2D body = b.gameObject.GetComponent<Rigidbody2D>();
		body.velocity = parent.velocity;
		
		float x = Mathf.Cos ((transform.eulerAngles.z - 270) * Mathf.Deg2Rad) * projectileSpeed;
		float y = Mathf.Sin ((transform.eulerAngles.z - 270) * Mathf.Deg2Rad) * projectileSpeed;
		
		body.AddForce(new Vector2(x, y));
		
		Physics2D.IgnoreCollision(parent.GetComponent<Collider2D>(), b.gameObject.GetComponent<BoxCollider2D>());
		Physics2D.IgnoreCollision(this.parent.GetComponentInChildren<CircleCollider2D>(), b.gameObject.GetComponent<BoxCollider2D>());
	}
}
