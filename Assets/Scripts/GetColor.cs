using UnityEngine;
using System.Collections;

public class GetColor : MonoBehaviour {

	public GameObject[] bodies;

	// Use this for initialization
	void Start () {

		//general
		GameObject settingsObj = GameObject.FindGameObjectWithTag ("Settings");
		Settings settings = settingsObj.GetComponent<Settings> ();

		//color
		if (settings.species == "angel")
			bodies[0].GetComponent<MeshRenderer> ().material.color = settings.color;

		//species
		foreach (GameObject body in bodies)
			body.SetActive (false);
		if (settings.species == "angel")
			bodies [0].SetActive (true);
		else if (settings.species == "minotaur")
			bodies [1].SetActive (true);
		else if (settings.species == "hellbeast")
			bodies [2].SetActive (true);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
