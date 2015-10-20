using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
	GameObject hinweis;
	private Component[] hinweisText;
	private Component[] hinweisBild;
	private float showtime;
	private float actime;

	void Start() {
		hinweis = GameObject.Find ("Hinweis");
		hinweisText = hinweis.GetComponentsInChildren<Text> ();
		hinweisBild = hinweis.GetComponentsInChildren<Image> ();
		showHinweis(false, "");
		showtime = 0;
	}

	public void showHinweis(bool show) {
		showHinweis(show, "");
	}

	public void showHinweis(bool show, string thetext) {
		if (show) {
			foreach (Text mytext in hinweisText) {
				mytext.enabled = true;
				mytext.text = thetext; 
			}
			foreach (Image myimage in hinweisBild) {
				myimage.enabled = true;
			}
		} else {
			foreach (Text mytext in hinweisText) {
				mytext.enabled = false;
				mytext.text = thetext;
			}
			foreach (Image myimage in hinweisBild) {
				myimage.enabled = false;
			}
		}

	}

	public void showHinweis(string thetext, float time) {
		actime = Time.deltaTime;
		showtime = time;
		showHinweis (true, thetext);
	}

	void Update() {
		if (actime > 0) {
			actime += Time.deltaTime;
			if(actime > showtime) {
				showHinweis(false);
				actime = 0;
			}
		}
	}
}
