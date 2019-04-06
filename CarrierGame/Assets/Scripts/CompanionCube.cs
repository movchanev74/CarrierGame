using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCube : MonoBehaviour,IBox
{
	public void DestroyBox(){
		Destroy (gameObject);
	}
}
