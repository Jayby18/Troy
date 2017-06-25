using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour 
{
	private GameObject player;
	
	[SerializeField]
	private GameObject dialogHolder;
	private Text dialogText;
	
	public TextAsset textFile;
	public string[] textLines;
	
	public bool isActive;
	
	public int currentLine;
	public int endAtLine;
	
	void Start()
	{
		player = GameObject.FindWithTag("Player");
		dialogHolder = GameObject.Find("Dialog Holder");
		dialogText = GameObject.Find("Dialog Text").GetComponent<Text>();
		
		if(textFile != null)
		{
			textLines = (textFile.text.Split('\n'));
		}
		
		if(endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}
		
		if(isActive)
		{
			ShowDialogHolder();
		}
		else
		{
			HideDialogHolder();
		}
	}
		
	void SetupText()
	{
		dialogHolder = GameObject.Find("Dialog Holder");
		dialogText = GameObject.Find("Dialog Text").GetComponent<Text>();
		
		textLines = (textFile.text.Split('\n'));
		endAtLine = textLines.Length - 1;
	}
	
	void Update()
	{
		if(!isActive)
		{
			return;
		}
		
		if(currentLine > endAtLine)
		{
			HideDialogHolder();
		}
		
		if(dialogHolder.activeSelf == true)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				currentLine++;
			}
		}
		
		dialogText.text = textLines[currentLine];
	}
	
	public void ShowDialogHolder()
	{
		dialogHolder.SetActive(true);
		isActive = true;
	}
	
	public void HideDialogHolder()
	{
		dialogHolder.SetActive(false);
		isActive = false;
	}
	
	public void AddDialogText(TextAsset _textFile)
	{
		Debug.Log("Recieved text");
		textFile = _textFile;
		SetupText();
		ShowDialogHolder();
	}
	
	public int GetCurrentLine()
	{
		return currentLine;
	}
}
