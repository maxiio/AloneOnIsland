using UnityEngine;
using System.Collections;

public class BaseGUIScript : MonoBehaviour {

	// 
	public static Vector2 ButtonDimension = new Vector2(250,50);

	// flag if active or not
	protected bool _IsActive = false;

	// for access to the manager GUI
	protected ManagerGUIScript _Main;

	///////////////////////////////////////////////////////////////
	// UNITY Functions
	///////////////////////////////////////////////////////////////

	// Use this for initialization
	void Start () {
		_Main = GetComponent<ManagerGUIScript> ();
	}

	///////////////////////////////////////////////////////////////
	// PUBLIC functions
	///////////////////////////////////////////////////////////////

	// draws the GUI
	virtual public void DrawGUI(){}

	///////////////////////////////////////////////////////////////
	// GETTER and SETTER
	///////////////////////////////////////////////////////////////

	virtual public void SetActive(){
		_IsActive = true;
	}
	
	virtual public void SetInactive(){
		_IsActive = false;
	}
}
