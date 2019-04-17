using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
	public GameObject pauseButtons;
	public float offset;
	public float offsetDuration;

	private PostProcessingProfile profile;
	private Sequence sequence;
	private SaveManager saveManager;

	void Awake()
	{
		saveManager = GameObject.Find ("SaveManager").GetComponent<SaveManager> ();
		offset = pauseButtons.GetComponent<RectTransform> ().rect.width * 5 ;
		profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
		HidePauseButtons (0);
	}

	void ShowPauseButtons(float offsetDuration)
	{
		Tween show = null;
		profile.depthOfField.enabled = true;
		for (int i = 0; i < pauseButtons.transform.childCount; i++) 
		{
			Transform button = pauseButtons.transform.GetChild (i);
			button.GetComponent<Button> ().interactable = true;
			show = button.DOMove (button.transform.position + Vector3.right*Mathf.Abs(offset),offsetDuration);
		}
		show.OnComplete (delegate() {
			Time.timeScale = 0;
		});
	}

	void HidePauseButtons(float offsetDuration)
	{
		profile.depthOfField.enabled = false;
		sequence = DOTween.Sequence ();
		Time.timeScale = 1;
		for (int i = pauseButtons.transform.childCount-1; i >= 0; i--)
		{
			var button = pauseButtons.transform.GetChild (i);
			button.GetComponent<Button> ().interactable = false;
			sequence.Append (button.DOMove (button.transform.position - Vector3.right*Mathf.Abs(offset),offsetDuration));
		}
	}


    public void Pause()
	{
		if(Time.timeScale == 1)
			ShowPauseButtons (offsetDuration);
    }
		
    public void Play()
	{
		HidePauseButtons (offsetDuration);
    }

	public void Save()
	{
		saveManager.Save ();
		Play ();
	}

	public void Load()
	{
		saveManager.Load ();
	}

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void Exit()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
