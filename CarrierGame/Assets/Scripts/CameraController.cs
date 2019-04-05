using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public  Vector3 ofset;
	public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
		ofset = target.transform.position - transform.position ;
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = target.transform.position - ofset;
    }
}
