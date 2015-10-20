using UnityEngine;
using System.Collections;

public class ingameGUI : MonoBehaviour {

	Vector2 size = new Vector2(200,20);

	// Gesundheit
	public Vector2 healthPos = new Vector2(20,20);
	float healthBarDisplay = 1;
	public Texture2D healthBarEmpty;
	public Texture2D healthBarFull;
	public int healthdecrease = 200;

	// Hunger
	public Vector2 hungerPos = new Vector2(20,50);
	float hungerBarDisplay = 1;
	public Texture2D hungerBarEmpty;
	public Texture2D hungerBarFull;
	public int hungerincrease = 200;

	// Thirst
	public Vector2 thirstPos = new Vector2(20,80);
	float thirstBarDisplay = 1;
	public Texture2D thirstBarEmpty;
	public Texture2D thirstBarFull;
	public int thirstincrease = 200;

	
	// Use this for initialization
	void Start () {
	
	}

	void OnGUI () {
		// Health GUI
		GUI.BeginGroup (new Rect (healthPos.x, healthPos.y, size.x, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), healthBarEmpty);

		GUI.BeginGroup (new Rect (0, 0, size.x * healthBarDisplay, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), healthBarFull);

		GUI.EndGroup ();
		GUI.EndGroup ();

		// Hunger GUI
		GUI.BeginGroup (new Rect (hungerPos.x, hungerPos.y, size.x, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), hungerBarEmpty);
		
		GUI.BeginGroup (new Rect (0, 0, size.x * hungerBarDisplay, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), hungerBarFull);
		
		GUI.EndGroup ();
		GUI.EndGroup ();

		// Thirst GUI
		GUI.BeginGroup (new Rect (thirstPos.x, thirstPos.y, size.x, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), thirstBarEmpty);
		
		GUI.BeginGroup (new Rect (0, 0, size.x * thirstBarDisplay, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), thirstBarFull);
		
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
	
	// Update is called once per frame
	void Update () {
		// Health
		if (hungerBarDisplay <= 0 && thirstBarDisplay <= 0) {
			healthBarDisplay -= Time.deltaTime / healthdecrease * 2;
		} else {
			if (hungerBarDisplay <= 0 || thirstBarDisplay <= 0) {
				healthBarDisplay -= Time.deltaTime / healthdecrease;
			}
		}

		if (healthBarDisplay <= 0) {
			PlayerDeath();
		}


		// Hunger 
		if (hungerBarDisplay >= 0) {
			hungerBarDisplay -= Time.deltaTime / hungerincrease;
		}

		if (hungerBarDisplay <= 0) {
			hungerBarDisplay = 0;
		}

		if (hungerBarDisplay >= 1) {
			hungerBarDisplay = 1;
		}

		// Thirst 
		if (thirstBarDisplay >= 0) {
			thirstBarDisplay -= Time.deltaTime / thirstincrease;
		}
		
		if (thirstBarDisplay <= 0) {
			thirstBarDisplay = 0;
		}
		
		if (thirstBarDisplay >= 1) {
			thirstBarDisplay = 1;
		}
	}

	void PlayerDeath() {
		Application.LoadLevel ("menu");
	}
}
