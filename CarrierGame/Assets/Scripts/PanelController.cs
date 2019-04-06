using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    // Start is called before the first frame update
	void Awake()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Box") 
			col.gameObject.GetComponent<IBox>().DestroyBox();
	}
}
