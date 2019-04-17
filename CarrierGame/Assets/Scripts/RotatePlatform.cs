using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatePlatform : ParentMovablePlatform
{
	public Vector4[] pathTimeRotation;
	public float durationRotation;

	void Start()
	{
		Rotate();
	}

	void Rotate()
	{
		Sequence sequence = DOTween.Sequence ();
		for (int i = 0; i < pathTimeRotation.Length; i++) 
		{
			Vector3 newRotation = new Vector3 (pathTimeRotation [i].x, pathTimeRotation [i].y, pathTimeRotation [i].z);
			sequence.Append (transform.DORotate (newRotation, pathTimeRotation[i].w).SetEase (Ease.InOutCubic));
		}
		sequence.Append (transform.DORotate (gameObject.transform.eulerAngles, durationRotation).SetEase (Ease.InOutCubic));
		sequence.SetLoops (-1);
	}
}
