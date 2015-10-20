using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventarScript : MonoBehaviour {
	private ItemScript itemlog;
	private ItemScript itemcoco;
	private ItemScript itemaxe;
	private ItemScript itemfire;
	private ItemScript itemtent;
	private ItemScript itemnet;
	private ItemScript itemfish;
	private ItemScript itemmeat;
	private ItemScript itemhealth; 
	private ItemScript itemBottle;
	private ItemScript itemStone;
	
	private GameObject invenpen;

	private GameObject qp;
	private GameObject qpItem;
	private Component[] qptexts;
	private Component[] qpimages;

	public string rightHand;
	public string leftHand;

	private WeaponScript axe;
	private WeaponScript net;

	public GameObject panel;

	private playerController player;

	private AudioSource craftingcorrect;


	public Text inventhin;

	// Use this for initialization
	void Start () {
		invenpen = GameObject.Find ("InvenPan");
		inventhin = GameObject.Find ("InventarHinweis").GetComponent<Text> ();

		player = GameObject.Find ("First Person Controller").GetComponent<playerController> ();

		qp = GameObject.Find ("QuestionPanel");
		qptexts = qp.GetComponentsInChildren<Text> ();
		qpimages = qp.GetComponentsInChildren<Image> ();

		itemlog = GameObject.Find ("ItemLog").GetComponent<ItemScript> ();
		itemcoco = GameObject.Find ("ItemCoco").GetComponent<ItemScript> ();
		itemaxe = GameObject.Find ("ItemAxe").GetComponent<ItemScript> ();
		itemfire = GameObject.Find ("ItemFire").GetComponent<ItemScript> ();
		itemtent = GameObject.Find ("ItemTent").GetComponent<ItemScript> ();
		itemnet = GameObject.Find ("ItemNet").GetComponent<ItemScript> ();
		itemfish = GameObject.Find ("ItemFish").GetComponent<ItemScript> ();
		itemmeat = GameObject.Find ("ItemMeat").GetComponent<ItemScript> ();
		itemhealth = GameObject.Find ("ItemHealth").GetComponent<ItemScript> ();
		itemBottle = GameObject.Find ("ItemBottle").GetComponent<ItemScript> ();
		itemStone = GameObject.Find ("ItemStone").GetComponent<ItemScript> ();

		if(craftingcorrect == null)
			craftingcorrect = GameObject.Find("CraftingCorrect").audio;

		axe = GameObject.Find ("axe").GetComponent<WeaponScript> ();
		net = GameObject.Find ("net").GetComponent<WeaponScript> ();

		leftHand = "";
		rightHand = "";
		setInventarHinweis ("");
	}

	public void pausedTheGame(bool showGUI) {
		invenpen.SetActive (showGUI);
		this.gameObject.SetActive (false);
		this.gameObject.SetActive (true);
		// panel.SetActive (showGUI);
	}

	public void addItemToInventory(string name) {
		if(name == "LogBig(Clone)" || name == "LogBig") {
			itemlog.count += 1;
		}
		if(name == "Coconut(Clone)" || name == "Coconut") {
			itemcoco.count += 1;
		}

		if (name == "Axe") {
			itemaxe.count += 1;
		}
		if (name == "Fire") {
			itemfire.count += 1;
		}
		if (name == "Fish") {
			itemfish.count += 1;
		}
		if (name == "rock1") {
			itemStone.count += 1;
		}
		if (name == "Net" || name == "Netz") {
			itemnet.count += 1;
		}
		if (name == "Tent") {
			itemtent.count += 1;
		}
		if (name == "Verbandskasten") {
			itemhealth.count += 1;
		}
		if (name == "Flasche") {
			itemBottle.maxCount += 1;
		}
		if (name == "FlascheWasser") {
			if(itemBottle.maxCount > itemBottle.count) 
				itemBottle.count = itemBottle.maxCount;
		}

	}

	public void setHand(GameObject myWeapon) {
		if (myWeapon.name == "ItemAxe") {
			rightHand = "Axe";
			axe.equipWeapon();
			player.equiped = true;
		}
		if (myWeapon.name == "ItemNet") {
			leftHand = "Net";
			net.equipWeapon();
			player.equiped = true;
		}


	}

	public string[] getEquip() {
		string[] myequip = { rightHand, leftHand };
		return myequip;
	}

	public void setQuestion(GameObject usableObject) {
		qpItem = usableObject;
		foreach (Text mytext in qptexts) {
			mytext.enabled = true;
			if(mytext.name == "Question") {
				mytext.text = getQuestionText(usableObject);
			}
		}
		foreach (Image myimage in qpimages) {
			myimage.enabled = true;
		}


	}

	public void setInventarHinweis(string text) {
		inventhin.text = text;
	}

	public void hideQuestion() {
		foreach (Text mytext in qptexts) {
			mytext.enabled = false;
		}
		foreach (Image myimage in qpimages) {
			myimage.enabled = false;
		}
	}

	private string getQuestionText(GameObject usableObject) {
		string questiontext = "";
		if (usableObject.name == "ItemCoco") {
			questiontext = "Möchtest du die Kokosnuss essen? Dadurch wird ein wenig Durst und Hunger vermindert.";
			return questiontext;
		}
		if (usableObject.name == "ItemFire") {
			questiontext = "Möchtest du ein Feuer machen?";
			return questiontext;
		}
		if (usableObject.name == "ItemFish") {
			questiontext = "Möchtest du den rohen Fisch essen? Dadurch wird ein wenig Hunger vermindert. Fisch kann auch über Feuer gebraten werden.";
			if(player.isNearFire) 
				questiontext = "Möchtest du ein Fisch braten?";
			return questiontext;
		}
		if (usableObject.name == "ItemTent") {
			questiontext = "Möchtest du dein Zelt hier absetzen? Überleg gut, denn du hast vermutlich nur 1 Zelt.";
			return questiontext;
		}
		if (usableObject.name == "ItemMeat") {
			questiontext = "Möchtest du ein Stück Fleisch essen? Du vermindest dein Hunger dadurch enorm.";
			return questiontext;
		}
		if (usableObject.name == "ItemHealth") {
			questiontext = "Möchtest du dein Verbandskasten benutzen? Du bekommst 50% Leben zurück.";
			return questiontext;
		}
		if (usableObject.name == "ItemBottle") {
			questiontext = "Möchtest du frisches Wasser aus deiner Flasche trinken? Du bekommst 50% Durst zurück.";
			return questiontext;
		}

		
		return questiontext;
	}

	public void useUsableItem() {
		if (qpItem.name == "ItemCoco") {
			if(itemcoco.count > 0) {
				itemcoco.count -= 1;
				player.eat(0.05F);
				player.drink(0.1F);
				hideQuestion();
				craftingcorrect.audio.Play ();
			} else {
				hideQuestion();
			}
		}
		if (qpItem.name == "ItemFire") {
			if(itemfire.count > 0) {
				itemfire.count -= 1;
				player.makeFire();
				hideQuestion();
				craftingcorrect.audio.Play ();
			} else {
				hideQuestion();
			}
		}		
		if (qpItem.name == "ItemTent") {
			if(itemtent.count > 0) {
				itemtent.count -= 1;
				player.sentTent();
				craftingcorrect.audio.Play ();
				hideQuestion();
			} else {
				hideQuestion();
			}
		}

		if (qpItem.name == "ItemFish") {
			if(itemfish.count > 0) {
				if(player.isNearFire) {
					itemmeat.count += 1;
					itemfish.count -= 1;
					craftingcorrect.audio.Play ();
					hideQuestion();
				} else {
					itemfish.count -= 1;
					player.eat(0.2F);
					craftingcorrect.audio.Play ();
					hideQuestion();
				}
			} else {
				hideQuestion();
			}
		}

		if (qpItem.name == "ItemMeat") {
			if(itemmeat.count > 0) {
				itemmeat.count -= 1;
				player.eat(0.5F);
				craftingcorrect.audio.Play ();
				hideQuestion();
			}
		}

		if (qpItem.name == "ItemHealth") {
			if(itemhealth.count > 0) {
				itemhealth.count -= 1;
				player.healthBarDisplay += 0.5F;
				craftingcorrect.audio.Play ();
				hideQuestion();
			} else {
				hideQuestion();
			}
		}

		if (qpItem.name == "ItemBottle") {
			if(itemBottle.count > 0) {
				itemBottle.count -= 1;
				player.drink(0.5F);
				craftingcorrect.audio.Play ();
				hideQuestion();
			} else {
				hideQuestion();
			}
		}
	}

	public int getBottles() {
		return itemBottle.maxCount;
	}
	                        

}
