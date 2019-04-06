using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
	public  Vector3 ofset;
	public GameObject target;
	Tween moveTween;
    // Start is called before the first frame update
    void Start()
    {
		ofset = target.transform.position - transform.position ;
    }

    // Update is called once per frame
    void Update()
    {
		//moveTween.
		transform.DOMove(target.transform.position - ofset, 0.01f); 
    }
}
