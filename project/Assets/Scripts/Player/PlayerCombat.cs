using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{	
	public GameObject throwable;
	
	public GameObject player;
	
	private GameObject weapon;
	private PlayerWeapon pw;
	private Animator anim;
	
	public float fireRate;
	public float currentFireRate;
	public bool canThrow = false;
	public float meleeCooldown;
	public float currentMeleeCooldown;
	public bool canMelee = false;
	
	public int currentAmmo;
	public static int maxAmmo;
	
	void Start()
	{
		player = GameObject.FindWithTag("Player");
		weapon = GameObject.FindWithTag("PlayerWeapon");
		pw = GetComponent<PlayerWeapon>();
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		currentFireRate += Time.deltaTime;
		if(currentFireRate >= fireRate && currentAmmo > 0)
		{
			canThrow = true;
		}
		else if(currentFireRate < fireRate || currentAmmo <= 0)
		{
			canThrow = false;
		}
		
		currentMeleeCooldown += Time.deltaTime;
		if(currentMeleeCooldown >= meleeCooldown)
		{
			canMelee = true;
		}
		else if(currentMeleeCooldown <= meleeCooldown)
		{
			canMelee = false;
		}
		
		if(anim.GetBool("MeleeAttacking") == true)
		{
			player.GetComponent<Player>().isAttacking = true;
		}
		else
		{
			player.GetComponent<Player>().isAttacking = false;
		}
		
		if(Input.GetKeyDown(KeyCode.Mouse0) && canMelee == true)
		{
			MeleeAttack();
			canMelee = false;
			currentMeleeCooldown = 0f;
		}
		else
		{
			anim.SetBool("MeleeAttacking", false);
		}
		
		if(Input.GetKeyDown(KeyCode.Mouse1) && canThrow == true)
		{
			Throw();
			canThrow = false;
			currentFireRate = 0f;
		}
	}
	
	public void MeleeAttack()
	{
		Debug.Log("Melee Attacking...");
		anim.SetBool("MeleeAttacking", true);
	}
	
	public void Throw()
	{
		Debug.Log("Throwing Object...");
		
		Vector3 addY = new Vector3(0f, 0.2f, 0f);
		Vector3 spawnPoint = gameObject.transform.position + addY;
		Instantiate(throwable, spawnPoint, gameObject.transform.rotation);
		
		currentAmmo--;
	}
}
