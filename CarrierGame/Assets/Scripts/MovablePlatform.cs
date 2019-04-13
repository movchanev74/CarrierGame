using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : ParentMovablePlatform
{
	public GameObject pathMove;
	public float durationMove;
	void Start(){
		MovePlatform (pathMove,durationMove);
	}
}
