using UnityEngine;
using System.Collections;

public class OrbitRenderer : MonoBehaviour {

	private LineRenderer line;


	// Use this for initialization
	void Start () {
		line = gameObject.AddComponent<LineRenderer>();
		line.SetColors (Color.white, Color.black);

		line.useWorldSpace = true;
		line.SetWidth(0.2f,0.01f);
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 pos = this.GetComponent<Rigidbody2D>().transform.position;
		Vector2 vel = this.GetComponent<Rigidbody2D>().velocity;
		Vector2 vf = new Vector2(0,0);

		int verts = 500;
		line.SetVertexCount(verts);
		for (int i=0;i<verts;i++) {
			Vector3 linePos = new Vector3(pos.x, pos.y, 0);
			line.SetPosition(i, linePos);
			float t = Time.fixedDeltaTime;
			float gravConstant = 0.01f;

			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, 1200);
			for (int k=0;k < hitColliders.Length;k++) {
				GameObject obj = hitColliders[k].gameObject;
				if (obj == this.gameObject) continue;
				Vector2 diff = new Vector2(obj.transform.position.x - pos.x, obj.transform.position.y - pos.y);
				float distSquared = (diff.x * diff.x + diff.y * diff.y);
				float dist = Mathf.Sqrt(distSquared);
				
				if (obj.GetComponent<Rigidbody2D>() != null) {
					float force = obj.GetComponent<Rigidbody2D>().mass / distSquared;
					force *= gravConstant;
					vf = new Vector2((force * diff.x / dist), (force * diff.y / dist));
				}
			}

			pos += vel * t + 0.5f * gravConstant * vf * t * t;
			vel += vf * t;
		}
	}
}
