using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueActivator : MonoBehaviour 
{
	public TextAsset prologue;
	
	private GameObject dialogMan;
	
	void Start()
	{
		dialogMan = GameObject.Find("DialogManager");
		dialogMan.GetComponent<DialogManager>().AddDialogText(prologue);
	}
	
	void Update()
	{
		int _currentLine = dialogMan.GetComponent<DialogManager>().GetCurrentLine();
		
		if(_currentLine >= 4)
		{
			SceneManager.LoadScene("main menu");
		}
	}
}
