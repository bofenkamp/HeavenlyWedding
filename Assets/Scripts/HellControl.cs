using UnityEngine;
using System.Collections;

public class HellControl : MonoBehaviour {

	public float behindSpawnTime;
	public GameObject player;
	public GameObject cam;
	public GameObject hellBeast;
	public float spawnDist;
	public float y;

	public GameObject[] spawnPoints;
	public float spawnTime;
	public float difficultyTime;
	public float minSpawnTime;

	public float calmTime;

	[HideInInspector] public bool spawning = true;

	// Use this for initialization
	void Start () {

		Settings settings = GameObject.FindGameObjectWithTag ("Settings").GetComponent<Settings> ();
		string name = settings.pcName;
		string old = "PurgatoryMino";
		if (settings.species == "angel")
			old = "Purgatory";
		string curr = "Hell";

		GameObject.FindGameObjectWithTag ("Settings").GetComponent<Transfer> ().TransferRealms (name, old, curr);

		Invoke ("LetTheGamesBegin", calmTime);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LetTheGamesBegin() {

		Invoke ("BehindSpawn", behindSpawnTime);
		Invoke ("Spawn", spawnTime);
		Invoke ("Faster", difficultyTime);

	}

	void BehindSpawn () {

		if (spawning) {
			Vector3 camPos = cam.transform.position;
			Vector3 pcPos = player.transform.position;
			Vector3 diff = camPos - pcPos;
			Vector3 pos = pcPos + (spawnDist + 1.0f) * diff;
			pos = new Vector3 (pos.x, y, pos.z);

			Instantiate (hellBeast, pos, Quaternion.identity);

			Invoke ("BehindSpawn", behindSpawnTime);
		}

	}

	void Spawn () {

		if (spawning) {
			Vector3 spawnPt = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position;
			Instantiate (hellBeast, spawnPt, Quaternion.identity);

			Invoke ("Spawn", spawnTime);
		}

	}

	void Faster () {

		if (spawnTime > minSpawnTime && spawning) {
			spawnTime -= 0.01f;
			Invoke ("Faster", difficultyTime);
		}

	}

	public void End () {

		spawning = false;
		GameObject[] beasts = GameObject.FindGameObjectsWithTag ("Hellbeast");
		foreach (GameObject beast in beasts) {
			beast.tag = "Untagged";
			Hellbeast control = beast.GetComponent<Hellbeast> ();
			control.isPursuing = false;
			control.speed = control.speed / 2.0f;
			control.GoForth ();
		}

	}
}
