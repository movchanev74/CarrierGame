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

	private GameObject curBox;
	private GameObject boxInHands;
	private Rigidbody rigidbody;
	private bool isGroundCollision;
	private bool isTakingBox = false;
	private Animator playerAnimator;
	private bool isBackMove = false;
    
    void Start()
    {
		base.Start();
		rigidbody = gameObject.GetComponent<Rigidbody> ();
		rigidbody.centerOfMass = new Vector3();
		playerAnimator = gameObject.GetComponent<Animator> ();
    }

	void TakeBox()
	{
		playerAnimator.SetBool ("IsBoxInHands",true);
		isTakingBox = true;
		boxInHands = curBox;
		float rotationY = transform.localEulerAngles.y*Mathf.Deg2Rad;

		Rigidbody boxRigidBody = boxInHands.GetComponent<Rigidbody> ();
		boxRigidBody.isKinematic = true;
		boxRigidBody.useGravity = false;

		Sequence seq = DOTween.Sequence ();
		float posy = (boxInHands.transform.localEulerAngles.y+360)%360 + ((transform.localEulerAngles.y+360)%360 - (boxInHands.transform.localEulerAngles.y+360)%360)%90;

		seq.Append (boxInHands.transform.DORotate(new Vector3 (transform.localEulerAngles.x, posy, transform.localEulerAngles.z),0.5f));
		seq.Join (boxInHands.transform.DOMove (gameObject.transform.position + new Vector3 (Mathf.Sin(rotationY), 0.4f, Mathf.Cos(rotationY)),0.2f));
		seq.OnComplete (delegate {
			isTakingBox = false;
		});
		
		boxInHands.transform.SetParent (gameObject.transform.GetChild(0));
	}

	void BreakBox()
	{
		playerAnimator.SetBool ("IsBoxInHands", false);
		boxInHands.transform.SetParent (null);
		var boxRigidBody = boxInHands.GetComponent<Rigidbody> ();
		boxRigidBody.isKinematic = false;
		boxRigidBody.useGravity = true;
		boxInHands = null;
	}

	void KeyboardUpdate()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		float rotationY = transform.eulerAngles.y*Mathf.Deg2Rad;

		if (Input.GetKeyUp(KeyCode.DownArrow) ) 
		{
			isBackMove = false;
			playerAnimator.SetBool ("IsBack",false);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			isBackMove = true;
			playerAnimator.SetBool ("IsBack",true);
		}

		if (Mathf.Abs (vertical) > 0)
			playerAnimator.SetBool ("IsMove",true);
		else
			playerAnimator.SetBool ("IsMove",false);
		
		Vector3 force = new Vector3 (Mathf.Sin(rotationY), 0, Mathf.Cos(rotationY))*vertical*speedMove*Time.deltaTime;

		if(isGroundCollision && !isTakingBox && rigidbody.velocity.magnitude < maxSpeed)
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

	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "Box") {
			curBox = collision.gameObject;
		} else {
			isGroundCollision = true;		
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Box") {
			curBox = null;
		}
		else {
			isGroundCollision = false;
		}
	}
}
