using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SleepScript : MonoBehaviour {
	public GameObject FadePanel;

	// Use this for initialization
	void Start () {
		if (FadePanel == null)
			FadePanel = GameObject.Find("SleepPanel");

		FadePanel.SetActive (false);
	}

	public void sleep(bool sleeping) {	
		FadePanel.SetActive (sleeping);
	}



}
