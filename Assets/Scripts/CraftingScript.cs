using UnityEngine;
using System.Collections;

public class CraftingScript : MonoBehaviour {
	private InventarScript inventar;
	private AudioSource craftingcorrect;
	private AudioSource craftingwrong;


	void Start() {
		inventar = GameObject.Find ("Inventory").GetComponent<InventarScript> ();
		if(craftingcorrect == null)
			craftingcorrect = GameObject.Find("CraftingCorrect").audio;
		if(craftingwrong == null)
			craftingwrong = GameObject.Find("CraftingWrong").audio;
	}

	public void CraftItem() {
		inventar.setInventarHinweis ("");
		GameObject craftItemLeftSlot = GameObject.Find ("CraftingLeft");
		GameObject craftItemRightSlot = GameObject.Find ("CraftingRight");
		if (craftItemLeftSlot.transform.childCount > 0 && craftItemRightSlot.transform.childCount > 0) {
			GameObject craftItemLeft = craftItemLeftSlot.transform.GetChild(0).gameObject;
			GameObject craftItemRight = craftItemRightSlot.transform.GetChild(0).gameObject;
			combineItems(craftItemLeft, craftItemRight);
		} else if(craftItemLeftSlot.transform.childCount > 0 || craftItemRightSlot.transform.childCount > 0) {
			if(craftItemLeftSlot.transform.childCount > 0) {
				GameObject craftItem = craftItemLeftSlot.transform.GetChild(0).gameObject;
				combineItem(craftItem);	
			} else {
				GameObject craftItem = craftItemRightSlot.transform.GetChild(0).gameObject;
				combineItem(craftItem);	
			}
					 
		} else {
			inventar.setInventarHinweis("Sie haben zu wenig Items");
			craftingwrong.audio.PlayDelayed(0.1F);
		}
	}

	private void combineItems(GameObject ob1, GameObject ob2) {
		ItemScript item1 = ob1.GetComponent<ItemScript> ();
		ItemScript item2 = ob2.GetComponent<ItemScript> ();
		if ((ob1.name == "ItemLog" && ob2.name == "ItemStone") || (ob2.name == "ItemLog" && ob1.name == "ItemStone")) {
			if(item1.count >= 5 && item2.count >= 5) {
				inventar.addItemToInventory("Axe");
				craftingcorrect.audio.PlayDelayed(0.1F);
				item1.count -= 5;
				item2.count -= 5;
			} else {
				inventar.setInventarHinweis("Du hast zu wenig Holz oder Stein. Du brauchst jeweils 5 für eine Axt.");
				craftingwrong.audio.PlayDelayed(0.1F);
			}
		}
		if ((ob1.name == "ItemLog" && ob2.name == "ItemSchirm") || (ob2.name == "ItemLog" && ob1.name == "ItemSchirm")) {
			if(item1.count >= 5 && item2.count >= 1) {
				inventar.addItemToInventory("Tent");
				item1.count -= 5;
				item2.count -= 1;
				craftingcorrect.audio.PlayDelayed(0.1F);
			} else if(item1.count >= 1 && item2.count >= 5) {
				inventar.addItemToInventory("Tent");
				craftingcorrect.audio.PlayDelayed(0.1F);
				item1.count -= 1;
				item2.count -= 5;
			}else {
				craftingwrong.audio.PlayDelayed(0.1F);
				inventar.setInventarHinweis("Du hast zu wenig Holz (5) oder kein Schirm mehr um ein Zelt zu bauen.");
			}
		}

	
	}

	private void combineItem(GameObject ob1) {
		ItemScript item1 = ob1.GetComponent<ItemScript> ();
		if (ob1.name == "ItemLog") {
			if(item1.count >= 15) {
				inventar.addItemToInventory("Fire");
				craftingcorrect.audio.PlayDelayed(0.1F);
				item1.count -= 15;
			} else {
				craftingwrong.audio.PlayDelayed(0.1F);
				inventar.setInventarHinweis("Du hast zu wenig Holz für ein Feuer. Du benötigst 15 Holzstücke.");
			}

		}
	}
}
