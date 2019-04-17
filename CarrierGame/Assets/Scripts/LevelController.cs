using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
	public static UnityEvent OnRestartBoxesEvent = new UnityEvent();
	public float heightGameOver;
	public Text score;
	public Text restartTimer;
	public float maxTimeRestartBox;
	public float timerRestartBox = 0;

	private int maxScore;

	void Awake()
	{
		maxScore = GameObject.FindGameObjectsWithTag ("Box").Length;
		UpdateScore ();
	}

    void Start()
    {
		if (timerRestartBox == 0) 
			timerRestartBox = maxTimeRestartBox;
    }

	bool checkScene (string nameScene)
	{
		for (int s = 0; s < SceneManager.sceneCount; s++)
			if (SceneManager.GetSceneByBuildIndex(s).name == nameScene)
				return true;
		return false;
	}

	public void BoxDestroyed()
	{
		CheckEndGame ();
		UpdateScore ();
	}

	void CheckEndGame()
	{
		GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
		if(boxes.Length == 0)
		{
			string currentNameScene = SceneManager.GetActiveScene ().name;
			int numberScene = Convert.ToInt32 (currentNameScene.Substring (5)) + 1;
			Debug.Log (currentNameScene.Substring (5));
			string newScene = "Level" + numberScene;

			if(SceneManager.GetSceneByName(newScene).IsValid())
				SceneManager.LoadScene(newScene);
			else
				SceneManager.LoadScene("MainMenu");
		}
	}

	public void UpdateScore()
	{
		if (score){
			int newScore = maxScore - GameObject.FindGameObjectsWithTag ("Box").Length;	
			score.text = newScore + "/" + maxScore;
		}
	}

	void CheckGameOver()
	{
		GameObject player = GameObject.FindWithTag("Player");
		if (player.transform.position.y < heightGameOver) 
		{
			var position = player.GetComponent<Player>().getSaveData ().startPosition; 
			player.transform.rotation = new Quaternion ();
			player.transform.position = new Vector3 (position[0], position[1], position[2]);   
		}
	}

	void RestartBoxes()
	{
		timerRestartBox -= Time.deltaTime;
		if(restartTimer)
			restartTimer.text = ((int)(timerRestartBox/60)).ToString ("0")+":"+((int)(timerRestartBox%60)).ToString("00");
		if (timerRestartBox <= 0.0f)
		{
			OnRestartBoxesEvent.Invoke();
			timerRestartBox = maxTimeRestartBox;
		}
	}
		
    void Update()
    {
		RestartBoxes ();
		CheckGameOver ();
    }
}
