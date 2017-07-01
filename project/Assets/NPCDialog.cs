using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour 
{
	public TextAsset textFile;
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if(obj.tag == "Player")
		{
			GameObject.Find("DialogManager").SendMessage("AddDialogText", textFile);
		}
	}
}
