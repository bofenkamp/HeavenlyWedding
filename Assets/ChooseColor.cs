using UnityEngine;
using System.Collections;

public class ChooseColor : MonoBehaviour {

	public Color color;

	// Use this for initialization
	void Start () {

		this.GetComponent<MeshRenderer> ().material.color = color;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
