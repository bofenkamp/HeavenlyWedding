using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGen : MonoBehaviour {

	public int rows;
	public int cols;

	private List<Vector2> walls;
	[HideInInspector] public List<Vector2> spaces;

	private Vector2 entrance;

	public bool minimizeDeadEnds;
	public bool increaseBranching;

	//forBuildOutsideEdge
	private List<Vector2> outside;

	//for BuildMaze
	private List<Vector2> failAdd;
	private List<Vector2> added;

	//for Construct
	public GameObject WallSeg;
	public float WallLen;
	public float WallWidth;
	public float WallHeight;
	public GameObject collectible;
	public float collectY;
	private Vector2 hole;

	//for ConstructFloor
	public GameObject floor;
	public int maxSize;

	//for Flood Fill
	private List<Vector2> unfilled;
	private List<Vector2> filled;

	//for Minotaur
	public GameObject minotaur;

	// Use this for initialization
	void Start () {

		walls = new List<Vector2> ();
		spaces = new List<Vector2> ();

		rows = rows + 2;
		cols = cols + 2;

		entrance = new Vector2 (1.0f, 0.0f);

		BuildOutsideEdge ();

		BuildMaze ();

		if (minimizeDeadEnds)
			RemoveDeadEnds ();

		Construct ();

		ConstructFloor ();

		PlaceMinotaur ();
	
	}

	void BuildOutsideEdge() {

		for (int x = 0; x < cols; x++) {
			for (int y = 0; y < rows; y++) {
				Vector2 spot = new Vector2 (x, y);
				if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1)
					walls.Add (spot);
				else
					spaces.Add (spot);
			}
		}
			
		Transfer (entrance, walls, spaces);

		outside = new List<Vector2> ();

		foreach (Vector2 wall in walls)
			outside.Add (wall);

	}

	void BuildMaze () {

		List<Vector2> toAdd = new List<Vector2> ();
		failAdd = new List<Vector2> ();
		added = new List<Vector2> ();

		foreach (Vector2 space in spaces)
			toAdd.Add (space);

		toAdd.Remove (entrance);

		while (toAdd.Count > 0) {

			int l = toAdd.Count;
			int i = Random.Range (0, l);
			Vector2 newWall = toAdd [i];
			toAdd.Remove (newWall);

			if (increaseBranching && IsDeadEnd(newWall))
				failAdd.Add (newWall);

			else {

				Transfer (newWall, spaces, walls);

				if (CanFloodFill ())
					added.Add (newWall);
				else {
					failAdd.Add (newWall);
					Transfer (newWall, walls, spaces);

				}
			}

		}

	}

	void RemoveDeadEnds () {

		List<Vector2> deadEnds = new List<Vector2> ();

		foreach (Vector2 space in spaces) {
			if (IsDeadEnd (space))
				deadEnds.Add (space);
		}

		deadEnds.Remove (entrance);

		while (deadEnds.Count > 0) {

			int i = Random.Range (0, deadEnds.Count);

			Vector2 testSpace = deadEnds [i];

			if (IsDeadEnd(deadEnds[i]))
				RemoveDeadEnd (testSpace);

			deadEnds.Remove (deadEnds [i]);

		}

	}

	void Construct () {

		int i = Random.Range (0, spaces.Count);
		hole = spaces [i];

		float wallY = WallHeight / 2.0f;

		foreach (Vector2 wallPos in walls) {

			float wallX = wallPos [0] * WallLen;
			float wallZ = wallPos [1] * WallWidth;
			Vector3 wallSegPos = new Vector3 (wallX, wallY, wallZ);
			Instantiate (WallSeg, wallSegPos, Quaternion.identity);

		}

		foreach (Vector2 collectPos in spaces) {

			if (collectPos != hole) {
				float collectX = collectPos [0] * WallLen;
				float collectZ = collectPos [1] * WallWidth;
				Vector3 collectiblePos = new Vector3 (collectX, collectY, collectZ);
				Instantiate (collectible, collectiblePos, Quaternion.identity);
			}

		}

		this.GetComponent<CollectibleTracker> ().total = spaces.Count - 1;

	}

	void ConstructFloor () {

		for (int row = 0; row < maxSize + 2; row++) {
			for (int col = 0; col < maxSize + 2; col++) {
				Vector2 pos = new Vector2 (row, col);
				if (pos != hole) {
					Vector3 floorPos = new Vector3 (row * WallLen, -0.5f, col * WallWidth);
					Instantiate (floor, floorPos, Quaternion.identity);
				}
			}
		}

	}

	void PlaceMinotaur () {

		List<Vector2> minotaurSpots = new List<Vector2> ();

		int minDist = (rows + cols) / 2;

		foreach (Vector2 space in spaces) {

			if (space [0] + space [1] > minDist)
				minotaurSpots.Add (space);

		}

		int i = Random.Range (0, minotaurSpots.Count);
		Vector2 minoPlace = minotaurSpots [i];
		Vector3 minotaurSpot = new Vector3 (minoPlace [0] * WallLen, 2.0f, minoPlace [1] * WallWidth);

		minotaur.transform.position = minotaurSpot;

		MinotaurControl minCon = minotaur.GetComponent<MinotaurControl> ();

		minCon.spot = minoPlace;
		minCon.spaces = spaces;
			
	}

	void Transfer (Vector2 position, List<Vector2> current, List<Vector2> moveTo) {

		current.Remove (position);
		moveTo.Add (position);

	}

	bool CanFloodFill () {

		unfilled = new List<Vector2> ();
		filled = new List<Vector2> ();

		foreach (Vector2 space in spaces)
			unfilled.Add (space);

		FloodFill (entrance);

		if (unfilled.Count == 0)
			return true;
		else
			return false;

	}

	void FloodFill (Vector2 start) {

		Transfer (start, unfilled, filled);

		float x = start [0];
		float y = start [1];

		Vector2 left = new Vector2 (x - 1.0f, y);
		Vector2 right = new Vector2 (x + 1.0f, y);
		Vector2 up = new Vector2 (x, y + 1.0f);
		Vector2 down = new Vector2 (x, y - 1.0f);

		List<Vector2> neighbors = new List<Vector2>();
		neighbors.Add(left);
		neighbors.Add(right);
		neighbors.Add(up);
		neighbors.Add(down);

		foreach(Vector2 neighbor in neighbors) {
			if (unfilled.Contains(neighbor))
				FloodFill(neighbor);
		}
	}

	bool IsDeadEnd(Vector2 space) {

		float x = space [0];
		float y = space [1];

		int count = 0;

		Vector2 left = new Vector2 (x - 1.0f, y);
		Vector2 right = new Vector2 (x + 1.0f, y);
		Vector2 up = new Vector2 (x, y + 1.0f);
		Vector2 down = new Vector2 (x, y - 1.0f);

		List<Vector2> neighbors = new List<Vector2>();
		neighbors.Add(left);
		neighbors.Add(right);
		neighbors.Add(up);
		neighbors.Add(down);

		foreach (Vector2 neighbor in neighbors) {
			if (spaces.Contains (neighbor))
				count++;
		}

		if (count <= 1)
			return true;
		else
			return false;

	}

	void RemoveDeadEnd(Vector2 deadEnd) {

		float x = deadEnd [0];
		float y = deadEnd [1];

		Vector2 left = new Vector2 (x - 1.0f, y);
		Vector2 right = new Vector2 (x + 1.0f, y);
		Vector2 up = new Vector2 (x, y + 1.0f);
		Vector2 down = new Vector2 (x, y - 1.0f);

		List<Vector2> neighbors = new List<Vector2>();
		neighbors.Add(left);
		neighbors.Add(right);
		neighbors.Add(up);
		neighbors.Add(down);

		List<Vector2> toRemove = new List<Vector2> ();

		foreach (Vector2 wall in neighbors) {
			if (outside.Contains(wall))
				toRemove.Add (wall);
		}


		foreach (Vector2 wall in toRemove)
			neighbors.Remove (wall);

		while (neighbors.Count > 0) {

			int i = Random.Range (0, neighbors.Count);
			Vector2 neighbor = neighbors [i];

			if (IsDeadEnd (neighbor) || spaces.Contains(neighbor))
				neighbors.Remove (neighbor);
			else {
				Transfer (neighbor, walls, spaces);
				Transfer (neighbor, added, failAdd);
				neighbors = new List<Vector2> ();
			}

		}

	}

}
