using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {
	private Light lights;
	public float intensity;
	public float speed;

	void Start () {

		lights = this.GetComponent<Light> ();
		lights.intensity = 0;
		Invoke ("Brighten", speed);
	
	}

	void Brighten () {

		lights.intensity += 0.01f;

		if (lights.intensity < intensity)
			Invoke ("Brighten", speed);

	}
}
