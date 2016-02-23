using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public int health;

	public Sprite fullSprite;
	public Sprite halfSprite;
	public Sprite lowSprite;

	public CircleCollider2D myCollider;

	// Use this for initialization
	void Start () {
		myCollider = this.GetComponent<CircleCollider2D>();
		this.GetComponent<SpriteRenderer>().sprite = fullSprite;
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D collision) {
		SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
		if (health > 0) health -= 1;

		if (health > 7) {
			renderer.sprite = fullSprite;
		}
		else if (health > 3) {
			renderer.sprite = halfSprite;
			transform.localPosition = new Vector2(0, -0.01f);
		}
		else if (health > 0) {
			renderer.sprite = lowSprite;
			transform.localPosition = new Vector2(0, -0.01f);
		}
		else {
			renderer.sprite = null;
			this.GetComponent<CircleCollider2D>().enabled = false;
		}
	}
}
