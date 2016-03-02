using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	public float duration = 5;
	public float strength = 100;
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
			if (!gameObject.GetComponent<AudioSource>().isPlaying && gameObject.GetComponent<AudioSource>().enabled) {
				gameObject.GetComponent<AudioSource>().Play();
			}


			Rigidbody2D r = this.GetComponent<Rigidbody2D>();
			r.mass += strength;

			ParticleSystem ps = this.GetComponent<ParticleSystem>();
			ParticleSystem.EmissionModule em = ps.emission;

			ParticleSystem.MinMaxCurve mmc = em.rate;
			mmc.mode = ParticleSystemCurveMode.Constant;
			mmc.constantMax *= 1.1f;
			mmc.constantMin *= 1.1f;
			em.rate = mmc;
		}
	}
}
