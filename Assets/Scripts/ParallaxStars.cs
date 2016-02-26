using UnityEngine;
using System.Collections;

public class ParallaxStars : MonoBehaviour {

	Star[] stars1 = new Star[100];
	Star[] stars2 = new Star[200];
	Star[] stars3 = new Star[300];

	public GameObject parent;
	private float width;
	private float height;

	private Vector2 lastParentPos;

	// Use this for initialization
	void Start () {
		Vector3 camDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
		width = camDimensions.x * 2f;
		height = camDimensions.y * 2f;
		print (width);
		lastParentPos = parent.transform.position;
		Star prefab = (Star)Resources.Load ("Prefabs/Star", typeof(Star));
		for (int i=0;i<100;i++) {
			Vector3 pos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 100);
			pos = Camera.main.ScreenToWorldPoint(pos);
			Star a = (Star)Instantiate(prefab, pos, Quaternion.identity);
			a.transform.localScale = new Vector3(Random.value * 0.8f,Random.value * 0.8f,0);
			stars1[i] = a;
		}
		for (int i=0;i<200;i++) {
			Vector3 pos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 100);
			pos = Camera.main.ScreenToWorldPoint(pos);
			Star a = (Star)Instantiate(prefab, pos, Quaternion.identity);
			a.transform.localScale = new Vector3(Random.value * 1.1f,Random.value * 1.1f,0);
			stars2[i] = a;
		}
		for (int i=0;i<300;i++) {
			Vector3 pos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 100);
			pos = Camera.main.ScreenToWorldPoint(pos);
			Star a = (Star)Instantiate(prefab, pos, Quaternion.identity);
			a.transform.localScale = new Vector3(Random.value * 1.4f,Random.value * 1.4f,0);
			stars3[i] = a;
		}
	}

	// updateStars is called on camera position change
	public void updateStars () {
		Vector2 currentPos = new Vector2(parent.transform.position.x, parent.transform.position.y);
		Vector2 diff = new Vector2(lastParentPos.x - currentPos.x, lastParentPos.y - currentPos.y);
		lastParentPos = currentPos;
		diff *= 0.9f;
		for (int i=0;i<stars1.Length;i++) {
			stars1[i].gameObject.transform.position = new Vector3(stars1[i].gameObject.transform.position.x - diff.x, stars1[i].gameObject.transform.position.y - diff.y, 100);
			repositionStar(stars1[i]);
		}
		diff *= 0.5f;
		for (int i=0;i<stars2.Length;i++) {
			stars2[i].gameObject.transform.position = new Vector3(stars2[i].gameObject.transform.position.x - diff.x, stars2[i].gameObject.transform.position.y - diff.y, 100);
			repositionStar(stars2[i]);
		}
		diff *= 0.3f;
		for (int i=0;i<stars3.Length;i++) {
			diff *= 0.9f;
			stars3[i].gameObject.transform.position = new Vector3(stars3[i].gameObject.transform.position.x - diff.x, stars3[i].gameObject.transform.position.y - diff.y, 100);
			repositionStar(stars3[i]);
		}
	}

	private void repositionStar(Star star) {
		Vector3 bottomLeft = new Vector3(parent.transform.position.x - (width / 2),  parent.transform.position.y - (height / 2), 100);

		Vector3 screenPos = new Vector3(star.gameObject.transform.position.x - bottomLeft.x, star.gameObject.transform.position.y - bottomLeft.y, 100);
		if (screenPos.x < 0) {
			star.gameObject.transform.position = new Vector3(star.gameObject.transform.position.x + width, star.gameObject.transform.position.y, star.gameObject.transform.position.z);
		}
		else if (screenPos.x > width) {
			star.gameObject.transform.position = new Vector3(star.gameObject.transform.position.x - width, star.gameObject.transform.position.y, star.gameObject.transform.position.z);
		}
		if (screenPos.y < 0) {
			star.gameObject.transform.position = new Vector3(star.gameObject.transform.position.x, star.gameObject.transform.position.y + height, star.gameObject.transform.position.z);
		}
		else if (screenPos.y > height) {
			star.gameObject.transform.position = new Vector3(star.gameObject.transform.position.x, star.gameObject.transform.position.y - height, star.gameObject.transform.position.z);
		}

	}
}
