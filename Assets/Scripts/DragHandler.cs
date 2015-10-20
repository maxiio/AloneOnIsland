using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {
	public static GameObject itemBeingDragged;
	public Vector3 startPosition;
	public static Transform startParent;
	private InventarScript inventar;
	private AudioSource slotWrong;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	#endregion
	
	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{
		Vector3 mPos = Input.mousePosition;
		mPos.z = 150;
		transform.position = mPos;
	}
	
	#endregion
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent == startParent) {
			if(slotWrong == null) 
				slotWrong = GameObject.Find("SlotWrong").audio;

			slotWrong.audio.Play();
			transform.position = startPosition;
		}

	}
	
	#endregion

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		inventar = GameObject.Find ("Inventory").GetComponent<InventarScript> ();
		if (gameObject.GetComponent<ItemScript> ().usable) {
			if(gameObject.GetComponent<ItemScript>().count > 0) {
				inventar.setQuestion(gameObject);
			}
		}
	}

	#endregion
}
