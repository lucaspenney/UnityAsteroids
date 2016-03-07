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
	private float waveKills = 0;
	private float waveSpawns = 0;
	private float lastSpawnTime = 0;
	private float waveTotalSpawns = 0;
	private float spawnRate = 1;
	private int score = 0;

	public GameObject parent;

	public static EventDispatcher eventManager;

	// Use this for initialization
	void Awake () {
		Game.eventManager = new EventDispatcher();
		waveStartTime = Time.time;

		eventManager.addListener("ENEMY_DESTROYED", onEnemyDestroyed);
	}

	public void onEnemyDestroyed(object e) {
		Enemy enemy = (Enemy)e;
		if (enemy) {
			waveKills++;
			score += 10;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (waveSpawns < waveTotalSpawns && Time.time - lastSpawnTime > spawnRate) {
			spawnEnemy();
			waveSpawns++;
			lastSpawnTime = Time.time;
		}

		if (waveKills == waveTotalSpawns) {
			currentWave++;
			UnityEngine.UI.Text text = GameObject.Find("WaveText").GetComponent<UnityEngine.UI.Text>();
			text.text = "Wave " + currentWave;
			waveStartTime = Time.time;
			waveTotalSpawns = currentWave * 10;
			waveSpawns = 0;
			waveKills = 0;
			spawnRate *= 0.9f;
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
		if (!parent) return;
		Vector2 pos = Random.insideUnitCircle;
		pos.x *= maxSpawnRange;
		pos.y *= maxSpawnRange;
		pos.x += parent.transform.position.x;
		pos.y += parent.transform.position.y;
		Vector2 diff = (Vector2)transform.position - pos;
		if (diff.magnitude < minSpawnRange) {
			spawnEnemy();
			return;
		}

		Enemy prefab = (Enemy)Resources.Load ("Prefabs/Enemy", typeof(Enemy));
		Enemy a = (Enemy)Instantiate(prefab, pos, Quaternion.identity);
	}
}
