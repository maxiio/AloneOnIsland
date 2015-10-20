using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour {
	public int treeHealth = 5;
	public GameObject tree;
	public int speed = 1;
	private bool canDie = false;
	private bool falling = false;
	private float falltime;
	private Vector3 pos;
	private GameObject log;
	private GameObject coco;
	private int logs = 0;
	private int coconut = 0;

	// Use this for initialization
	void Start () {
		tree = this.gameObject;
		rigidbody.isKinematic = true;
		log = GameObject.Find("LogBig");
		coco = GameObject.Find("Coconut");
		if (this.name == "PalmCollider 2" || this.name == "PalmCollider") {
			speed = 1;
			rigidbody.mass = 150;
			treeHealth = Random.Range (8, 15);
			logs = Random.Range(0, 3);
			coconut = 0;
		}
		if (this.name == "PalmTree_single_2sided" || this.name == "PalmTree_single-bended_2sided") {
			speed = 1;
			rigidbody.mass = 250;
			treeHealth = Random.Range (15, 20);
			logs = Random.Range(1, 4);
			coconut = Random.Range(0, 2);
		}
		if (this.name == "PalmTree_dual-bended_2sided" || this.name == "PalmTree_dual_ 2sided") {
			speed = 1;
			rigidbody.mass = 450;
			treeHealth = Random.Range (20, 30);
			logs = Random.Range(2, 6);
			coconut = Random.Range(0, 3);
		}
		if (this.name == "PalmTree_trio_1sided" || this.name == "PalmTree_trio_2sided") {
			speed = 1;
			rigidbody.mass = 450;
			treeHealth = Random.Range (25, 40);
			logs = Random.Range(2, 6);
			coconut = Random.Range(1, 4);
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (treeHealth <= 0) {
			rigidbody.isKinematic = false;
			rigidbody.AddForce(transform.forward * speed);
			isfalling();
			DestroyTree();
		}
	}

	void isfalling () {
		if (!falling) {
			falltime = Time.deltaTime;
			falling = true;
		} else {
			falltime -= Time.deltaTime;		
		}
	}

	void dropItems () {
		pos = this.transform.position;
		for (int i = 0; i < logs; i++) {
			Instantiate (log, pos, Quaternion.identity);
		}
		for (int i = 0; i < coconut; i++) {
			Instantiate (coco, pos, Quaternion.identity);
		}
		
	}
	
	void DestroyTree () {
		if (falltime <= -5) {
			canDie = true;
		}
		if (canDie) {
			dropItems();
			Destroy(tree);
		}
			
	}
	     

}
