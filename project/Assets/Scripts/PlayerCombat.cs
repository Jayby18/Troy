using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour 
{	
	public GameObject throwable;
	
	public void MeleeAttack()
	{
		Debug.Log("Melee Attacking...");
	}
	
	public void Throw()
	{
		Debug.Log("Throwing Object...");
		GameObject throwedObject = GameObject.Instantiate(throwable);
	}
}
