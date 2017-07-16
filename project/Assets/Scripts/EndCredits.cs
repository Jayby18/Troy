using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredits : MonoBehaviour 
{
	float fadeTime = 0f;
	float count = 0f;
	
	GameObject endScreen;
	
	void Start()
	{
		endScreen = GameObject.Find("EndScreen");
		endScreen.SetActive(false);
	}
	
	void Update()
	{
		if(fadeTime != 0f)
		{
			count += Time.deltaTime;
			if(count >= fadeTime)
			{
				ShowEndCredits();
			}
		}
	}
	
	public void EndLevel()
	{
		fadeTime = GetComponent<FadeOut>().BeginFade(1);
	}
	
	public void ShowEndCredits()
	{
		endScreen.SetActive(true);
		
		float i = 0f;
		while(true)
		{
			i += Time.deltaTime;
			if(i >= 3f)
			{
				break;
			}
		}
		
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
