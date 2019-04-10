using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

abstract public class ParentMovablePlatform : MonoBehaviour
{
	protected GameObject pathMove;
	protected float durationMove;
	protected Vector4[] pathTimeRotation;
	protected float durationRotation;

	protected void RotatePlatform(){
		Sequence seq = DOTween.Sequence ();
		for (int i = 0; i < pathTimeRotation.Length; i++) {
			seq.Append (transform.DORotate (new Vector3 (pathTimeRotation[i].x, pathTimeRotation[i].y, pathTimeRotation[i].z), pathTimeRotation[i].w).SetEase (Ease.InOutCubic));
		}
		seq.Append (transform.DORotate (gameObject.transform.eulerAngles, durationRotation).SetEase (Ease.InOutCubic));
		seq.SetLoops (-1);

		//transform.DORotate (new Vector3 (0, 0, 0), durationRotation).SetLoops(-1);
		//seq.SetLoops (-1);
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Box" || col.gameObject.tag == "Player")
			col.gameObject.transform.SetParent(gameObject.transform);
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "Box" || col.gameObject.tag == "Player")
			col.gameObject.transform.SetParent(null);
	}

	protected void MovePlatform(){
		float sumDistantion = 0;
		Vector3[] path = new Vector3[pathMove.transform.childCount+1];
		float[] distantion = new float[pathMove.transform.childCount + 1];

		for (int i = 0; i < pathMove.transform.childCount ; i++) {
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
			timeParts.Add (distantion[i]/sumDistantion*durationMove);

		Sequence seq = DOTween.Sequence ();
		for (int i = 0; i < path.Length; i++) {
			seq.Append(transform.DOMove( path[i], timeParts[i]).SetEase (Ease.InOutCubic));
			Debug.Log (timeParts[i].ToString());
			Debug.Log (distantion[i].ToString());
		}
		seq.SetLoops (-1);
	}
}
