﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class OfficeBox : SavedObject,IBox
{
	float durationScale = 1;
	public void DestroyBox(){
		//transform.GetComponent<Animator> ().Play ("BoxDestroy");

		//transform.DOScale (new Vector3 (0, 0, 0), durationScale).SetAutoKill (true);
		//Invoke(delegate() { Destroy(gameObject);},transform.GetComponent<Animator> ().);
	} 
	void DestroyMe()
	{
		Destroy(gameObject);
	}

}
