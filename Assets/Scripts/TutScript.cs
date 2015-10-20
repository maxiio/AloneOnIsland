using UnityEngine;
using System.Collections;

public class TutScript : MonoBehaviour {
	GameObject tutpan;

	// Use this for initialization
	void Start () {
		tutpan = GameObject.Find ("Tutpanel");

		tutpan.SetActive (false);
	}
	
	public void showTutPan(bool show) {
		tutpan.SetActive (show);
		this.gameObject.SetActive (false);
		this.gameObject.SetActive (true);
	}
}
