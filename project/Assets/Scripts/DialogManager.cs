using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour 
{
	private GameObject player;
	
	[SerializeField]
	private GameObject dialogHolder;
	private GameObject dialogTextObject;
	private Text dialogText;
	
	public TextAsset textFile;
	public string[] textLines;
	
	public bool isActive;
	
	public int currentLine;
	public int endAtLine;
	
	void Start()
	{
		dialogHolder = GameObject.Find("Dialog Holder");
		dialogTextObject = GameObject.Find("Dialog Text");
		dialogText = dialogTextObject.GetComponent<Text>();
		
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
			textFile = null;
			textLines = null;
			endAtLine = 0;
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
	
	public bool GetActiveSelf()
	{
		return isActive;
	}
}
