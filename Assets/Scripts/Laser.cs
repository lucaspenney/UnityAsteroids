using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public GameObject parent;

	private LineRenderer line;
	private bool turnedOn = true;
	public float length = 30;

	// Use this for initialization
	void Start() {
		line = this.gameObject.GetComponent<LineRenderer>();
		line.useWorldSpace = true;
		line.SetWidth(0.2f, 0.2f);
	}
	
	// Update is called once per frame
	void Update() {
		if (turnedOn) {
			line.enabled = true;
			Vector2 dir = (Vector2)parent.transform.up;
			print(LayerMask.NameToLayer("Damageable"));
			RaycastHit2D hit = Physics2D.Raycast((Vector2)parent.transform.position, dir, length, 1 << LayerMask.NameToLayer("Damageable"));
			Vector2 point = (Vector2)parent.transform.position + (dir * length);
			if (hit.collider != null) {
				IDamageable d = hit.collider.gameObject.GetComponent<IDamageable>();
				if (d != null) {
					d.takeDamage(500);
					if (Random.value > 0.1) {
						Explosion.createExplosion("explosion", (Vector3)hit.point);
					}
				}
				point = hit.point;
			}
			Vector2 dist = ((Vector2)parent.transform.position - point);
			print(dist);
			int segs = (int)dist.magnitude + 1;
			line.SetVertexCount(segs);
			for (int i = 0;i <= segs;i++) {
				Vector2 pos = (Vector2)parent.transform.position + (dir) * i;
				line.SetPosition(i, pos);
			}
		} else {
			line.enabled = false;
		}
	}
}
