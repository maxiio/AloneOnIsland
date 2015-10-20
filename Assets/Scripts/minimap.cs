using UnityEngine;
using System.Collections;

public class minimap : MonoBehaviour {

	public Transform Target;

	void LateUpdate () {
		transform.position = new Vector3 (Target.position.x, transform.position.y, Target.position.z);
		float yang = Target.eulerAngles.y;
		transform.eulerAngles = new Vector3 (90, yang, 0);	}
}
