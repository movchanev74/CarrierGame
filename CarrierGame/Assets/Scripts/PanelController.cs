using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelController : MonoBehaviour
{
	public UnityEvent BoxTrigerEvent;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Box") {
			col.gameObject.tag = "Untagged";
			col.gameObject.GetComponent<IBox> ().DestroyBox ();
			BoxTrigerEvent.Invoke ();
		}
	}
}