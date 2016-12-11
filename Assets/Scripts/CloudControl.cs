using UnityEngine;
using System.Collections;

public class CloudControl : MonoBehaviour {

	private Rigidbody rb;

	public GameObject[] dests;
	private int i;

	public float speed;
	private Vector3 velo;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody> ();
		i = 0;

		JumpToLocation ();
	
	}
	
	// Update is called once per frame
	void Update () {

		rb.velocity = velo;
	
	}

	void JumpToLocation () {

		Vector3 start = this.transform.position;
		Vector3 end = dests[i].transform.position;
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
		i++;
		if (i >= dests.Length)
			i = 0;
		JumpToLocation ();

	}

}
