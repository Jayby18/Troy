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
	
	public GameObject target;
	public Transform targetTransform;
	
	public float health = 100f;
	float distance;
	float lookAtDistance = 15.0f;
	float attackRange = 1.0f;
	float moveSpeed = 5.0f;
	float damping = 6.0f;
	
	void Start()
	{
		target = GameObject.FindWithTag("Player");
		targetTransform = target.GetComponent<Transform>();
		
		em = GetComponent<EnemyMovement>();
		ec = GetComponent<EnemyCombat>();
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		distance = Vector2.Distance(target.transform.position, transform.position);
		Vector2 direction = targetTransform.position - target.transform.position;
		RaycastHit hit;
		
		//.Log(direction);
		
		if(distance < lookAtDistance)
		{
			//Player is nearby
		}
		
		//Check if any objects between player and enemy
		if(Physics.Raycast(transform.position, direction, out hit))
		{
			if(hit.transform == targetTransform)
			{
				Debug.Log("Nothing between player and enemy");
			}
			else
			{
				Debug.Log("Something between player and enemy");
			}
		}
	}
	
	/*
	void Movement()
	{
		float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");
		
		if(xMov != 0 || zMov != 0)
		{
			anim.SetBool("Moving", true);
			Debug.Log("Moving...");
		}
		else
		{
			anim.SetBool("Moving", false);
		}
	
        Vector2 movHorizontal = (target.transform.position), transform.position);
		
        Vector2 movVertical = transform.forward * zMov;
		Vector2 velocity = (movHorizontal + movVertical) * speed;
		
		Vector2 velocity = movHorizontal * speed;
	
        em.MoveHorizontal(velocity);
	}
	*/
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		
		if(obj.tag == "Player" && obj.GetComponent<Player>().isAttacking == false)
		{
			Debug.Log("Hit player");
			obj.SendMessage("ApplyDamage", 1);
		}
	}
	
	public void ApplyDamage(float damage)
	{
		health -= damage;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
