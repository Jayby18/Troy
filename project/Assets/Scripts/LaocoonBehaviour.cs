using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaocoonBehaviour : MonoBehaviour 
{
	private int countMax = 3;
	private int count;
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			count++;
		}
		
		if(count >= countMax)
		{
			Scene currentScene = SceneManager.GetActiveScene();
			int n = currentScene.buildIndex;
			n++;
			SceneManager.LoadScene(n);
		}
	}
}
