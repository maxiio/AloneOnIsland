using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemScript : MonoBehaviour {
	public int count = 0;
	public bool usable = false;
	public bool craftable = false;
	public bool wearable = false;
	public GameObject slot;
	public Text counter;
	public int maxCount = 0;

	// Use this for initialization
	void Start () {
		slot = transform.parent.gameObject;
		counter = transform.GetChild (1).GetChild(0).GetComponent<Text> ();
		counter.text = count.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (counter.text != count.ToString ())
						counter.text = count.ToString ();
	}

	public bool Usable {
		get {
			return usable;
		}
	}

	public bool Craftable {
		get {
			return craftable;
		}
	}
	
	public bool Wearable {
		get {
			return wearable;
		}
	}
}
