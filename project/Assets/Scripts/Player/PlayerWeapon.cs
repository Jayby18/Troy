using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerWeapon : MonoBehaviour 
{
	public float weaponDamage = 10f;
	
	private GameObject player;
	
	void Start()
	{
		player = GameObject.FindWithTag("Player");
		Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
	
	void Update()
	{
		float xaxis = player.transform.position.x;
		float yaxis = player.transform.position.y;
		transform.position = new Vector2(xaxis + 0.35f, yaxis + 0.03f);
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		
		if(obj.tag == "Enemy")
		{
			Debug.Log("Hit enemy!");
			obj.SendMessage("ApplyDamage", weaponDamage);
		}
	}
}
