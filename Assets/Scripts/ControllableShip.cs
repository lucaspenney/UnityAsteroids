using UnityEngine;
using System.Collections;

public class ControllableShip : MonoBehaviour {

	public Rigidbody2D rigidBody;
	public Camera myCamera;
	public AudioSource shieldsSound;
	public AudioSource engineSound;
	public ParticleSystem engineParticles;

	public float turnSpeed;
	public float forwardSpeed;
	public float boostSpeed;
	
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
			if (Input.GetKey (KeyCode.LeftShift)) {
				float x = Mathf.Cos ((rigidBody.rotation - 270) * Mathf.Deg2Rad) * boostSpeed;
				float y = Mathf.Sin ((rigidBody.rotation - 270) * Mathf.Deg2Rad) * boostSpeed;
				rigidBody.AddForce (new Vector2 (x, y));
				engineParticles.startColor = Color.white;
			} else {
				float x = Mathf.Cos ((rigidBody.rotation - 270) * Mathf.Deg2Rad) * forwardSpeed;
				float y = Mathf.Sin ((rigidBody.rotation - 270) * Mathf.Deg2Rad) * forwardSpeed;
				rigidBody.AddForce (new Vector2 (x, y));
				engineParticles.startColor = Color.gray;
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
		
	}
}
