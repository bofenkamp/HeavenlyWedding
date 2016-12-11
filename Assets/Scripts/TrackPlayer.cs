using UnityEngine;
using System.Collections;

public class TrackPlayer : MonoBehaviour {

	private Rigidbody rb;

	public GameObject player;
	public float distance;
	public float speed;
	public float lowSpeed;
	private Vector3 velo;
	public float maxDist;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody> ();
		velo = Vector3.zero;
	
	}
	
	// Update is called once per frame
	void Update () {

//		Vector3 pos = player.transform.position;
//		pos.z -= distance;
//		this.transform.position = pos;

		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		Vector2 pcPos = new Vector2 (player.transform.position.x, player.transform.position.y);
		Vector2 diff = pcPos - pos;
		float d = Vector2.Distance (pos, pcPos);

		if (player.transform.parent == null) {
			
			float k = lowSpeed / d;
			velo = new Vector3 (diff.x * k, diff.y * k, 0.0f);

		}

		if (d > maxDist && player.transform.parent != null)
			Position (player.transform.parent.gameObject);

		rb.velocity = velo;

	}

	public void Position (GameObject platform) {

		GameObject[] points = platform.GetComponent<CloudControl> ().dests;

		float left = points [0].transform.position.x;
		float right = points [0].transform.position.x;
		float top = points [0].transform.position.y;
		float bottom = points [0].transform.position.y;

		foreach (GameObject point in points) {

			Vector3 pos = point.transform.position;
			if (pos.x < left)
				left = pos.x;
			if (pos.x > right)
				right = pos.x;
			if (pos.y > top)
				top = pos.y;
			if (pos.y < bottom)
				bottom = pos.y;

		}

		float x = (left + right) / 2.0f;
		float y = (top + bottom) / 2.0f + player.GetComponent<Collider> ().bounds.size.y / 2.0f;
		float z = -distance;

		Vector3 start = this.transform.position;
		Vector3 end = new Vector3(x, y, z);
		float dX = end [0] - start[0];
		float dY = end [1] - start[1];
		float d = Mathf.Sqrt (dX * dX + dY * dY);
		float xSpeed = speed * (dX / d);
		float ySpeed = speed * (dY / d);
		velo = new Vector3 (xSpeed, ySpeed, 0.0f);
		Invoke ("Land", d / speed);

	}

	public void PositionFixed (GameObject platform) {

		float x = platform.transform.position.x;
		float y = platform.transform.position.y + player.GetComponent<Collider> ().bounds.size.y / 2.0f;
		float z = -distance;

		Vector3 start = this.transform.position;
		Vector3 end = new Vector3(x, y, z);
		float dX = end [0] - start[0];
		float dY = end [1] - start[1];
		float d = Mathf.Sqrt (dX * dX + dY * dY);
		float xSpeed = speed * (dX / d);
		float ySpeed = speed * (dY / d);
		velo = new Vector3 (xSpeed, ySpeed, 0.0f);
		Invoke ("Land", d / speed);

	}

	void Land() {

		velo = Vector3.zero;

	}

}
