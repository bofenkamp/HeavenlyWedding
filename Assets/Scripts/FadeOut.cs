using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour {

	public GameObject lightsObj;
	private Light lights;

	public float speed;
	public float deadTime;

	public GameObject darkness;

	// Use this for initialization
	void Start () {

		lights = lightsObj.GetComponent<Light> ();
	
	}
	
	public void Fade () {

//		Invoke ("Dim", speed);

		darkness.GetComponent<Animator>().SetBool("Falling", true);
		Invoke ("GoToPurg", deadTime);

	}

	void Dim () {

		if (lights.intensity < 0.01f) {

			lights.intensity = 0.0f;
			Invoke ("GoToPurg", deadTime);

		} else {

			lights.intensity -= 0.01f;
			Invoke ("Dim", speed);

		}

	}

	void GoToPurg () {

		SceneManager.LoadScene ("purgatory");

	}
}
