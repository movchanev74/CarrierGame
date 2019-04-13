using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SavedObject : MonoBehaviour
{
	ObjectSaveData save;

	protected void Start()
    {
		if (save == null)
			save = new ObjectSaveData ();
		else
			save.startPosition = new float[3] {transform.position.x,transform.position.y, transform.position.z };
    }

	public ObjectSaveData getSaveData(){
		save.position = new float[3] {transform.position.x, transform.position.y, transform.position.z};
		save.rotation = new float[4] {transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w};
		save.typeObject = this.GetType().ToString();
		Debug.Log (this.GetType().ToString());
		return save;
	}

	public void setSaveData(ObjectSaveData oldData){
		if (save == null)
			save = new ObjectSaveData ();
		else
			save.startPosition = oldData.startPosition;
		transform.position = new Vector3 (oldData.position [0], oldData.position [1], oldData.position [2]);
		transform.rotation = new Quaternion (oldData.rotation [0], oldData.rotation [1], oldData.rotation [2], oldData.rotation [3]);
	}
}
