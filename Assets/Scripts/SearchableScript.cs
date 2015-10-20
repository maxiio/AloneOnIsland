using UnityEngine;
using System.Collections;

public class SearchableScript : MonoBehaviour {
	public string Itemname;

	public string search() {
		string myitem = Itemname;
		Itemname = "";
		this.gameObject.tag = "Untagged";
		return myitem;
	}
}
