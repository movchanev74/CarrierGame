using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
	public float speedMove;
	public float speedRotate;
	public GameObject curBox;
	public GameObject boxInHands;
	Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.centerOfMass = new Vector3(0,0,0);
    }

	void TakeBox(){
		float rotationY = transform.localEulerAngles.y*Mathf.Deg2Rad;
		boxInHands = curBox;

		Sequence seq = DOTween.Sequence ();
		seq.Append (boxInHands.transform.DORotate(transform.localEulerAngles,0.5f));
		var newPosition = gameObject.transform.position + new Vector3 (Mathf.Sin(rotationY), 0, Mathf.Cos(rotationY))*1;
		seq.Join (boxInHands.transform.DOMove (newPosition,0.5f));
		newPosition = gameObject.transform.position + Vector3.up*1.9f;
		seq.Append (boxInHands.transform.DOMove(newPosition,1.0f));

		boxInHands.transform.SetParent (gameObject.transform);
		boxInHands.GetComponent<Rigidbody> ().isKinematic = true;
	}

	void BreakBox(){
		float rotationY = transform.localEulerAngles.y*Mathf.Deg2Rad;
		Sequence seq = DOTween.Sequence ();

		var newPosition = gameObject.transform.position + new Vector3 (Mathf.Sin(rotationY), 0, Mathf.Cos(rotationY))*2;
		seq.Append (boxInHands.transform.DOMove(newPosition,1.0f));

		boxInHands.transform.SetParent (null);
		boxInHands.GetComponent<Rigidbody> ().isKinematic = false;
		boxInHands = null;
	}

	void KeyboardUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		float rotationY = transform.localEulerAngles.y*Mathf.Deg2Rad;
		Vector3 force = new Vector3 (Mathf.Sin(rotationY), 0, Mathf.Cos(rotationY))*vertical*speedMove*Time.deltaTime;  
		transform.GetComponent<Rigidbody> ().AddForceAtPosition(force,transform.position+0.2f*Vector3.down,ForceMode.Acceleration);
		rb.
		transform.Rotate(new Vector3(0,horizontal*speedRotate*Time.deltaTime,0),Space.Self);

		if (Input.GetKeyDown (KeyCode.Space) && boxInHands)
			BreakBox ();
		else if(Input.GetKeyDown (KeyCode.Space) && curBox && !boxInHands)
			TakeBox();
	}

    void Update()
    {
		KeyboardUpdate ();
    }

	void OnCollisionStay(Collision col){
		if (col.gameObject.tag == "Box") 
			curBox = col.gameObject;
	}
	void OnCollisionExit(Collision col){
		if (col.gameObject.tag == "Box") 
			curBox = null;
	}
}
