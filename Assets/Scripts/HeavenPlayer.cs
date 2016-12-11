using UnityEngine;
using System.Collections;

public class HeavenPlayer : MonoBehaviour {

	private Rigidbody rb;

	public GameObject cam;

	private float xVelo;

	public float walkSpeed;
	public float runSpeed;
	public float jumpForce;

	bool isMoving;
	bool canLand;
	private bool isRunning;

	private float groundHeight;
	private float playerWidth;

	private Vector3 diff;

	public float gameOverHeight;

	private GameObject prevPlatform;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody> ();
		canLand = true;

		groundHeight = gameObject.GetComponent<Collider>().bounds.size.y / 2 + 0.02f;
		playerWidth = gameObject.GetComponent<Collider> ().bounds.size.x / 2 - 0.02f;
	
	}
	
	// Update is called once per frame
	void Update () {

//		isMoving = Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)
//		|| Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)
//		|| Input.GetKeyDown (KeyCode.Space);

		//lookit
		if (!(Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)
			|| Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)))
			this.transform.eulerAngles = new Vector3 (180f, -90f, 90f);

		if (transform.parent != null)
			CheckForPlatform (transform.parent.gameObject);

		//are we running?
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
			isRunning = true;
		else
			isRunning = false;

		//where do we go now?
		if (transform.parent == null) {
			
			xVelo = 0.0f;
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
				this.transform.eulerAngles = new Vector3 (180f, 0f, 90f);
				if (isRunning)
					xVelo -= runSpeed;
				else
					xVelo -= walkSpeed;
			}
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
				this.transform.eulerAngles = new Vector3 (0f, 0f, -90f);
				if (isRunning)
					xVelo += runSpeed;
				else
					xVelo += walkSpeed;
			}
			rb.velocity = new Vector3 (xVelo, rb.velocity.y, 0.0f);

		} else {

			this.transform.position = transform.parent.position + diff;

			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
				this.transform.eulerAngles = new Vector3 (0f, 0f, 90f);
				if (isRunning)
					diff.x -= runSpeed / 100.0f;
				else
					diff.x -= walkSpeed / 100.0f;
			}
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
				this.transform.eulerAngles = new Vector3 (0f, 0f, -90f);
				if (isRunning)
					diff.x += runSpeed / 100.0f;
				else
					diff.x += walkSpeed / 100.0f;
			}
		}

		//jumping
		if (Input.GetKeyDown (KeyCode.Space) && OnPlatform()) {
			rb.AddForce (Vector3.up * jumpForce);
			Detach ();
		}

		//did we lose?
		if (this.transform.position.y < gameOverHeight)
			this.GetComponent<FadeOut> ().Fade ();
	
	}

	bool OnPlatform () {

		Vector3 leftUp = this.transform.position - Vector3.right * playerWidth;
		Vector3 leftDown = this.transform.position - Vector3.up * groundHeight - Vector3.right * playerWidth;

		if (Physics.Linecast (leftUp, leftDown))
			return true;

		Vector3 centUp = this.transform.position;
		Vector3 centDown = this.transform.position - Vector3.up * groundHeight;

		if (Physics.Linecast (centUp, centDown))
			return true;

		Vector3 rightUp = this.transform.position + Vector3.right * playerWidth;
		Vector3 rightDown = this.transform.position - Vector3.up * groundHeight + Vector3.right * playerWidth;

		if (Physics.Linecast (rightUp, rightDown))
			return true;
		else
			return false;

	}

	void OnTriggerEnter (Collider coll) {
		
		if (coll.transform.tag == "Cloud" && canLand && OnPlatform()) {

			if (coll.transform != transform.parent || rb.velocity.y < 0.0f) {

				diff = this.transform.position - coll.transform.position;
				transform.parent = coll.transform;
				if (prevPlatform != coll.gameObject)
					cam.GetComponent<TrackPlayer> ().Position (transform.parent.gameObject);
				prevPlatform = coll.gameObject;

			}

		} else if (coll.transform.tag == "FixedCloud") {
			cam.GetComponent<TrackPlayer> ().PositionFixed (coll.gameObject);
			prevPlatform = coll.gameObject;
		}

	}

	void CheckForPlatform (GameObject platform) {

		Vector3 platformPos = platform.transform.position;
		Vector3 pcPos = this.transform.position;
		float platformWidth = platform.GetComponent<Collider> ().bounds.size.x / 2;
		float playerWidth = this.GetComponent<Collider> ().bounds.size.x / 2;
		float xDist = Mathf.Abs (platformPos.x - pcPos.x);
		float maxDist = platformWidth + playerWidth;
		if (xDist > maxDist)
			Detach();
		if (platformPos.y > pcPos.y)
			Detach ();

	}

	void Detach () {

		transform.parent = null;
		canLand = false;
		Invoke ("ActivateLanding", 0.2f);

	}

	void ActivateLanding () {

		canLand = true;

	}

}
