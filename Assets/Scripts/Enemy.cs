using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int health;

	private ParticleSystem engineParticles;

	public Vector2 targetPos;


	private bool engineOn = true;
	public float forwardSpeed = 10;

	// Use this for initialization
	void Start () {
		engineParticles = this.GetComponentInChildren<ParticleSystem>();
	}

	// Update is called once per frame
	void Update () {
		Vector2 pos = new Vector2(transform.position.x, transform.position.y);

		this.findTarget();
		this.lookAt(targetPos);
		Rigidbody2D r = this.GetComponent<Rigidbody2D>();
		RaycastHit2D[] obstacles = Physics2D.LinecastAll(transform.position, targetPos);
		bool obstructed = false;
		for (int i=0;i<obstacles.Length;i++) {
			if (obstacles[i].collider.gameObject.GetComponent<Asteroid>()) {
				obstructed = true;
			}
		}

		if (!obstructed && Random.value > 0.95) {
			Vector2 dist = (Vector2)transform.position - targetPos;
			if (dist.magnitude < 15) {
				this.GetComponentInChildren<Weapon>().aimAt(targetPos);
				this.GetComponentInChildren<Weapon>().shoot();
			}
		}
		if (r.velocity.magnitude < 2 || (pos - targetPos).magnitude > 5) {
			engineOn = true;
		}
		else engineOn = false;
		if (engineOn) {
			float x = Mathf.Cos ((r.rotation - 90) * Mathf.Deg2Rad) * forwardSpeed;
			float y = Mathf.Sin ((r.rotation - 90) * Mathf.Deg2Rad) * forwardSpeed;
			r.AddForce (new Vector2 (x, y));
		}
	}

	public void findTarget() {
		ControllableShip obj = (ControllableShip)GameObject.FindObjectOfType(typeof(ControllableShip));
		targetPos = new Vector2(obj.transform.position.x, obj.transform.position.y);
		Vector2 tvel = obj.GetComponent<Rigidbody2D>().velocity;
		Vector2 mvel = this.GetComponent<Rigidbody2D>().velocity;
		Vector2 vdiff = tvel - mvel;
		if (mvel.magnitude > 2) {
			targetPos += (new Vector2(tvel.x / 4, tvel.y / 4));	
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.relativeVelocity.magnitude > 5) {
			this.takeDamage((int)collision.relativeVelocity.magnitude * 2);
		}
	}

	public void takeDamage(int damage) {
		this.health -= damage;
		if (this.health <= 0) {
			Explosion prefab = (Explosion)Resources.Load ("Prefabs/ExplosionMedium", typeof(Explosion));
			Explosion a = (Explosion)Instantiate(prefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}

	private void lookAt(Vector2 target) {
		Vector2 dir = new Vector2(transform.position.x, transform.position.y) - target;
		this.gameObject.transform.rotation = Quaternion.Euler(0f,0f,Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 270);
	}
}
