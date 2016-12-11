using UnityEngine;
using System.Collections;

public class Lookit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!((Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)
			|| Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)))) {
			this.transform.eulerAngles = new Vector3 (this.transform.eulerAngles.x, 180f, this.transform.eulerAngles.z);
			Debug.Log (Time.time);
		}
		else
			this.transform.eulerAngles = new Vector3 (this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
	
	}
}
