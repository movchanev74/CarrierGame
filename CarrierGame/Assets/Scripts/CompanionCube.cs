﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompanionCube : AbstractBox,IBox
{
	public void DestroyBox()
	{
		Destroy (gameObject);
	}
}
