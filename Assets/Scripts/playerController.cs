using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	public AudioClip axeSound;
	public AudioClip fishingSound;
	public TOD tod;
	public bool equiped = false;

	Vector2 size = new Vector2(200,20);
	
	// Gesundheit
	public Vector2 healthPos = new Vector2(20,20);
	public float healthBarDisplay = 1;
	public Texture2D healthBarEmpty;
	public Texture2D healthBarFull;
	public int healthdecrease = 200;
	
	// Hunger
	public Vector2 hungerPos = new Vector2(20,50);
	public float hungerBarDisplay = 1;
	public Texture2D hungerBarEmpty;
	public Texture2D hungerBarFull;
	public int hungerincrease = 200;
	
	// Thirst
	public Vector2 thirstPos = new Vector2(20,80);
	public float thirstBarDisplay = 1;
	public Texture2D thirstBarEmpty;
	public Texture2D thirstBarFull;
	public int thirstincrease = 200;

	public Terrain myterrain;


	private TreeScript tree;

	private InventarScript inventory;
	private UIScript ui;

	public bool isNearTent = false;
	public bool isNearFire = false;
	public bool isSleeping = false;

	private bool getPoint = true;

	private float waketime = 8F;

	public float lastTimeFish;


	public Transform prefabFire; 
	public Transform prefabTent;

	// Use this for initialization
	void Start () {
		inventory = GameObject.Find("Inventory").GetComponent<InventarScript>();
		ui = GameObject.Find ("GUI").GetComponent<UIScript> ();
		if(tod == null)
			tod = GameObject.Find ("TOD").GetComponent<TOD> ();
		lastTimeFish = -1F;
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

		if (tod.Hour > 22 || tod.Hour < 5) {
			if(!isNearFire && !isNearTent)
				healthBarDisplay -= Time.deltaTime / healthdecrease * 3;

			if(Mathf.Floor(tod.Hour) == 23) {
				getPoint = true;
			}
			if((Mathf.Floor(tod.Hour) == 0) && (getPoint == true)) {
				GameState.GamePoints += 1;
				getPoint = false;
			}
		}

		if (isSleeping) {
			if(Mathf.Floor(waketime) == Mathf.Floor(tod.Hour)) {
				sleep(false);
			} else {
				if(hungerBarDisplay > 0.5 && thirstBarDisplay > 0.5) {
					if(healthBarDisplay <= 1) {
						healthBarDisplay += Time.deltaTime / healthdecrease * 5; 
					}
				}
				if (hungerBarDisplay >= 0) {
					hungerBarDisplay -= Time.deltaTime / hungerincrease * 4;
				}
				if (thirstBarDisplay >= 0) {
					thirstBarDisplay -= Time.deltaTime / thirstincrease * 2;
				}

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

	void OnCollisionStay(Collision collisionInfo) {
		GameObject other = collisionInfo.gameObject;
		if(other.tag == "Collectable") {
			ui.showHinweis(true, "Mit 'E' den Gegenstand aufheben");
			if(Input.GetKeyDown(KeyCode.E)) {
				inventory.addItemToInventory(other.name);
				Destroy(other);
				ui.showHinweis(false, "");
			}
		}

		if (other.tag == "Fire") {
			other.transform.rigidbody.isKinematic = true;
		}
		if (other.tag == "Tent") {
			ui.showHinweis(true, "Mit 'T' kannst du schlafen gehen.");
			if(Input.GetKeyDown(KeyCode.T)) {
				ui.showHinweis(false, "");
				sleep(true);
			}
		}
	}

	void OnCollisionExit(Collision collisionInfo) {
		GameObject other = collisionInfo.gameObject;
		if(other.tag == "Collectable") {
			ui.showHinweis(false, "");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Tent") {
			isNearTent = true;
		}
		if (other.tag == "Fire") {
			isNearFire = true;
		}
	}

	void OnTriggerStay(Collider other) {
		if (Input.GetMouseButtonDown (0)) {
			if(other.tag == "Tree") {
				if(inventory.getEquip()[0] == "Axe" || inventory.getEquip()[1] == "Axe") {
					audio.PlayOneShot(axeSound);
					tree = other.GetComponent<TreeScript>();
					tree.treeHealth -= 1;
				} else {
					ui.showHinweis("Sie benötigen eine Axt um Bäume zu fällen", 3.0F);
				}

			}
			if(other.tag == "Cliff") {
				ui.showHinweis("Sie benötigen eine Picke um Steine zu schlagen", 3.0F);
			}

		}

		if(other.tag == "Wasser") {
			if(inventory.getBottles() > 0) {
				ui.showHinweis(true, "Mit 'E' kannst du Wasser trinken. Mit 'T' kannst du deine Flaschen auffüllen. ");	
				if(Input.GetKeyDown(KeyCode.E)) {
					drink(0.2F);
				}
				if(Input.GetKeyDown(KeyCode.T)) 
					inventory.addItemToInventory("FlascheWasser");
			} else {
				ui.showHinweis(true, "Mit 'E' kannst du Wasser trinken.");	
				if(Input.GetKeyDown(KeyCode.E)) {
					drink(0.2F);
				}
			}

		}

		if (other.tag == "Meer") {
			if (Input.GetMouseButtonDown (0)) {
				if(inventory.getEquip()[0] == "Net" || inventory.getEquip()[1] == "Net") {
					if((lastTimeFish - tod.Hour) <= -2) {
						int fishcatched = Random.Range(0,3) + 1;
						audio.PlayOneShot(fishingSound);
						ui.showHinweis("Du hast " + fishcatched + " Fische gefangen. Du kannst in 2 Stunden wieder fischen.", 2.0F);
						for(int i = 0; i < fishcatched; i++) {
							inventory.addItemToInventory("Fish");
						};
						lastTimeFish = tod.Hour;					
					} else if((lastTimeFish - tod.Hour) < 0) {						
						ui.showHinweis("Du kannst nur alle 2 Stunden fischen.", 3.0F);
					}


				} else {
					ui.showHinweis("Mit einem Netz kannst du fischen.", 3.0F);
				}
			}
		}

		if(other.tag == "Searchable") {
			ui.showHinweis(true, "Mit 'E' den Gegenstand durchsuchen");	
			if(Input.GetKeyDown(KeyCode.E)) {	
				GameObject collisionInfo = other.gameObject;
				string myitem = collisionInfo.GetComponent<SearchableScript>().search();
				if(myitem != "") {
					inventory.addItemToInventory(myitem);
					ui.showHinweis("Du hast " + myitem + " gefunden.", 3.0F);
				} else {
					ui.showHinweis("Du hast nichts gefunden.", 3.0F);
				}
			}
		}
	}

	void OnTriggerExit(Collider other) {
		ui.showHinweis(false, "");

		if (other.tag == "Tent") {
			isNearTent = false;
		}

		if (other.tag == "Fire") {
			isNearFire = false;
		}
	}

	void PlayerDeath() {
		GameState.EndGame ();
	}		                  

	public void drink(float zahl) {
		thirstBarDisplay += zahl;
	}

	public void eat(float zahl) {
		hungerBarDisplay += zahl;
	}

	public void makeFire() {
		Quaternion myrot = transform.rotation;
		myrot.eulerAngles = new Vector3 (270, myrot.eulerAngles.y, myrot.eulerAngles.z);
		Instantiate (prefabFire, new Vector3 (this.transform.position.x + 1.5F, this.transform.position.y, this.transform.position.z - 1.5F), myrot);
	}

	public void sentTent() {
		Quaternion myrot = transform.rotation;
		myrot.eulerAngles = new Vector3 (0, myrot.eulerAngles.y, myrot.eulerAngles.z);
		Instantiate (prefabTent, new Vector3 (this.transform.position.x + 1.5F, this.transform.position.y - 1F, this.transform.position.z - 1.5F), myrot);
	}

	public void sleep(bool gosleep) {
		if (gosleep) {
			this.GetComponent<pauseGameScript>().isSleeping = gosleep;
			isSleeping = gosleep;
			this.GetComponent<SleepScript>().sleep(gosleep);
			waketime = tod.Hour + 6;
			if(waketime > 23)
				waketime -= 24;
			tod.speed = 80;
		} else {
			this.GetComponent<pauseGameScript>().isSleeping = gosleep;
			isSleeping = gosleep;
			this.GetComponent<SleepScript>().sleep(gosleep);
			waketime = 8F;
			tod.speed = 2000;
		}
	}
}
