using UnityEngine;
using System.Collections;

public class PlayerTracking : MonoBehaviour {

	public GameObject player;
	public float speed;
	public float distance;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 pos = transform.position;
		Vector2 pcPos = pos;

		if (player.transform.parent == null) {
			pcPos = player.transform.position;
		} else {
			pcPos = PlatformCenter (player);
		}


		Vector3 diff = pcPos - pos;
		float d = Vector2.Distance (pos, pcPos);
		float k = speed / d;
//		Vector3 pcVelo = player.GetComponent<Rigidbody> ().velocity;

//		if (d < speed / 20f && (pcVelo.x == 0f || pcVelo.y == 0f) && player.transform.parent == null) {
//			if (pcVelo.x == 0f && pcVelo.y == 0f)
//				transform.position = new Vector3 (pcPos.x, pcPos.y, -distance);
//			else if (pcVelo.x == 0f && pcVelo.y != 0f) {
//				transform.position = new Vector3 (pcPos.x, transform.position.y, -distance);
//				rb.velocity = new Vector3 (0f, diff.y * k, 0f);
//			} else if (pcVelo.x != 0f && pcVelo.y == 0f) {
//				transform.position = new Vector3 (transform.position.x, pcPos.y, -distance);
//				rb.velocity = new Vector3 (diff.x * k, 0f, 0f);
//			}
//		} else if (d < speed / 10f && player.transform.parent == null) {
//
//			transform.position = new Vector3 (pcPos.x, pcPos.y, -distance);
//
//		}
//		else
//			rb.velocity = new Vector3(diff.x * k, diff.y * k, 0.0f);

		if (d < speed / 20f)
			transform.position = new Vector3 (pcPos.x, pcPos.y, -distance);
		else
			rb.velocity = new Vector3(diff.x * k, diff.y * k, 0.0f);
	
	}

	Vector2 PlatformCenter(GameObject player) {

		GameObject platform = player.transform.parent.gameObject;

		if (platform.tag == "FixedCloud") {

			return platform.transform.position;

		} else if (platform.tag == "Cloud") {

			GameObject[] dests = platform.GetComponent<CloudControl> ().dests;

			float left = dests [0].transform.position.x;
			float right = dests [0].transform.position.x;
			float top = dests [0].transform.position.y;
			float bottom = dests [0].transform.position.y;

			foreach (GameObject dest in dests) {

				if (dest.transform.position.x < left)
					left = dest.transform.position.x;
				else if (dest.transform.position.x > right)
					right = dest.transform.position.x;

				if (dest.transform.position.y < bottom)
					bottom = dest.transform.position.y;
				else if (dest.transform.position.y > top)
					top = dest.transform.position.y;

			}

			float x = (left + right) / 2.0f;
			float y = (top + bottom) / 2.0f;

			return new Vector2 (x, y);

		}
			
		return player.transform.position;

	}
}
