using UnityEngine;
using System.Collections;

public class ShipCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int orthographicSizeMin = 1;
		int orthographicSizeMax = 12;
		
		
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			Camera.main.orthographicSize++;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) 
		{
			Camera.main.orthographicSize--;
		}
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthographicSizeMin, orthographicSizeMax);
		ParallaxStars stars = (ParallaxStars)GameObject.FindObjectOfType(typeof(ParallaxStars));
		stars.updateStars();
	}
}
