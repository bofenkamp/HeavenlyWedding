using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

	public Color color;
	public string pcName;
	public string species;
	public bool onlyInHeaven;

	// Use this for initialization
	void Start () {

		species = "angel";
		DontDestroyOnLoad (this.gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Escape))
			Application.Quit();
	
	}
}
