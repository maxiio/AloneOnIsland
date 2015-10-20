using UnityEngine;
using System.Collections;

public class PlayGUIScript : BaseGUIScript {

	// draws the GUI
	override public void DrawGUI(){

		if (!_IsActive) { return; }

		// show points
		GUI.Label(new Rect(0,
		                   0,
		                   200,
		                   20),
		          "Punkte: " + GameState.GamePoints);
		// show time
		GUI.Label(new Rect(Screen.width-200,
		                   0,
		                   200,
		                   20),
		          "Zeit: " + _CreateTimeString(GameState.GameTime));
	}

	// creates a formatted time string by given seconds
	private string _CreateTimeString(float Time){
		int minutes = (int)Time / 60;
		int seconds = (int)Time % 60;
		
		string minuteString = minutes.ToString();
		if(minutes < 10){
			minuteString = "0" + minuteString;	
		}
		string secondsString = seconds.ToString();
		if(seconds < 10){
			secondsString = "0" + secondsString;	
		}
		return minuteString + ":" + secondsString;	
	}
}
