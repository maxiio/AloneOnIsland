using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AuswertungScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text mytext = this.GetComponent<Text> ();
		mytext.text = "Klasse du hast " + GameState.GamePoints + " Tage auf der Insel überlebt.";
	}

	public void loadMenu () {
		Application.LoadLevel ("menu");	
	}

}
