using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

	static public float gravitationalConstant = 0.001f;

	float gravityRadius = 1200;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, gravityRadius);

		for (int i=0;i < hitColliders.Length;i++) {
			GameObject obj = hitColliders[i].gameObject;
			if (obj == this.gameObject) continue;
			Vector2 diff = new Vector2(this.transform.position.x - obj.gameObject.transform.position.x, this.transform.position.y - obj.gameObject.transform.position.y);
			float distSquared = (diff.x * diff.x + diff.y * diff.y);
			float dist = Mathf.Sqrt(distSquared);
			if (obj.GetComponent<Rigidbody2D>() != null && dist != 0) {
				float force = (this.GetComponent<Rigidbody2D>().mass * obj.GetComponent<Rigidbody2D>().mass) / distSquared;
				force *= gravitationalConstant;
				Vector2 grav = new Vector2(force * diff.x / dist, force * diff.y / dist);

					obj.GetComponent<Rigidbody2D>().AddForce(grav, ForceMode2D.Impulse);	
			}
		}
	}


}
