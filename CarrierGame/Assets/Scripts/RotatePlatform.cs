﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : ParentMovablePlatform
{
	public Vector4[] pathTimeRotation;
	public float durationRotation;
	void Start(){
		RotatePlatform(pathTimeRotation,durationRotation);
	}
}