using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToHell : MonoBehaviour {

	public GameObject player;
	public float fallDist;

	public bool fadeOut;

	public GameObject lightsObj;
	private Light lights;
	public GameObject darkness;
	public float dimSpeed;
	public float deadTime;

	private bool transitioning;

	// Use this for initialization
	void Start () {

		transitioning = false;
		lights = lightsObj.GetComponent<Light> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (player.transform.position.y < fallDist && !transitioning) {

			transitioning = true;

			if (!fadeOut) {
				
				SceneManager.LoadScene ("Hell");

			} else {
				
				Invoke ("Dim", dimSpeed);
				FirstPersonController pcCont = player.GetComponent<FirstPersonController> ();
				pcCont.m_WalkSpeed = 0.0f;
				pcCont.m_RunSpeed = 0.0f;
				darkness.GetComponent<Animator> ().SetBool ("Falling", true);

			}

		}
	
	}

	void Dim () {

		if (lights.intensity >= 0.01f) {
			
			lights.intensity -= 0.01f;
			Invoke ("Dim", dimSpeed);

		} else {

			lights.intensity = 0.0f;
			Invoke ("Move", deadTime);

		}

	}

	void Move () {

		SceneManager.LoadScene ("Hell");

	}

}
