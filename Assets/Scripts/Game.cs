using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public int waves;
	public int currentWave = -1;

	public float minSpawnRange;
	public float maxSpawnRange;

	private float waveStartTime = 0;
	public float waveTime = 0;

	private float endTime = -1;

	public GameObject parent;

	// Use this for initialization
	void Start () {
		waveStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		Enemy[] enemies = (Enemy[])GameObject.FindObjectsOfType (typeof(Enemy));
		if (enemies.Length == 0) {
			currentWave++;
			UnityEngine.UI.Text text = GameObject.Find("WaveText").GetComponent<UnityEngine.UI.Text>();
			text.text = "Wave " + currentWave;
			waveStartTime = Time.time;
			for (int i=0;i<currentWave * 10 ;i++) {
				Vector2 pos = Random.insideUnitCircle;
				pos.x *= maxSpawnRange;
				pos.y *= maxSpawnRange;
				pos.x += parent.transform.position.x;
				pos.y += parent.transform.position.y;
				Vector2 diff = (Vector2)transform.position - pos;
				if (diff.magnitude < minSpawnRange) {
					i--;
					continue;
				}

				Enemy prefab = (Enemy)Resources.Load ("Prefabs/Enemy", typeof(Enemy));
				Enemy a = (Enemy)Instantiate(prefab, pos, Quaternion.identity);
			}
		}

		if (Time.time - waveStartTime > 3) {
			UnityEngine.UI.Text text = GameObject.Find("WaveText").GetComponent<UnityEngine.UI.Text>();
			text.text = "";
		}

		ControllableShip[] players = (ControllableShip[])GameObject.FindObjectsOfType (typeof(ControllableShip));
		if (players.Length == 0 && endTime == -1) {
			endTime = Time.time;
		}
		if (endTime != -1 && Time.time - endTime > 3) {
			SceneManager.LoadScene("scene");
		}
	}

	void spawnEnemy() {

	}
}
