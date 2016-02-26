using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;

	public float changeRate = 0.09f;
	private float lastChange = 0;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(new Vector3(Random.value,Random.value,Random.value * 2));
		Sprite s = this.gameObject.GetComponent<SpriteRenderer>().sprite;
		if (Time.time - lastChange > changeRate) {
			lastChange = Time.time;
			if (s == sprite1)
				this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
			else if (s == sprite2) {
				if (Random.value > 0.5)
					this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite3;
				else
					this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
			}
			else this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;

			this.gameObject.GetComponent<SpriteRenderer>().color = new Color(2525,255,255,Random.Range (0.7f,1f));
		}
	}
}
