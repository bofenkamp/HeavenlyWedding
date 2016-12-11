using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	private GameObject controller;

	// Use this for initialization
	void Start () {

		controller = GameObject.FindGameObjectWithTag ("GameController");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {

		if (other.tag == "Player") {
			controller.GetComponent<CollectibleTracker> ().Collect ();
			Destroy (this.gameObject);
		}

	}
}
