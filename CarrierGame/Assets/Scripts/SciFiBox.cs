using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class SciFiBox : SavedObject,IBox
{
	//Vector3 startPosition;

	//public Vector3 getStartPosition(){
	//	return startPosition;
	//}

	//void Start (){
		//startPosition = transform.position;
	//}

	float durationScale = 1;
	public void DestroyBox(){
		transform.DOScale (new Vector3 (0, 0, 0), durationScale).SetAutoKill (true);
	}
}
