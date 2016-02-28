using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public int waves;
	public int currentWave = 0;

	public float minSpawnRange;
	public float maxSpawnRange;

	private float waveStartTime = 0;
	public float waveTime = 0;

	// Use this for initialization
	void Start () {
		waveStartTime = 0;
		startNextWave();
	}

	void startNextWave() {
		if (currentWave < waves) {
			currentWave++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Enemy[] enemies = (Enemy[])GameObject.FindObjectsOfType (typeof(Enemy));
		GameObject parent = this.gameObject;
		if (enemies.Length == 0) {
			currentWave++;
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
				print(pos);
				Enemy prefab = (Enemy)Resources.Load ("Prefabs/Enemy", typeof(Enemy));
				Enemy a = (Enemy)Instantiate(prefab, pos, Quaternion.identity);
			}
		}
	}

	void spawnEnemy() {

	}
}
