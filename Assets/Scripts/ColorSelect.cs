using UnityEngine;
using System.Collections;

public class ColorSelect : MonoBehaviour {

	public Color randColor;

	private GameObject settingsObj;

	// Use this for initialization
	void Start () {

		randColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
		this.GetComponent<MeshRenderer> ().material.color = randColor;

//		if (this.tag == "Collictible") {
//			foreach (Transform child in this.transform)
//				child.GetComponent<MeshRenderer> ().material.color = randColor;
//		} else
//			this.GetComponent<MeshRenderer> ().material.color = randColor;

		if (this.gameObject.tag == "Player") {
			settingsObj = GameObject.FindGameObjectWithTag ("Settings");
			settingsObj.GetComponent<Settings> ().color = randColor;
			Invoke ("SaveColor", 0.01f);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SaveColor () {

		Settings settings = settingsObj.GetComponent<Settings> ();
		Color color = settings.color;
		string name = settings.pcName.ToLower();

		PlayerPrefs.SetFloat (name + "R", color.r);
		PlayerPrefs.SetFloat (name + "G", color.g);
		PlayerPrefs.SetFloat (name + "B", color.b);

	}
}
