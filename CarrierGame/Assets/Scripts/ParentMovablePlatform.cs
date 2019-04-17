using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ParentMovablePlatform : MonoBehaviour
{

	void OnTriggerStay(Collider collider)
	{
		if((collider.gameObject.tag == "Box" && collider.gameObject.transform.parent == null) || collider.gameObject.tag == "Player")
			collider.gameObject.transform.SetParent(gameObject.transform);
	}

	void OnTriggerExit(Collider collider)
	{
		if((collider.gameObject.tag == "Box" && collider.gameObject.transform.parent == gameObject.transform) || collider.gameObject.tag == "Player")
			collider.gameObject.transform.SetParent(null);
	}
}
