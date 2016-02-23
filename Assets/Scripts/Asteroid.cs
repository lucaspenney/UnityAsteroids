using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Object[] textures = Resources.LoadAll ("img/Meteors", typeof(Sprite));
		Sprite texture = (Sprite)textures[Random.Range(0, textures.Length)];
		this.GetComponent<SpriteRenderer>().sprite = texture;
		this.gameObject.AddComponent<PolygonCollider2D>();

		PolygonCollider2D collider = gameObject.GetComponent<PolygonCollider2D>();
		GetComponent<Rigidbody2D>().mass = collider.bounds.size.magnitude * 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.relativeVelocity.magnitude > 0) {
			collision.gameObject.GetComponent<Shield>();
		}
	}
}
