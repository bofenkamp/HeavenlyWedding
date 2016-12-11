using UnityEngine;
using System.Collections;

public class Hellbeast : MonoBehaviour {

	private GameObject player;
	public float speed;
	private Rigidbody rb;

	public bool isPursuing = true;

	public float minX;
	public float maxX;
	public float minZ;
	public float maxZ;

	public Vector2 dest;
	private Vector3 velo;
	private Vector3 rot;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isPursuing) {
			Vector3 pcPos = player.transform.position;
			Vector3 pos = this.gameObject.transform.position;
			Vector3 diff = pcPos - pos;
			float d = Mathf.Sqrt ((diff.x * diff.x) + (diff.z * diff.z));
			float xVelo = diff.x * (speed / d);
			float yVelo = rb.velocity.y;
			float zVelo = diff.z * (speed / d);
			rb.velocity = new Vector3 (xVelo, yVelo, zVelo);

			float theta = Mathf.Rad2Deg * Mathf.Atan (diff.z / diff.x);
			theta = 90.0f - theta;
			if (pcPos.x < pos.x)
				theta += 180.0f;
			transform.eulerAngles = new Vector3 (0, theta, 0);
		} else {
			velo = new Vector3 (velo.x, rb.velocity.y, velo.z);
			rb.velocity = velo;
			this.gameObject.transform.eulerAngles = rot;
		}

		if (this.gameObject.transform.position.y < -10.0f)
			Destroy (this.gameObject);
	
	}

	public void GoForth () {

		float xDest = Random.Range (minX, maxX);
		float zDest = Random.Range (minZ, maxZ);
		dest = new Vector2 (xDest, zDest);
		Vector3 pos = this.transform.position;
		float dx = xDest - pos.x;
		float dz = zDest - pos.z;
		float c = Mathf.Sqrt (dx * dx + dz * dz);
		float k = speed / c;
		velo = new Vector3 (dx * k, rb.velocity.y, dz * k);

		float theta = Mathf.Rad2Deg * Mathf.Atan (dz / dx);
		theta = 90.0f - theta;
		if (xDest < pos.x)
			theta += 180.0f;
		rot = new Vector3 (0, theta, 0);

		Invoke ("Stop", 1.0f / k);

	}

	public void Stop () {

		velo = new Vector3 (0.0f, rb.velocity.y, 0.0f);
		Invoke ("GoForth", 1.0f);

	}
}
