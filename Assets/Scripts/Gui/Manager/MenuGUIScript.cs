using UnityEngine;
using System.Collections;

public class MenuGUIScript : BaseGUIScript {
	

	
	private bool _IsStart = true;
	private int _TimeForLogoScreen = 2;

	// texture shown on startup, must be selected in the scene
	public Texture2D LogoScreen;

	///////////////////////////////////////////////////////////////
	// PUBLIC functions
	///////////////////////////////////////////////////////////////
	
	public override void DrawGUI(){

		// only draw texture on startup
		if(_IsStart && LogoScreen != null){
			// just a texture
			GUI.Label(new Rect(
						(Screen.width - LogoScreen.width) * 0.5f, 
						(Screen.height - LogoScreen.height) * 0.5f, 
						LogoScreen.width, LogoScreen.height), 
			          LogoScreen);
		} else {
			GUI.Label(new Rect(
				(Screen.width - LogoScreen.width) * 0.5f, 
				(Screen.height - LogoScreen.height) * 0.1f, 
				LogoScreen.width, LogoScreen.height), 
			          LogoScreen);


			// start game button
			if(GUI.Button(new Rect(
						(Screen.width - ButtonDimension.x) * 0.5f, 
						(Screen.height - ButtonDimension.y) * 0.5f,
						ButtonDimension.x, 
						ButtonDimension.y), 
			       		"Start Game",
			            _Main.ButtonStyle)){
				// start the game
				// ....	
				Application.LoadLevel("theIsland");
			}
			// options button
			if(GUI.Button(new Rect(
						(Screen.width - ButtonDimension.x) * 0.5f, 
						(Screen.height - ButtonDimension.y) * 0.5f + ButtonDimension.y + 10, 
						ButtonDimension.x, 
						ButtonDimension.y), 
			           	"Optionen",
			            _Main.ButtonStyle)){
				// set the options GUI
				_Main.SetOptionsGUI();	
			}
		}
	}

	///////////////////////////////////////////////////////////////
	// GETTER and SETTER
	///////////////////////////////////////////////////////////////

	// if set active and logo was not shown before, show logo
	public override void SetActive(){
		if(_IsStart){
			StartCoroutine(_ShowLogoScreen());	
		}
	}
	
	private IEnumerator _ShowLogoScreen(){
		yield return new WaitForSeconds(_TimeForLogoScreen);
		_IsStart = false;
	}
}
