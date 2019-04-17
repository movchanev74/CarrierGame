using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class SciFiBox : AbstractBox,IBox
{
	public float durationDestroy = 1; 
	
	void Start()
	{
		base.Start ();
	}

	public void DestroyBox()
	{
		var boxRigidBody = transform.GetComponent<Rigidbody> ();
		boxRigidBody.isKinematic = true;
		boxRigidBody.useGravity = false;
		transform.DOMove(transform.position + Vector3.down*1.5f, durationDestroy).OnComplete (delegate {
			Destroy(gameObject);
		});
	}
}
