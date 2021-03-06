﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class OfficeBox : AbstractBox,IBox
{
	public float durationDestroy = 1; 

	public void DestroyBox()
	{
		transform.DOScale (new Vector3 (0, 0, 0), durationDestroy).OnComplete (delegate 
		{
			Destroy(gameObject);
		});
	} 
	void DestroyMe()
	{
		Destroy(gameObject);
	}

}
