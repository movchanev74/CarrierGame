using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Player : SavedObject
{
	public float speedMove;
	public float speedRotate;
	public float maxSpeed;

	//ObjectSaveData playerSave;

	GameObject curBox;
	GameObject boxInHands;
	Rigidbody rb;
	bool isGroundCollision;
	bool isTakingBox = false;

	Animator playerAnimator;
	bool isBackMove = false;
    
    void Start()
    {
		base.Start();
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.centerOfMass = new Vector3(0,0,0);
		playerAnimator = gameObject.GetComponent<Animator> ();
    }

	void TakeBox(){
		playerAnimator.SetBool ("IsBoxInHands",true);
		isTakingBox = true;
		float rotationY = transform.localEulerAngles.y*Mathf.Deg2Rad;
		boxInHands = curBox;

		var boxRigidBody = boxInHands.GetComponent<Rigidbody> ();
		boxRigidBody.isKinematic = true;
		boxRigidBody.useGravity = false;

		//0,0.3,1
		//0 2.3 0 
		Sequence seq = DOTween.Sequence ();
		//float posy = 
		//	(boxInHands.transform.localEulerAngles.y+360)%360 + ((transform.localEulerAngles.y+360)%360 - (boxInHands.transform.localEulerAngles.y+360)%360)%90;
		//var newRotaton = new Vector3 (transform.localEulerAngles.x, posy, transform.localEulerAngles.z);

		float posy = (boxInHands.transform.eulerAngles.y+360)%360 + ((transform.eulerAngles.y+360)%360 - (boxInHands.transform.eulerAngles.y+360)%360)%90;
		var newRotaton = new Vector3 (transform.eulerAngles.x, posy, transform.eulerAngles.z);

		//boxInHands.transform.position = gameObject.transform.localPosition + Vector3.up * 0.4f + Vector3.forward*0.9f;
		boxInHands.transform.position = gameObject.transform.position + (new Vector3 (0, Mathf.Sin(rotationY)* 1.4f, Mathf.Cos(rotationY)*0.9f ));
		//boxInHands.transform.localPosition = gameObject.transform.position + Vector3.up * 0.4f + Vector3.forward*0.9f;

		isTakingBox = false;
		boxInHands.transform.SetParent (gameObject.transform);
		//seq.Append (boxInHands.transform.DORotate(newRotaton,0.5f));
		//var newPosition = gameObject.transform.position + new Vector3 (Mathf.Sin(rotationY), 0, Mathf.Cos(rotationY))*1;
		//newPosition = gameObject.transform.position + Vector3.up*0.4f + Vector3.forward*1.0f;
		//seq.Join (boxInHands.transform.DOMove (newPosition,0.2f));
		//var newPosition = gameObject.transform.position + Vector3.up*0.4f + Vector3.forward*1.0f;
		//seq.Append (boxInHands.transform.DOMove(newPosition,1));
		//newPosition = gameObject.transform.position + Vector3.up*1.0f + Vector3.forward*1.0f;
		//seq.Append (boxInHands.transform.DOMove(newPosition,1));
		//seq.OnComplete (delegate {

		//});
		//seq.Join (boxInHands.transform.DORotate (boxInHands.transform.eulerAngles - new Vector3(45,0,0),2));
		//newPosition = gameObject.transform.position + Vector3.up*1.3f;
		//seq.Append (boxInHands.transform.DOMove(newPosition,1));
		//seq.Join (boxInHands.transform.DORotate (boxInHands.transform.eulerAngles - new Vector3(90,0,0),2));


	}

	void BreakBox(){
		playerAnimator.SetBool ("IsBoxInHands", false);

		float rotationY = transform.localEulerAngles.y*Mathf.Deg2Rad;
		Sequence seq = DOTween.Sequence ();

		var newPosition = gameObject.transform.position + new Vector3 (Mathf.Sin(rotationY), 0, Mathf.Cos(rotationY))*1.0f;
		newPosition = gameObject.transform.position + Vector3.up*0.4f + Vector3.forward*1.0f;
		//seq.Append (boxInHands.transform.DOMove(newPosition,1.0f));

		boxInHands.transform.SetParent (null);
		var boxRigidBody = boxInHands.GetComponent<Rigidbody> ();
		boxRigidBody.isKinematic = false;
		boxRigidBody.useGravity = true;
		boxInHands = null;
	}

	void KeyboardUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		float rotationY = transform.eulerAngles.y*Mathf.Deg2Rad;

		if (Input.GetKeyUp(KeyCode.DownArrow) ) {
			Debug.Log ("up");
			isBackMove = false;
			playerAnimator.SetBool ("IsBack",false);
			//playerAnimator.Play ("ForwardRotatePlayerAnimation");
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Debug.Log ("down");
			isBackMove = true;
			playerAnimator.SetBool ("IsBack",true);
			//playerAnimator.Play ("BackRotatePlayerAnimation");
		}






		if (Mathf.Abs (vertical) > 0)
			playerAnimator.SetBool ("IsMove",true);
		else
			playerAnimator.SetBool ("IsMove",false);
		//	playerAnimator.Play ("BackRotatePlayerAnimation");
		//if ( vertical > 0 )
		//	if(isTakingBox)
		//		playerAnimator.Play ("MoveWithBoxPlayerAnimation");
		//	else
		//		playerAnimator.Play ("MovePlayerAnimation");
		//if ( vertical == 0 )
			//playerAnimator.Play ("StayPlayerAnimation");
		Vector3 force = new Vector3 (Mathf.Sin(rotationY), 0, Mathf.Cos(rotationY))*vertical*speedMove*Time.deltaTime;
		if(isGroundCollision && !isTakingBox && rb.velocity.magnitude < maxSpeed)
			transform.GetComponent<Rigidbody> ().AddForceAtPosition(force,transform.position+0.2f*Vector3.down,ForceMode.Acceleration);
		if(!isTakingBox)
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
		else
			isGroundCollision = true;		
	}
	void OnCollisionExit(Collision col){
		if (col.gameObject.tag == "Box")
			curBox = null;
		else {
			//rb.velocity = new Vector3 (0,0,0);
			isGroundCollision = false;
		}
	}
}
