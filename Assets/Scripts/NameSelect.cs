using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameSelect : MonoBehaviour {

	public Text textbox;
	public Settings settings;
	public Text errorText;

	// Use this for initialization
	void Start () {
	
	}

	void Update () {

		if (Input.GetKey (KeyCode.Return))
			CheckAvailability ();

	}

	public void CheckAvailability () {

		string name = textbox.text;
		if (name == "")
			name = "The nameless one";
		string location = FindPerson (name);

		settings.pcName = name;

		if (location == "None") {
			SceneManager.LoadScene ("Heaven");
		} else {
//			if (location == "PurgatoryMino")
//				Debug.Log ("Make the minotaur scene already"); //make scene later
			if (location == "Purgatory" || location == "PurgatoryMino") {
				float red = PlayerPrefs.GetFloat (name.ToLower() + "R");
				float green = PlayerPrefs.GetFloat (name.ToLower() + "G");
				float blue = PlayerPrefs.GetFloat (name.ToLower() + "B");
				settings.color = new Color (red, green, blue);
				SceneManager.LoadScene ("Purgatory");
			} else if (location == "Hell") {
				errorText.gameObject.SetActive (true);
				errorText.text = name + " is in Hell.";
			} else if (location == "Heaven") {
				errorText.gameObject.SetActive (true);
				errorText.text = name + " already went to their wedding.";
			}
		}
	}

	string FindPerson (string name) {

		name = name.ToLower ();

		string[] realms = { "Hell", "PurgatoryMino", "Purgatory", "Heaven" };

		foreach (string realm in realms) {

			bool incomplete = true;
			int i = 1;

			while (incomplete) {

				string check = realm + i.ToString();
				string testSubject = PlayerPrefs.GetString (check);

				if (testSubject == PlayerPrefs.GetString ("not a real pref")) {
					incomplete = false;
				} else if (testSubject == name) {
					incomplete = false;
					return realm;
				} else
					i++;

			}

		}

		return "None";

	}

	void Prepare () {



	}

}
