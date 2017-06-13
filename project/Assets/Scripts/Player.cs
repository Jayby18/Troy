using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	
	public int health = 5;

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
			//TODO: Animate weapon
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
	
	public void ApplyDamage(int damage)
	{
		health -= damage;
		if(health <= 0)
		{
			KillYourself();
		}
		else
		{
			Respawn();
		}
	}
	
	void KillYourself()
	{
		Destroy(gameObject);
		SceneManager.LoadScene("main menu");
	}
	
	void Respawn()
	{
		transform.position = new Vector2(0, 1);
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if(obj.tag == "KillZone")
		{
			Respawn();
		}
	}
}
