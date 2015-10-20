using UnityEngine;
using System.Collections;

public class ManagerGUIScript : MonoBehaviour {

	// custom skin
	public GUISkin CustomSkin;

	// custom style
	public GUIStyle ButtonStyle;
	
	// current GUI
	private BaseGUIScript CurrentGUI;
	
	// possible GUIs
	private MenuGUIScript MenuGUI;
	private PlayGUIScript PlayGUI;
	private OptionsGUIScript OptionsGUI;
	// ... may be continued
	
	// flag for initialization
	private bool _initGUI = false;
	
	///////////////////////////////////////////////////////////////
	// UNITY Functions
	///////////////////////////////////////////////////////////////
	
	// Use this for initialization
	void Start () {
		
		// get other gui scripts
		MenuGUI = gameObject.GetComponent<MenuGUIScript>();
		PlayGUI = gameObject.GetComponent<PlayGUIScript>();
		OptionsGUI = gameObject.GetComponent<OptionsGUIScript>();
		
		// start with the menu gui
		_SetNewGUI(MenuGUI);
	}
	
	// Unity GUI function
	void OnGUI(){

		// use custom skin if not defined in the editor
		if (CustomSkin != null) {
			GUI.skin = CustomSkin;
		}
		
		if (!_initGUI) {
			// if not set in the editor
			if (CustomSkin != null) {
				// ta
				ButtonStyle = CustomSkin.GetStyle ("CustomButtonStyle");
			} else {
				ButtonStyle = GUI.skin.GetStyle ("Button");
			}
			_initGUI = true;
		}
		
		if(CurrentGUI != null){
			CurrentGUI.DrawGUI();	
		}
	}
	
	///////////////////////////////////////////////////////////////
	// GETTER and SETTER
	///////////////////////////////////////////////////////////////
	
	// set play GUI
	public void SetPlayGUI(){
		_SetNewGUI(PlayGUI);
	}

	// set Menu GUI
	public void SetMenuGUI(){
		_SetNewGUI(MenuGUI);
	}

	// set Options GUI
	public void SetOptionsGUI(){
		_SetNewGUI(OptionsGUI);
	}

	// set given GUI if not known before
	public void SetGivenGUI(BaseGUIScript newGUI){
		_SetNewGUI(OptionsGUI);
	}
	
	///////////////////////////////////////////////////////////////
	// PUBLIC functions
	///////////////////////////////////////////////////////////////
	
	///////////////////////////////////////////////////////////////
	// PRIVATE functions
	///////////////////////////////////////////////////////////////
	
	// set the new GUI as current GUI
	private void _SetNewGUI(BaseGUIScript NewGUI){
		// if defined, set inactive
		if(CurrentGUI != null){
			CurrentGUI.SetInactive();	
		}
		// save new GUI
		CurrentGUI = NewGUI;
		// set it active
		CurrentGUI.SetActive();
	}
}
