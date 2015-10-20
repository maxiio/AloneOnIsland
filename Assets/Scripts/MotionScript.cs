using UnityEngine;
using System.Collections;

public class MotionScript : MonoBehaviour {
	public AudioClip axeSound;
	public AudioClip punshSound;
	Animator anim;
	bool left = false;
	playerController player;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		player = GetComponentInParent<playerController>();
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis ("Vertical");
		anim.SetFloat("Speed", move);

		if (Input.GetMouseButtonDown (0)) {
			if(player.equiped) {
				anim.SetTrigger("SwingAxe");
				audio.PlayOneShot(axeSound);			
			} else {
				audio.PlayOneShot(punshSound);
				anim.SetTrigger("Punsh");
			}
			anim.SetBool("Left", left);
			left = !left;
		}
	}
}
