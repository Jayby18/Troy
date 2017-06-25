using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Throwable : MonoBehaviour 
{
	private Rigidbody2D rb;
	
	public float speed;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		
		Invoke("KillYourself", 10f);
	}
	
	void Update()
	{
		Vector2 movHorizontal = transform.right;

        Vector2 velocity = (movHorizontal) * speed;
		
		MoveHorizontal(velocity);
	}
	
	public void MoveHorizontal(Vector2 _velocity)
	{
		rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon")
		{
			KillYourself();
		}
	}
	
	void KillYourself()
	{
		Destroy(gameObject);
	}
}
