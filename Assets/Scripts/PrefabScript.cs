using UnityEngine;
using System.Collections;

public class PrefabScript : MonoBehaviour {
	
	public GameObject BoxPrefab;
	
	//
	void Awake(){
		GameState.SetPrefabScript (this);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}