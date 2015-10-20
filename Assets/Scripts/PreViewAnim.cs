using UnityEngine;
using System.Collections;

public class PreViewAnim : MonoBehaviour {
	Animator anim;
	static int endstate = Animator.StringToHash("Base.End");
	GameObject prevcam;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		prevcam = this.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (anim.GetCurrentAnimatorStateInfo (0).nameHash == endstate) {
			prevcam.SetActive(false);
			GameObject.Find("First Person Controller").GetComponent<pauseGameScript>().showTut();
		}
	}
}
