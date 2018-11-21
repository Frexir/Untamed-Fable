using UnityEngine;
using System.Collections;

public class DeathScreen : MonoBehaviour {
	public bool playerDead;
	public GameObject deathPanel;
	public string mainMenu;
	public string retry;

	// Use this for initialization
	void Start () {
		playerDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerDead){
			Time.timeScale = 0;
			deathPanel.SetActive(true);

		}
	}



	public void MainMenu(){
		playerDead = false;
		Time.timeScale = 1;
		Application.LoadLevel (mainMenu);
		deathPanel.SetActive(false);
	}
	public void Retry(){
		deathPanel.SetActive(false);
		playerDead = false;
		Time.timeScale = 1;
		Application.LoadLevel(retry);
	}
}