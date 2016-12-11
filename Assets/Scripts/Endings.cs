using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Endings : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {

		string name = GameObject.FindGameObjectWithTag ("Settings").GetComponent<Settings> ().pcName;
		string species = GameObject.FindGameObjectWithTag ("Settings").GetComponent<Settings> ().pcName;
		string old = "PurgatoryMino";
		if (species == "angel")
			old = "Purgatory";
		string curr = "Heaven";

		GameObject.FindGameObjectWithTag ("Settings").GetComponent<Transfer> ().TransferRealms (name, old, curr);

		Settings settings = GameObject.FindGameObjectWithTag ("Settings").GetComponent<Settings> ();
		if (settings.species != "angel")
			text.text = "You came to your wedding as a minotaur. Your partner was... not psyched. The guests ran in fear, and the wedding was called off. Who could love a monster like you?";
		else if (settings.onlyInHeaven)
			text.text = "Congratulations! You've had a perfect wedding, and all the guests were impressed by the physical gauntlet you undertook to arrive today. With a start like this, only perfection is yet to come. Well done!";
		else
			text.text = "Congratulations! You were able to attend your wedding! Your trip to Purgatory made you very late, and most of the guests left before you arrived because of that, but you were still able to have a quiet ceremony with your new spouse. Have a wonderful life!";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
