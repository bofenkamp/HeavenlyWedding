using UnityEngine;
using System.Collections;

public class FinalCloud : MonoBehaviour {

	private Rigidbody rb;

	public float finalHeight;
	public float speed;

	private bool moving;
	private bool neverMoved;

	// Use this for initialization
	void Start () {

		moving = false;
		neverMoved = true;
		rb = this.GetComponent<Rigidbody> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (moving) {
			rb.velocity = Vector3.up * speed;
			if (this.transform.position.y >= finalHeight)
				moving = false;
		} else
			rb.velocity = Vector3.zero;
	
	}

	void OnTriggerEnter (Collider other) {

		if (neverMoved) {
			rb.velocity = Vector3.up * speed;
			moving = true;
			neverMoved = false;
		}

	}
}
