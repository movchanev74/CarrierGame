using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SciFiBox : MonoBehaviour,IBox
{
	float durationScale = 1;
	public void DestroyBox(){
		transform.DOScale (new Vector3 (0, 0, 0), durationScale).SetAutoKill (true);
	}
}
