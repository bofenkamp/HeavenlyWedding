using UnityEngine;
using System.Collections;

public class MinotaurHitCheck : MonoBehaviour {

	public GameObject lights;
	public GameObject ceiling;

	private GameObject predator;

	public GameObject playerBody;
	public GameObject minoBody;
	public GameObject hellBody;

	public float eatTime;

	private FirstPersonController cont;

	private string predaType;

	void Start() {

		cont = this.gameObject.GetComponent<FirstPersonController> ();

	}

	void OnTriggerStay (Collider other) {

		if (other.gameObject.tag == "Minotaur" || other.gameObject.tag == "Hellbeast") {
			predator = other.gameObject;
			Eat ();
			predaType = other.tag;
			BodySwap ();
		}

	}

	void Eat () {

		lights.SetActive (false);
		ceiling.SetActive (true);
		cont.m_WalkSpeed = 0.0f;
		cont.m_RunSpeed = 0.0f;

		string name = GameObject.FindGameObjectWithTag ("Settings").GetComponent<Settings> ().pcName;
		string old = "Purgatory";
		string curr = "PurgatoryMino";

		GameObject.FindGameObjectWithTag ("Settings").GetComponent<Transfer> ().TransferRealms (name, old, curr);

		Invoke ("Awaken", eatTime);

	}

	void BodySwap () {

		Destroy (predator);
		playerBody.SetActive (false);
		Settings settings = GameObject.FindGameObjectWithTag ("Settings").GetComponent<Settings> ();
		if (predaType == "Minotaur") {
			minoBody.SetActive (true);
			settings.species = "minotaur";
		}
		else if (predaType == "Hellbeast") {
			hellBody.SetActive (true);
			GameObject control = GameObject.FindGameObjectWithTag ("GameController");
			control.GetComponent<HellControl> ().End ();
			settings.species = "hellbeast";
		}
	}

	void Awaken () {

		lights.SetActive (true);
		ceiling.SetActive (false);

		if (predaType == "Minotaur") {
			cont.m_WalkSpeed = 5.0f;
			cont.m_RunSpeed = 10.0f;
		} else if (predaType == "Hellbeast") {
			cont.m_WalkSpeed = 4.0f;
			cont.m_RunSpeed = 8.0f;
		}

	}

}
