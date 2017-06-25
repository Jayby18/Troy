using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour 
{
	private Player player;
	
	void Start()
	{
		player = GetComponentInParent<Player>();
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if(obj.tag == "KillZone")
		{
			player.ApplyDamage(1);
		}
		else if(obj.tag == "ThrowablePickup")
		{
			player.pc.currentAmmo++;
		}
	}
	
	void OnCollisionStay2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if(obj.layer == 8)
		{
			player.isGrounded = true;
		}
	}
	
	void OnCollisionExit2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if(obj.layer == 8)
		{
			player.isGrounded = false;
		}
	}
}
