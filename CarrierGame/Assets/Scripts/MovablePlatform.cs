using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : ParentMovablePlatform
{
	void Start(){
		base.Start ();
		MovePlatform ();
	}
}
