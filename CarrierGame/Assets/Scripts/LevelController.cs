using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
	Scene[] allScenes;
	GameObject player;
	public float heightGameOver;
	public Text score;
	int maxScore;

	void Awake(){
		maxScore = GameObject.FindGameObjectsWithTag ("Box").Length;
		UpdateScore ();
	}

    void Start()
    {
		allScenes = SceneManager.GetAllScenes();
		player = GameObject.Find("Player");


    }

	bool checkScene (string nameScene){
		for (int s = 0; s < allScenes.Length; s++) //get total number of scenes in build
			if (allScenes [s].buildIndex != -1 && allScenes [s].name == nameScene)
				return true;
		return false;
	}

	public void CheckEndGame(){
		var boxes = GameObject.FindGameObjectsWithTag("Box");
		if(boxes.Length == 0){
			var currentNameScene = SceneManager.GetActiveScene ().name;
			int numberScene = Convert.ToInt32 (currentNameScene.Substring (5)) + 1;
			string newScene = "level" + numberScene;

			if(checkScene(newScene))
				SceneManager.LoadScene(newScene);
			else
				SceneManager.LoadScene("MainMenu");
		}
	}

	public void UpdateScore(){
		if (score){
			int newScore = maxScore - GameObject.FindGameObjectsWithTag ("Box").Length;	
			score.text = newScore + "/" + maxScore;
		}
	}

	void CheckGameOver(){
		if(player.transform.position.y < heightGameOver)
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}




    // Update is called once per frame
    void Update()
    {
		CheckGameOver ();
    }
}
