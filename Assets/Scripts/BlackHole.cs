using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	public float duration = 5;
	public float strength = 10000;
	public float startDelay = 1;
	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > duration + startDelay) {
			Destroy(this.gameObject);
		}

		if (Time.time - startTime > startDelay) {
			Rigidbody2D r = this.GetComponent<Rigidbody2D> ();
			r.mass += strength;
		}
	}
}
