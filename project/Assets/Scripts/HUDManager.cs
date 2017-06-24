using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour 
{
	public GameObject deathScreen;
	
	void Start()
	{
		deathScreen = GameObject.Find("DeathScreen");
		deathScreen.SetActive(false);
	}
	
	void Update()
	{
		if(deathScreen.activeSelf == true && Input.GetKeyDown(KeyCode.Space))
		{
			Die();
		}
	}
	
	public void ActivateDeathScreen()
	{
		deathScreen.SetActive(true);
		Invoke("Die", 3.0f);
	}
	
	void Die()
	{
		SceneManager.LoadScene("main menu");
	}
}
