using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Rigidbody2D parent;
	public Projectile bullet;
	public OrbiterProjectile orbiterBullet;
	public Rigidbody2D orbitTarget;

	public float projectileSpeed = 1000f;
	public float fireRate = 0.5f;

	private float lastFire = 0f;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float camDis = Camera.main.transform.position.y - transform.position.y;
		Vector3 mouse = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));
		float AngleRad = Mathf.Atan2 (mouse.y - transform.position.y, mouse.x - transform.position.x);
		float angle = (180 / Mathf.PI) * AngleRad;
		transform.eulerAngles = new Vector3(0,0,angle + 270);
		if (Input.GetMouseButton(0) && Time.time  - lastFire > fireRate) {
			fireLaser();
		}
		if (Input.GetMouseButton(1) && Time.time  - lastFire > fireRate) {
			fireOrbiter();
		}
	}

	private void fireLaser() {
		lastFire = Time.time;
		Projectile b = (Projectile)Instantiate (bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
		b.transform.rotation = transform.rotation;
		b.transform.Rotate(new Vector3(0f,0f,180f)); //Fix rotation
		Rigidbody2D body = b.gameObject.GetComponent<Rigidbody2D>();
		body.velocity = parent.velocity;
		
		float x = Mathf.Cos ((transform.eulerAngles.z - 270) * Mathf.Deg2Rad) * projectileSpeed;
		float y = Mathf.Sin ((transform.eulerAngles.z - 270) * Mathf.Deg2Rad) * projectileSpeed;
		
		body.AddForce(new Vector2(x, y));
		
		Physics2D.IgnoreCollision(parent.GetComponent<Collider2D>(), b.gameObject.GetComponent<BoxCollider2D>());
		Physics2D.IgnoreCollision(this.parent.GetComponentInChildren<CircleCollider2D>(), b.gameObject.GetComponent<BoxCollider2D>());
	}

	private void fireOrbiter() {
		lastFire = Time.time;
		OrbiterProjectile b = (OrbiterProjectile)Instantiate (orbiterBullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
		b.orbitTarget = orbitTarget;
		b.transform.rotation = transform.rotation;
		b.transform.Rotate(new Vector3(0f,0f,180f)); //Fix rotation
		Rigidbody2D body = b.gameObject.GetComponent<Rigidbody2D>();
		body.velocity = parent.velocity;
		
		float x = Mathf.Cos ((transform.eulerAngles.z - 270) * Mathf.Deg2Rad) * projectileSpeed;
		float y = Mathf.Sin ((transform.eulerAngles.z - 270) * Mathf.Deg2Rad) * projectileSpeed;
		
		body.AddForce(new Vector2(x, y));
		
		Physics2D.IgnoreCollision(parent.GetComponent<Collider2D>(), b.gameObject.GetComponent<BoxCollider2D>());
		Physics2D.IgnoreCollision(this.parent.GetComponentInChildren<CircleCollider2D>(), b.gameObject.GetComponent<BoxCollider2D>());
	}
}
