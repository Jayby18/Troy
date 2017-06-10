using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement pm;
	private PlayerCombat pc;
	private Animator anim;
	private Rigidbody2D rb;
	
	public int health;

    public float speed;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpSpeed = 7f;
	
	public float fireRate;
	public float currentFireRate;
	public bool canThrow = false;
	public float meleeCooldown;
	public float currentMeleeCooldown;
	public bool canMelee = false;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
		pc = GetComponent<PlayerCombat>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		//Movement:
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        Movement();
		
		//Combat:
		currentFireRate += Time.deltaTime;
		if(currentFireRate >= fireRate)
		{
			canThrow = true;
			currentFireRate = 0;
		}
		else if(currentFireRate < fireRate)
		{
			canThrow = false;
		}
		
		currentMeleeCooldown += Time.deltaTime;
		if(currentMeleeCooldown >= meleeCooldown)
		{
			canMelee = true;
			currentMeleeCooldown = 0;
		}
		else if(currentMeleeCooldown <= meleeCooldown)
		{
			canMelee = false;
		}
		
		if(Input.GetKeyDown(KeyCode.Mouse0) && canMelee == true)
		{
			pc.MeleeAttack();
			canMelee = false;
		}
		
		if(Input.GetKeyDown(KeyCode.Mouse1) && canThrow == true)
		{
			pc.Throw();
			canThrow = false;
		}
    }

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

        Vector2 movHorizontal = transform.right * xMov;
        Vector2 movVertical = transform.forward * zMov;

        Vector2 velocity = (movHorizontal + movVertical) * speed;

        pm.MoveHorizontal(velocity);
    }
}
