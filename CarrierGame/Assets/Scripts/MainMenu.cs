using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void LoadSave()
	{
		SaveManager saveManager = GameObject.Find ("SaveManager").GetComponent<SaveManager> ();
		saveManager.Load ();
	}

	public void loadLevel(int numberLevel)
	{
		SceneManager.LoadScene ("level" + numberLevel);
	}

	public void exit()
	{
		Application.Quit ();
	}
}
