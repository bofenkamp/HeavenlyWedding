using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {

	private Rigidbody rb;

	public GameObject player;
	private float originHeight;
	public float finalHeight;
	public float speed;

	private float length;
	private float width;

	private bool moving;

	// Use this for initialization
	void Start () {

		moving = false;
		rb = this.GetComponent<Rigidbody> ();

		length = this.GetComponent<BoxCollider> ().bounds.size.x - 0.1f;
		width = this.GetComponent<BoxCollider> ().bounds.size.z - 0.1f;

		originHeight = this.transform.position.y;

	}

	// Update is called once per frame
	void Update () {

		Vector3 pos = this.transform.position;
		Vector3 pcPos = player.transform.position;
		Vector3 diff = pcPos - pos;

		if (Mathf.Abs(diff.x) <= length / 2f && (Mathf.Abs(diff.z)) <= width / 2f)
			moving = true;
		else
			moving = false;

		Debug.Log (moving);

		if (moving) {
			player.GetComponent<Rigidbody> ().velocity = Vector3.up * speed;
			rb.velocity = Vector3.up * speed;
			if (this.transform.position.y >= finalHeight)
				moving = false;
		} else {
			if (this.transform.position.y <= originHeight)
				rb.velocity = Vector3.zero;
			else
				rb.velocity = Vector3.down * speed;
		}

		if (this.transform.position.y > finalHeight)
			SceneManager.LoadScene ("WeddingPurg");
	}

}
