using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float duration;
	private float startTime;

	// Use this for initialization
	void Start() {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update() {
		if (Time.time - duration > startTime) {
			Destroy(this.gameObject);
		}
	}

	public static void createExplosion(string name, Vector3 position) {
		Explosion prefab = (Explosion)Resources.Load("Prefabs/ExplosionMedium2", typeof(Explosion));
		Explosion a = (Explosion)Instantiate(prefab, position, Quaternion.identity);
	}

}
