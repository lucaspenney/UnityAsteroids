using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public GameObject target;

	private Vector2 targetPos;
	public float turnSpeedScale = 3f;
	public float forwardSpeed = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!target) {
			findTarget();
		}
		setTargetPosition();
		lookAt(targetPos);
		Rigidbody2D r = this.GetComponent<Rigidbody2D>();
		r.AddForce(getEngineForce());
	}

	void findTarget() {
		Enemy[] enemies = (Enemy[])GameObject.FindObjectsOfType (typeof(Enemy));
		float lastDist = 0;
		for (int i=0;i<enemies.Length;i++) {
			float dist = (enemies[i].transform.position - gameObject.transform.position).magnitude;
			if (dist < lastDist || lastDist == 0) {
				target = enemies[i].gameObject;
				lastDist = dist;
			}
		}
	}

	void setTargetPosition() {
		if (!target) return;
		Vector2 dist = (Vector2)(this.gameObject.transform.position - target.transform.position);
		Vector2 tvel = target.GetComponentInChildren<Rigidbody2D>().velocity;
		Vector2 mvel = this.GetComponent<Rigidbody2D>().velocity;

		targetPos = (Vector2)target.transform.position;
		targetPos += (tvel * (dist.magnitude / mvel.magnitude / 2));
	}

	void OnCollisionEnter2D(Collision2D collision) {
		IDamageable d = collision.gameObject.GetComponentInChildren<IDamageable>();
		if (d != null) {
			d.takeDamage(10000);
		}
		Explosion prefab = (Explosion)Resources.Load ("Prefabs/ExplosionMedium", typeof(Explosion));
		Instantiate(prefab, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}

	Vector2 getEngineForce() {
		//Get force that engine would apply for current rotation
		Rigidbody2D r = this.GetComponent<Rigidbody2D>();
		float x = Mathf.Cos ((r.rotation + 90) * Mathf.Deg2Rad) * forwardSpeed;
		float y = Mathf.Sin ((r.rotation + 90) * Mathf.Deg2Rad) * forwardSpeed;
		return new Vector2(x,y);
	}

	private void lookAt(Vector2 target) {
		Vector2 dir = new Vector2(transform.position.x, transform.position.y) - target;
		float r = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 90;
		gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f,0f,r), Time.time * this.turnSpeedScale);
	}
}
