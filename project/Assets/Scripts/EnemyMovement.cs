using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour 
{
	[SerializeField]
    private Player player;
	private Rigidbody2D rb;
	
	void Start()
	{
        player = GetComponent<Player>();
		rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
	
	public void MoveHorizontal(Vector2 _velocity)
	{
		rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
	}
}
