using UnityEngine;
using System.Collections;

public class OptionsGUIScript : BaseGUIScript {

	private string _BackText = "Back";

	///////////////////////////////////////////////////////////////
	// PUBLIC functions
	///////////////////////////////////////////////////////////////

	public override void DrawGUI(){

		// just to back to main menu button
		if(GUI.Button(new Rect((Screen.width - ButtonDimension.x)*0.5f, 
		                       (Screen.height - ButtonDimension.y)*0.5f,
		                       ButtonDimension.x, 
		                       ButtonDimension.y), 
		              _BackText)){
			_Main.SetMenuGUI();
		}

	}
}
