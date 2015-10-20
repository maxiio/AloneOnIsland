using UnityEngine;
using System.Collections;

public static class GameState {
	// time in seconds of the real elapsed 
	// time of the game while playing
	public static float GameTime = 0f;

	// means level is loaded, player is inside the game
	public static bool IsRunning = false;

	// is game paused (pause menu)
	public static bool IsPausing = false;

	public static float GamePoints = 0f;
	// private vars

	// for remembering the start time for each period
	private static float GameStartTime = 0f;

	private static PrefabScript prefabs = null;
	// public functions

	// starts the game, after level was loaded
	public static void StartGame(){
		// reset game time
		GameTime = 0f;
		ContinueGame ();
	}

	// called at the beginning or after a pause
	// start next time period
	public static void ContinueGame(){

		// remember start time
		GameStartTime = Time.time;

		IsRunning = true;
		IsPausing = false;
	}

	// pause game, elapsed time will not be tracked
	public static void PauseGame(){
		// add elapsed time
		SaveGameTime();

		IsRunning = false;
		IsPausing = true;
	}

	// end game, leave level etc.
	public static void EndGame(){

		// add elapsed time
		SaveGameTime ();

		IsRunning = false;
		IsPausing = false;

		Application.LoadLevel ("endgame");
	}


	// function returns the elapsed game time
	public static float GetGameTime(){

		// time from last periods and 
		// current elapsed time
		return GameTime + (Time.time - GameStartTime);
	}

	// sets the prefab singleton
	public static void SetPrefabScript(PrefabScript script){
		// only one instance possible
		if (prefabs == null) {
			prefabs = script;
		} else {
			Debug.Log ("PrefabScript already set!");
			Debug.Log ("Multiple components in scene?");
		}
	}

	public static PrefabScript GetPrefabScript(){
		return prefabs;
	}


	// private functions
	
	// add elapsed game time when game is paused or finished
	private static void SaveGameTime(){

		// add time period to total game time played
		GameTime += ( Time.time - GameStartTime );
	}
}
