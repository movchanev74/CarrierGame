using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
	public GameObject sciFiBoxPrefab;
	public GameObject officeBoxPrefab;
	public GameObject companionBoxPrefab;

	private SaveData saveData;
	static string savePath;
	bool loadedSave = false;
	static SaveManager instance;

	public void Start(){
		if (instance == null) { 
			instance = this; 
		} else if(instance == this){ 
			Destroy(gameObject); 
		}

		SaveManager.savePath = Path.Combine(Application.persistentDataPath, "save.dat");
		this.saveData = new SaveData();
		this.loadDataFromDisk();
		SceneManager.sceneLoaded += this.OnLevelFinishedLoading ;
		DontDestroyOnLoad (gameObject);
	}


	public void Save()
	{
		var save = new SaveData ();
		save.level = SceneManager.GetActiveScene().name;
		save.listObjects.Add (GameObject.FindWithTag ("Player").GetComponent<Player> ().getSaveData ());

		foreach (GameObject box in GameObject.FindGameObjectsWithTag("Box")) {
			save.listObjects.Add (box.GetComponent<SavedObject> ().getSaveData ());
		}
		saveDataToDisk (save);
	}

	public void Load()
	{
		if (SceneManager.GetSceneByName (loadDataFromDisk ().level) != null) {
			loadedSave = true;
			SceneManager.LoadScene (loadDataFromDisk ().level);
		}
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
		if(loadedSave){
			loadedSave = false;
			foreach (GameObject box in GameObject.FindGameObjectsWithTag("Box")) {
				box.tag = "Untagged";
				Destroy (box);
			}
			foreach (ObjectSaveData data in loadDataFromDisk().listObjects) {
				GameObject newBox = null;
				switch (data.typeObject) {
				case "Player":
					GameObject.Find ("Player").GetComponent<Player> ().setSaveData (data);
					break;
				case "SciFiBox":
					GameObject.Instantiate (sciFiBoxPrefab).GetComponent<SavedObject> ().setSaveData (data);
					break;
				case "OfficeBox":
					GameObject.Instantiate (officeBoxPrefab).GetComponent<SavedObject> ().setSaveData (data);
					break;
				case "CompanionCube":
					GameObject.Instantiate (companionBoxPrefab).GetComponent<SavedObject> ().setSaveData (data);
					break;
				}
				/*if (newBox) {
					newBox.GetComponent<SavedObject> ().setSaveData (data);
					newBox.transform.SetParent (null);
					newBox.transform.position = new Vector3 (data.position [0], data.position [1], data.position [2]);
					newBox.transform.rotation = new Quaternion (data.rotation[0], data.rotation[1], data.rotation[2], data.rotation[3]);
				}*/
			}
			GameObject.FindWithTag ("Level").GetComponent<LevelController> ().UpdateScore ();
		}

	}

	public void saveDataToDisk(SaveData saveData)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(savePath);
		bf.Serialize(file, saveData);
		file.Close();
	}
		
	public SaveData loadDataFromDisk()
	{
		if(File.Exists(savePath))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(savePath, FileMode.Open);
			this.saveData = (SaveData)bf.Deserialize(file);
			file.Close();
		}
		return this.saveData;
	}
}

[System.Serializable]
public class SaveData
{
	public string level;
	public int score;
	public List<ObjectSaveData> listObjects;

	public SaveData(){
		listObjects = new List<ObjectSaveData> ();
	}
}

[System.Serializable]
public class ObjectSaveData
{
	public float[] position;
	public float[] startPosition;
	public float[] rotation;
	public string typeObject;
}