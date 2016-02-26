using UnityEngine;
using System.Collections;

public class ShipCamera : MonoBehaviour {

	float orthographicSizeMin = 5f;
	float orthographicSizeMax = 15f;

	private float targetSize;

	// Use this for initialization
	void Start () {
		targetSize = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			targetSize += 0.5f;
			if (targetSize > orthographicSizeMax) targetSize = orthographicSizeMax;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) 
		{
			targetSize -= 0.5f;
			if (targetSize < orthographicSizeMin) targetSize = orthographicSizeMin;
		}

		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthographicSizeMin, orthographicSizeMax);
		ParallaxStars stars = (ParallaxStars)GameObject.FindObjectOfType(typeof(ParallaxStars));
		//stars.updateStars();
		Camera.main.orthographicSize += (targetSize - Camera.main.orthographicSize) * 0.1f;
	}
}
