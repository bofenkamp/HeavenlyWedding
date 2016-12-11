using UnityEngine;
using System.Collections;

public class AddToRealm : MonoBehaviour {

	public string location;

	// Use this for initialization
	void Start () {

		NaughtyList (location);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void NaughtyList(string realm) {

		Settings settings = GameObject.FindGameObjectWithTag ("Settings").GetComponent<Settings> ();
		string name = settings.pcName;
		name = name.ToLower ();
		bool incomplete = true;
		int i = 1;

		while (incomplete) {

			string call = realm + i.ToString ();
			string nextCall = realm + (i + 1).ToString ();
			if (PlayerPrefs.GetString (call) == PlayerPrefs.GetString (nextCall)) {
				PlayerPrefs.SetString (call, name);
				incomplete = false;
			} else if (PlayerPrefs.GetString (call) == name) {
				incomplete = false;
			} else
				i++;

		}

	}
}
