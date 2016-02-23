using UnityEngine;
using System.Collections;

public class Orbiter : MonoBehaviour {

	public Rigidbody2D target;
	public float rotation;

	public bool disabled;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (disabled) {
			return;
		}
		Vector2 dist = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
		float r = dist.magnitude;
		Rigidbody2D myBody = this.GetComponent<Rigidbody2D>();
		r *= myBody.mass + 1;

		Vector2 tdist = new Vector2(dist.y, -dist.x).normalized;
		float force = Mathf.Sqrt( ((myBody.mass  + target.mass) / r));
		Vector2 targetVel = tdist * force;


		Vector2 velDiff = targetVel - myBody.velocity;

		//Add the difference to the velocity to get us back orbiting
		myBody.AddForce (velDiff, ForceMode2D.Impulse);

		//Rotation
		myBody.angularVelocity = rotation;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		//TODO: Check magnitude?
		disabled = true;
	}
}
