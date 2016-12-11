using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinotaurControl : MonoBehaviour {

	//imported from maze gen
	public Vector2 spot;
	[HideInInspector] public List<Vector2> spaces;

	//for movement
	public float walkSpeed;
	public float runSpeed;
	private Animator anim;
	private int direction = 0;
	private Vector2 moveDest;
	private float speed;
	public float rotTime;

	// Use this for initialization
	void Start () {

		anim = this.GetComponent<Animator> ();

		Invoke("FindDirection", 0.01f);
	
	}

	void FindDirection () {

		//find all neighboring spaces to which the minotaur may move
		float x = spot [0];
		float y = spot [1];

		this.transform.position = new Vector3 (5.0f * x, 2.0f, 5.0f * y);

		Vector2 west = new Vector2 (x - 1, y);
		Vector2 north = new Vector2 (x, y + 1);
		Vector2 east = new Vector2 (x + 1, y);
		Vector2 south = new Vector2 (x, y - 1);
		List<Vector2> neighbors = new List<Vector2> ();
		neighbors.Add (west);
		neighbors.Add (east);
		neighbors.Add (north);
		neighbors.Add (south);

		//remove the space behind the minotaur to reduce backtracking
		if (direction == 0)
			neighbors.Remove (south);
		else if (direction == 1)
			neighbors.Remove (east);
		else if (direction == 2)
			neighbors.Remove (north);
		else if (direction == 3)
			neighbors.Remove (west);

		//find out which neighboring spaces are walls 
		//so the minotaur won't run into them
		List<Vector2> neighborWalls = new List<Vector2> ();

		foreach (Vector2 neighbor in neighbors) {

			if (!spaces.Contains (neighbor))
				neighborWalls.Add (neighbor);

		}

		foreach (Vector2 wall in neighborWalls)
			neighbors.Remove (wall);

		//triggers when minotaur hits dead end
		if (neighbors.Count == 0) {

			Vector2 destination = Vector2.zero;
			if (direction == 0)
				destination = south;
			else if (direction == 1)
				destination = east;
			else if (direction == 2)
				destination = north;
			else if (direction == 3)
				destination = west;
			TurnAround ();

			moveDest = destination;
			speed = walkSpeed;

			Invoke("Move", rotTime);

		} else {

			//triggers when minotaur is not in dead end
			int i = Random.Range (0, neighbors.Count);
			Vector2 destination = neighbors [i];

			moveDest = destination;
			speed = walkSpeed;

			if (destination == north) {
				if (direction == 3)
					TurnLeft ();
				else if (direction == 1)
					TurnRight ();
				else
					Move ();
			} else if (destination == west) {
				if (direction == 0)
					TurnLeft ();
				else if (direction == 2)
					TurnRight ();
				else
					Move ();
			} else if (destination == east) {
				if (direction == 0)
					TurnRight ();
				else if (direction == 2)
					TurnLeft ();
				else
					Move ();
			} else if (destination == south) {
				if (direction == 1)
					TurnLeft ();
				else if (direction == 3)
					TurnRight ();
				else
					Move ();
			}

		}

	}

	void Move () {

		//finds how far and in what direction to move, stopping once there.
		float dX = moveDest [0] - spot[0];
		float dZ = moveDest [1] - spot[1];
		float d = Mathf.Sqrt (dX * dX + dZ * dZ);
		float xSpeed = speed * (dX / d);
		float zSpeed = speed * (dZ / d);
		this.GetComponent<Rigidbody>().velocity = new Vector3 (xSpeed, 0.0f, zSpeed);
		spot = moveDest;
		Invoke ("Land", 5.0f / speed);

	}

	void Land () {

		this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		Invoke ("FindDirection", 0.0f);

	}

	void TurnLeft() {

		direction++;
		NormalizeDirection();
		anim.SetInteger ("Direction", direction);
		Invoke("Move", rotTime);

	}

	void TurnAround() {

		direction = direction + 2;
		NormalizeDirection();
		anim.SetInteger ("Direction", direction);

	}

	void TurnRight() {

		direction = direction - 1;
		NormalizeDirection();
		anim.SetInteger ("Direction", direction);
		Invoke("Move", rotTime);

	}

	//keeps the direction number in the range
	//that the program understands
	void NormalizeDirection() {

		if (direction < 0) {

			while (0 > direction)
				direction = direction + 4;

		} else if (direction > 3) {

			while (direction > 3)
				direction = direction - 4;

		}

	}
}
