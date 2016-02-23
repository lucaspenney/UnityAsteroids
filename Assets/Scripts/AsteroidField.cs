using UnityEngine;
using System.Collections;

public class AsteroidField : MonoBehaviour {

	public int numAsteroids;
	public int fieldSize;

	public Asteroid asteroid;

	// Use this for initialization
	void Start () {
		for (int i=0;i<numAsteroids;i++) {

			Vector2 pos = Random.insideUnitCircle;
			pos.x *= fieldSize;
			pos.y *= fieldSize;
			Asteroid a = (Asteroid)Instantiate(asteroid, pos, Quaternion.identity);
			a.gameObject.GetComponent<Rigidbody2D>().angularVelocity += (Random.value - 0.5f) * 50f;
			a.gameObject.GetComponent<Rigidbody2D>().AddForce (new Vector2((Random.value - 0.5f) * 50f, (Random.value - 0.5f) * 50f));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
