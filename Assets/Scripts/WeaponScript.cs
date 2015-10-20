using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	GameObject theWeapon;

	// Use this for initialization
	void Start () {
		theWeapon = this.gameObject;
		deequipWeapon ();
	}

	public void equipWeapon() {
		theWeapon.GetComponent<MeshRenderer> ().enabled = true;
	}

	public void deequipWeapon() {
		theWeapon.GetComponent<MeshRenderer> ().enabled = false;
	}
	

}
