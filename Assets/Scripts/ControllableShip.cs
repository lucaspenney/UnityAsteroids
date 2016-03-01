using UnityEngine;
using System.Collections;

public class ControllableShip : MonoBehaviour {

	public Rigidbody2D rigidBody;
	public Camera myCamera;
	public AudioSource shieldsSound;
	public AudioSource engineSound;
	public ParticleSystem engineParticles;

	public float maxSpeed;
	public float turnSpeed;
	public float forwardSpeed;
	public float boostSpeed;

	public int health;
	
	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody2D>();
		myCamera = Camera.main;
		shieldsSound = this.GetComponents<AudioSource>()[0];
		engineSound = this.GetComponents<AudioSource> ()[1];
		engineParticles = this.GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			float x, y;
			if (Input.GetKey (KeyCode.LeftShift)) {
				x = Mathf.Cos ((rigidBody.rotation - 270) * Mathf.Deg2Rad) * boostSpeed;
				y = Mathf.Sin ((rigidBody.rotation - 270) * Mathf.Deg2Rad) * boostSpeed;
				engineParticles.startColor = Color.white;
			} else {
				x = Mathf.Cos ((rigidBody.rotation - 270) * Mathf.Deg2Rad) * forwardSpeed;
				y = Mathf.Sin ((rigidBody.rotation - 270) * Mathf.Deg2Rad) * forwardSpeed;

				engineParticles.startColor = Color.gray;
			}
			Vector2 newVel = rigidBody.velocity;
			newVel += new Vector2(x,y);
			if (newVel.magnitude <= this.maxSpeed) {
				rigidBody.AddForce (new Vector2 (x, y));
			}

			engineParticles.Play();
			if (!engineSound.isPlaying) {
				engineSound.Play();
			}

		} else {
			engineSound.Stop();
			engineParticles.Stop();
		}
		if (Input.GetKey (KeyCode.A)) {
			if (rigidBody.angularVelocity < 0) rigidBody.angularVelocity += turnSpeed;
			rigidBody.angularVelocity += turnSpeed;
		}
		if (Input.GetKey (KeyCode.D)) {
			if (rigidBody.angularVelocity > 0) rigidBody.angularVelocity -= turnSpeed;
			rigidBody.angularVelocity -= turnSpeed;
		}

		if (Input.GetKey(KeyCode.Space)) {
			if (GetComponentInChildren<Weapon>().canShoot()) {
				this.GetComponentInChildren<Weapon>().rotateTo(this.transform.rotation.eulerAngles.z);
				this.GetComponentInChildren<Weapon>().shoot();
			}
		}


		Vector3 newPos =  new Vector3(rigidBody.position.x, rigidBody.position.y, -5);
		myCamera.transform.position = newPos;

	}

	void OnCollisionEnter2D(Collision2D collision) {
		foreach (ContactPoint2D contact in collision.contacts) {

		}
		// Play a sound if the colliding objects had a big impact.		
		if (collision.relativeVelocity.magnitude > 0) {
			shieldsSound.Play();
		}
		if (collision.relativeVelocity.magnitude > 5) {
			this.takeDamage((int)collision.relativeVelocity.magnitude * 2);
		}
	}

	public void takeDamage(int damage) {
		this.health -= damage;
		Explosion prefab = (Explosion)Resources.Load ("Prefabs/ExplosionRed", typeof(Explosion));
		Explosion a = (Explosion)Instantiate(prefab, transform.position, Quaternion.identity);
		a.transform.parent = gameObject.transform;
		a.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		if (this.health <= 0) {
			Explosion prefab2 = (Explosion)Resources.Load ("Prefabs/ExplosionLarge", typeof(Explosion));
			Explosion a2 = (Explosion)Instantiate(prefab2, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
