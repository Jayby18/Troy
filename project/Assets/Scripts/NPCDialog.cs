using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour 
{
	public TextAsset textFile;
	[SerializeField] private bool instantActivation;
	
	void Start()
	{
		if(instantActivation)
		{
			ActivateDialog(textFile);
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if(obj.tag == "Player")
		{
			ActivateDialog(textFile);
		}
	}
	
	public void ActivateDialog(TextAsset _textFile)	{	GameObject.Find("DialogManager").SendMessage("AddDialogText", _textFile);	}
}
