using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int health;

	private ParticleSystem engineParticles;

	public Vector2 targetPos;


	public bool engineOn = true;
	private float lastRetrograde = 0;
	private float lastThink = 0;

	public float maxSpeed = 25;
	public float forwardSpeed = 15;

	private float turnSpeedScale = 3f;

	// Use this for initialization
	void Start () {
		engineParticles = this.GetComponentInChildren<ParticleSystem>();
		lastThink = Time.time;
		lastRetrograde = Time.time;
	}

	// Update is called once per frame
	void Update () {
		
		Vector2 oldTargetPos = targetPos;
		Vector2 pos = new Vector2(transform.position.x, transform.position.y);

		Debug.DrawLine(transform.position, new Vector3(targetPos.x,targetPos.y, 0), Color.blue, 0.1f, true);
		Rigidbody2D r = this.GetComponent<Rigidbody2D>();
		RaycastHit2D[] obstacles = Physics2D.LinecastAll(transform.position, targetPos);
		bool obstructed = false;
		for (int i=0;i<obstacles.Length;i++) {
			if (obstacles[i].collider.gameObject.GetComponent<Asteroid>()) {
				obstructed = true;
			}
		}




		ControllableShip obj = (ControllableShip)GameObject.FindObjectOfType(typeof(ControllableShip));
		Vector2 tvel = obj.GetComponent<Rigidbody2D>().velocity;
		Vector2 mvel = this.GetComponent<Rigidbody2D>().velocity;
		Vector2 vdiff = (tvel - mvel);

		Vector2 dist = (Vector2)transform.position - targetPos;
		Vector2 futureOffPos = (Vector2)transform.position + (mvel * 10f);
		Vector2 futureOnPos = (Vector2)transform.position + (mvel + (getEngineForce() * 4f));
		Vector2 futureOffDist = futureOffPos - targetPos;
		Vector2 futureOnDist = futureOnPos - targetPos;

		float lastRotation = r.rotation * Mathf.Rad2Deg;
		lastThink = Time.time;
		targetPos = new Vector2(obj.transform.position.x, obj.transform.position.y);
		if (vdiff.magnitude > 18 && dist.magnitude > 10) {
			float diffMag = (vdiff.magnitude - 15) / 2;
			targetPos = new Vector2(obj.transform.position.x + (tvel.x * diffMag), obj.transform.position.y + (tvel.y * diffMag));
			lastRetrograde = Time.time;
			targetPos -= mvel * diffMag;
		}
		this.lookAt(targetPos);
		float currentRotation = r.rotation;



		if (!obstructed && Random.value > 0.95) {
			if (dist.magnitude < 15) {
				this.GetComponentInChildren<Weapon>().aimAt(targetPos);
				this.GetComponentInChildren<Weapon>().shoot();
			}
		}

		engineOn = false;

		Vector2 newVel = (mvel + this.getEngineForce());
		if (dist.magnitude > 8 && newVel.magnitude < this.maxSpeed) {
			engineOn = true;
		}


		if (engineOn) {
			r.AddForce(getEngineForce());
			engineParticles.Play();
		}
		else {
			engineParticles.Stop();
		}
	}

	Vector2 getEngineForce() {
		//Get force that engine would apply for current rotation
		Rigidbody2D r = this.GetComponent<Rigidbody2D>();
		float x = Mathf.Cos ((r.rotation - 90) * Mathf.Deg2Rad) * forwardSpeed;
		float y = Mathf.Sin ((r.rotation - 90) * Mathf.Deg2Rad) * forwardSpeed;
		return new Vector2(x,y);
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
		float r = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 270;
		gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f,0f,r), Time.time * this.turnSpeedScale);
	}
}
