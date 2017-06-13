using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerWeapon))]
public class PlayerCombat : MonoBehaviour 
{	
	public GameObject throwable;
	
	private GameObject weapon;
	private PlayerWeapon pw;
	
	void Start()
	{
		weapon = GameObject.FindWithTag("PlayerWeapon");
		pw = weapon.GetComponent<PlayerWeapon>();
	}
	
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
