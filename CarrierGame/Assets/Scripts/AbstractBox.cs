using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractBox : SavedObject
{
	void Start()
	{
		base.Start ();
		LevelController.OnRestartBoxesEvent.AddListener(RestartBox);
	}

    void RestartBox()
	{
		if (transform.parent == null || transform.parent.tag != "BodyPlayer") 
		{
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 ();
			transform.rotation = new Quaternion ();
			transform.position = new Vector3 (getSaveData ().startPosition [0], getSaveData ().startPosition [1], getSaveData ().startPosition [2]);     	   
		}
	}
}
