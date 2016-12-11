using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CollectibleTracker : MonoBehaviour {
	
	[HideInInspector] public int total;

	public Text countText;

	private int count;

	public GameObject elevator;

	// Use this for initialization
	void Start () {

		Invoke ("FindTotal", 0.01f);
		Invoke ("SetCountText", 0.02f);
	
	}

	void FindTotal () {

		GameObject[] all = GameObject.FindGameObjectsWithTag ("Collectible");
		total = all.Length;

	}

	void SetCountText () {

		count = FindCount ();
		countText.text = count.ToString() + "/" + total.ToString() + " gems collected.";
		if (count == total)
			elevator.SetActive (true);

	}

	public void Collect () {

		Invoke ("SetCountText", 0.01f);

	}

	int FindCount () {

		GameObject[] remaining = GameObject.FindGameObjectsWithTag ("Collectible");
		return total - remaining.Length;

	}
}
