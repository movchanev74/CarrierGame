﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
	public GameObject PauseButtons;
	public float offset;
	public float offsetDuration;
	public GameObject Camera;

	PostProcessingProfile profile;
	Sequence seq;

	void Awake(){
		offset = PauseButtons.GetComponent<RectTransform> ().rect.width * 5 ;
		profile = Camera.GetComponent<PostProcessingBehaviour>().profile;
		HidePauseButtons (0);
	}

	void ShowPauseButtons(float offsetDuration){
		Tween show = null;
		profile.depthOfField.enabled = true;
		for (int i = 0; i < PauseButtons.transform.childCount; i++) {
			var button = PauseButtons.transform.GetChild (i);
			button.GetComponent<Button> ().interactable = true;
			show = button.DOMove (button.transform.position + Vector3.right*offset,offsetDuration);
		}
		show.OnComplete (delegate() {
			Time.timeScale = 0;
		});
	}

	void HidePauseButtons(float offsetDuration){
		profile.depthOfField.enabled = false;
		seq = DOTween.Sequence ();
		Time.timeScale = 1;
		for (int i = PauseButtons.transform.childCount-1; i >= 0; i--) {
		//for (int i = 0; i < PauseButtons.transform.childCount; i++) {
			var button = PauseButtons.transform.GetChild (i);
			button.GetComponent<Button> ().interactable = false;
			seq.Append (button.DOMove (button.transform.position - Vector3.right*offset,offsetDuration));
		}
	}


    public void Pause(){
		if(Time.timeScale == 1)
			ShowPauseButtons (offsetDuration);
    }
		
    public void Play(){
		
		HidePauseButtons (offsetDuration);
    }

	public void Save(){
	}

	public void Load(){
	}
	public void Exit(){
	}
}
