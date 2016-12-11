using UnityEngine;
using System.Collections;

public class Transfer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TransferRealms(string name, string old, string curr) {

		bool incomplete = true;
		int i = 1;

		while (incomplete) {

			string check = old + i.ToString();
			string testSubject = PlayerPrefs.GetString (check);

			if (testSubject == PlayerPrefs.GetString ("not a real pref")) {
				incomplete = false;
			} else if (testSubject == name) {

				int i2 = i + 1;
				bool incomplete2 = true;

				while (incomplete2) {

					string call = old + i2.ToString();
					string prevCall = old + (i2 - 1).ToString ();
					string nextCall = old + (i2 + 1).ToString ();

					PlayerPrefs.SetString (prevCall, PlayerPrefs.GetString (call));

					if (PlayerPrefs.GetString (call) == PlayerPrefs.GetString (nextCall))
						incomplete2 = false;
					else
						i2++;

				}

				incomplete = false;

			} else
				i++;

		}

		incomplete = true;
		i = 1;
		name = name.ToLower ();

		while (incomplete) {

			string call = curr + i.ToString ();
			if (PlayerPrefs.GetString (call) == PlayerPrefs.GetString ("not a real pref")) {
				PlayerPrefs.SetString (call, name);
				incomplete = false;
			} else
				i++;

		}

	}
}
