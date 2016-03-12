using UnityEngine;
using System.Collections;

public class AsteroidField : MonoBehaviour {

	public GameObject parent;
	public Asteroid asteroid;

	public float spawnRate = 0.5f;
	public float minSpawnRange = 10f;
	public float despawnRange = 20f;
	public int maxAsteroids = 100;
	private float lastSpawn = 0f;



	// Use this for initialization
	void Start() {
		Vector2 centerPos = parent.transform.position;
		centerPos.y -= 60;
		for (int i = 10;i < maxAsteroids;i++) {
			spawnAsteroid(centerPos);
		}

	}
	
	// Update is called once per frame
	void Update() {
		if (Time.time - lastSpawn > spawnRate) {
			
			Vector2 centerPos = parent.transform.position;
			Asteroid[] asteroids = (Asteroid[])GameObject.FindObjectsOfType(typeof(Asteroid));

			if (asteroids.Length < maxAsteroids) {
				for (int i = 0;i < 5;i++) {
					spawnAsteroid(centerPos);
					lastSpawn = Time.time;
				}
			}
			for (int i = 0;i < asteroids.Length;i++) {
				Vector2 thisPos = new Vector2(asteroids[i].transform.position.x, asteroids[i].transform.position.y);
				Vector2 diff = centerPos - thisPos;

				if (diff.magnitude > despawnRange) {
					Destroy(asteroids[i].gameObject);
				}
			}
		}
	}

	private void spawnAsteroid(Vector2 centerPos) {
		Vector2 pos = Random.insideUnitCircle;
		pos.x *= despawnRange;
		pos.y *= despawnRange;
		pos.x += parent.transform.position.x;
		pos.y += parent.transform.position.y;
		Vector2 diff = centerPos - pos;
		if (diff.magnitude < minSpawnRange) {
			spawnAsteroid(centerPos);
			return;
		}
		Asteroid a = Asteroid.createAsteroid(pos, new Vector2(0, 0), 500, "large");
		Rigidbody2D ar = a.GetComponent<Rigidbody2D>();
		ar.angularVelocity += (Random.value - 0.5f) * 50f;
		ar.AddForce(new Vector2((Random.value - 0.5f) * 330f, (Random.value - 0.5f) * 330f));

		Rigidbody2D pr = parent.GetComponent<Rigidbody2D>();
		Vector2 dist = parent.transform.position - a.transform.position;
		ar.AddForce((pr.velocity) * Random.Range(-5, 10));
		ar.AddForce(dist * Random.Range(-5, 25));
	}
}
