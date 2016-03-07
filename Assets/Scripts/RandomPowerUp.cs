using UnityEngine;
using System.Collections;

public class RandomPowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Random.value < 0.2) {
			PowerUp prefab = (PowerUp)Resources.Load ("Prefabs/ShieldPowerUp", typeof(PowerUp));
			PowerUp a = (PowerUp)Instantiate(prefab, transform.position, Quaternion.identity);
		}
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
