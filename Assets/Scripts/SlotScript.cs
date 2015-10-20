using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler {
	public bool isCraftingSlot = false;
	public bool isWearableSlot = false;
	private InventarScript inventar;
	private AudioSource craftingcorrect;
	private AudioSource slotWrong;

	void Start() {
		inventar = GameObject.Find ("Inventory").GetComponent<InventarScript> ();
		if(slotWrong == null) 
			slotWrong = GameObject.Find("SlotWrong").audio;
		if(craftingcorrect == null)
			craftingcorrect = GameObject.Find("CraftingCorrect").audio;
	}

	public GameObject item {
		get {
			if(transform.childCount > 0) {
				return transform.GetChild (0).gameObject;
			}
			return null;
		}	
	}


	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		if (!item) {
			if(isCraftingSlot) {
				if(DragHandler.itemBeingDragged.GetComponent<ItemScript>().craftable) {
					DragHandler.itemBeingDragged.transform.SetParent (transform);
				} else {
					inventar.setInventarHinweis("Dieses Item ist kein Craftitem.");
					slotWrong.audio.Play();
				}
					
			} else if (isWearableSlot) {
				if(DragHandler.itemBeingDragged.GetComponent<ItemScript>().wearable && DragHandler.itemBeingDragged.GetComponent<ItemScript>().count > 0) {
					DragHandler.itemBeingDragged.transform.SetParent (transform);
					WeaponEquip(DragHandler.itemBeingDragged);
				} else {
					inventar.setInventarHinweis("Dieses Item ist nicht tragbar.");
					slotWrong.audio.Play();
				}

			} else {
				DragHandler.itemBeingDragged.transform.SetParent (transform);
			}

		}
	}
	#endregion

	private void WeaponEquip(GameObject theObject) {
		GameObject.Find ("Inventory").GetComponent<InventarScript> ().setHand (theObject);
		craftingcorrect.audio.Play ();
	}
}
