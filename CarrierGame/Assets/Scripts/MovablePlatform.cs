using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovablePlatform : ParentMovablePlatform
{
	public GameObject pathMove;
	public float durationMove;

	void Start()
	{
		Move ();
	}

	void Move()
	{
		float sumDistantion = 0;
		Vector3[] path = new Vector3[pathMove.transform.childCount+1];
		float[] distantion = new float[pathMove.transform.childCount + 1];

		for (int i = 0; i < pathMove.transform.childCount ; i++) 
		{
			if (i == pathMove.transform.childCount - 1)
				distantion[i] = Vector3.Distance (gameObject.transform.position, pathMove.transform.GetChild (i).position);
			else 
				distantion[i] = Vector3.Distance (pathMove.transform.GetChild (i + 1).position, pathMove.transform.GetChild (i).position);
			path [i] = pathMove.transform.GetChild (i).position;
			sumDistantion += distantion [i];
		}

		path [path.Length-1] = gameObject.transform.position;
		distantion[distantion.Length-1] = Vector3.Distance (pathMove.transform.GetChild (0).position, gameObject.transform.position);
		sumDistantion += distantion [distantion.Length - 1];

		List<float> timeParts = new List<float>();

		for (int i = 0; i < path.Length; i++) 
		{
			timeParts.Add (distantion [i] / sumDistantion * durationMove);
		}

		Sequence seq = DOTween.Sequence ();
		for (int i = 0; i < path.Length; i++) 
		{
			seq.Append(transform.DOMove( path[i], timeParts[i]).SetEase (Ease.InOutCubic));
		}
		seq.SetLoops (-1);
	}
}
