using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour 
{
	float weaponDamage;
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		
		if(obj.tag == "Enemy")
		{
			obj.SendMessage("ApplyDamage", weaponDamage);
		}
	}
}
