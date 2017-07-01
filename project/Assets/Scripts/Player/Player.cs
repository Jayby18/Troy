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
	private PlayerCombat pc;
	
	public int health = 5;
	public int exp = 0;

    public float speed;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpSpeed = 7f;
	
	public bool isGrounded;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
		anim = GetComponent<Animator>();
		rend = GetComponentInChildren<SpriteRenderer>();
		pc = GetComponentInChildren<PlayerCombat>();
		
		Physics.IgnoreLayerCollision(10, 10, true);
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
	
	public void GainExperiencePoints(int points)
	{
		exp += points;
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
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if(obj.tag == "KillZone")
		{
			ApplyDamage(1);
		}
		else if(obj.tag == "ThrowablePickup")
		{
			pc.currentAmmo++;
		}
	}
	
	void OnCollisionStay2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if(obj.layer == 8)
		{
			isGrounded = true;
		}
	}
	
	void OnCollisionExit2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if(obj.layer == 8)
		{
			isGrounded = false;
		}
	}
}
