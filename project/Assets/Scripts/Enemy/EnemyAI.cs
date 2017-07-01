using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
	[SerializeField]
    private EnemyMovement em;
	private EnemyCombat ec;
	private Animator anim;
	private Rigidbody2D rb;
	
	public float health = 100f;
	public float moveSpeed = 5.0f;
	
	public bool moveRight = true;
	
	void Start()
	{
		em = GetComponent<EnemyMovement>();
		ec = GetComponent<EnemyCombat>();
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if(moveRight == true)
		{
			transform.right = Vector3.right;
		}
		else
		{
			transform.right = Vector3.left;
		}
		Movement();
	}
	
	void Movement()
	{
		Vector2 velocity = transform.right * moveSpeed;
	
        em.MoveHorizontal(velocity);
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		
		if(obj.tag == "Player")
		{
			Debug.Log("Hit player");
			obj.SendMessage("ApplyDamage", 1);
		}
		else if(obj.layer != 8)
		{
			transform.localEulerAngles = new Vector3(0, 180, 0);
			if(moveRight == true)
			{
				moveRight = false;
			}
			else
			{
				moveRight = true;
			}
		}
	}
	
	public void ApplyDamage(float damage)
	{
		health -= damage;
		if(health <= 0)
		{
			GameObject.FindWithTag("Player").SendMessage("GainExperiencePoints", 25);
			Destroy(gameObject);
		}
	}
}
