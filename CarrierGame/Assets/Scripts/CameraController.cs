using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
	public Vector3 ofset;
	public GameObject target;
	public float speed;
	public float maxDistantion; 

	private Tween moveTween;

    void Update()
    {
		float distantion = (transform.position - target.transform.position - ofset).magnitude;
		if (distantion > maxDistantion)
			transform.position = target.transform.position - ofset;
		transform.DOMove(target.transform.position - ofset, distantion/speed); 
    }
}
