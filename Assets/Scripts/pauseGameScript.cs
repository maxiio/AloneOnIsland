using UnityEngine;
using System.Collections;

public class pauseGameScript : MonoBehaviour {
	public bool showGUI = false;
	public bool showtutor = false;
	public bool isSleeping = false;
	private MouseLook characterMouseLook;
	private MouseLook mcamera;
	private FPSInputController fpscontroller;
	private MotionScript motion;
	private InventarScript inventory;
	private TutScript tutorial;

	// Use this for initialization
	void Start () {
		characterMouseLook = GetComponent<MouseLook> ();
		mcamera = GameObject.Find("Main Camera").GetComponent<MouseLook> ();
		fpscontroller = GetComponent<FPSInputController> ();
		motion = GameObject.Find ("Arms").GetComponent<MotionScript> ();
		inventory = GameObject.Find ("Inventory").GetComponent<InventarScript> ();
		inventory.pausedTheGame (false);

		tutorial = GameObject.Find ("Tut").GetComponent<TutScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if(GameState.IsPausing) {
				showGUI = false;
				inventory.pausedTheGame (showGUI);
				GameState.ContinueGame();
			} else {
				showGUI = true;
				inventory.pausedTheGame (showGUI);
				GameState.PauseGame();			
			}
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			if(GameState.IsPausing) {
				GameState.ContinueGame();
			} else {
				GameState.PauseGame();			
			}

		}

		if (Input.GetKeyDown (KeyCode.F1)) {
			if(GameState.IsPausing) {
				showtutor = false;
				tutorial.showTutPan(showtutor);
				GameState.ContinueGame();
			} else {
				showtutor = true;
				tutorial.showTutPan(showtutor);
				GameState.PauseGame();			
			}
		}
		    
		freezeGame ();
	}
	
	private void freezeGame() {
		if (GameState.IsPausing) {
			Time.timeScale = 0;
			canMove (false);
			Screen.showCursor = true;
		} else if (isSleeping) {
			canMove (false);
			Screen.showCursor = false;
		} else {
			Time.timeScale = 1.0f;
			canMove(true);
			Screen.showCursor = false;
		}
	}

	public void canMove(bool freezing) {
		characterMouseLook.enabled = freezing;
		mcamera.enabled = freezing;
		fpscontroller.enabled = freezing;
		motion.enabled = freezing;
	}

	public void showTut() {
		showtutor = true;
		tutorial.showTutPan(showtutor);
		GameState.PauseGame();	
	}

	public void hideTut() {
		showtutor = false;
		tutorial.showTutPan(showtutor);
		GameState.ContinueGame();
	}
}
