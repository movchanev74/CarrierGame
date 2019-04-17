using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelController : MonoBehaviour
{
	public UnityEvent BoxTrigerEvent;

	void OnTriggerStay(Collider collider)
	{
		if (collider.gameObject.tag == "Box" && collider.gameObject.transform.parent == null ) 
		{
			collider.gameObject.tag = "Untagged";
			collider.gameObject.GetComponent<IBox> ().DestroyBox ();
			BoxTrigerEvent.Invoke ();
		}
	}
}