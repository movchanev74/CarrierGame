using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

abstract public class ParentMovablePlatform : MonoBehaviour
{
	public Vector3[] pathMove;
	public float durationMove;
	public float durationRotation;
	TweenParams LoopParams;
	protected void Start(){
		LoopParams = new TweenParams ().SetLoops (-1).SetEase(Ease.Linear);
	}

	protected void RotatePlatform(){
		transform.DORotate (new Vector3 (0, 0, 0), durationRotation).SetLoops(-1);
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
		Sequence seq = DOTween.Sequence ();
		foreach (var item in pathMove) {
			seq.Append(transform.DOMove(item,durationMove).SetEase (Ease.InOutCubic));
			//seq.Append (transform.DOPath (pathMove, durationMove, PathType.Linear).SetEase (Ease.InOutCirc));
		}
		//seq.AppendInterval (1.0f);
		seq.SetLoops (-1);
	}
}
