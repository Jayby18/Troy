using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement pm;
	private Animator anim;
	private SpriteRenderer rend;
	public PlayerCombat pc;
	
	public int health = 5;

    public float speed;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpSpeed = 7f;
	
	public bool isGrounded;
	
	public bool isAttacking;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
		anim = GetComponent<Animator>();
		rend = GetComponentInChildren<SpriteRenderer>();
		pc = GetComponentInChildren<PlayerCombat>();
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
		
		//Jumping:
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
		{
			Debug.Log("Jumping...");
			pm.Jump(jumpSpeed);
		}
		
		//Escape to main menu:
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//Make sure main menu is always scene number 0!!!
			SceneManager.LoadScene(0);
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
		
		if(xMov < 0)
		{
			rend.flipX = true;
		}
		else if(xMov > 0)
		{
			rend.flipX = false;
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
		else if(health == 1)
		{
			rend.color = Color.red;
		}
		else
		{
			Respawn();
		}
	}
	
	void KillYourself()
	{
		Destroy(gameObject);
		GameObject HUD = GameObject.Find("HUDManager");
		HUD.GetComponent<HUDManager>().ActivateDeathScreen();
	}
	
	void Respawn()
	{
		transform.position = new Vector2(0, 1);
	}
}
