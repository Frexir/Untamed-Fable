using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int playerHealth;
	public int maxPlayerHealth;
	public LevelLoader levelLoader;
	public Slider healthBar;

	public PlayerController2 player;
	private LevelManager levelManager;
	public GameObject ghost;
	public GameObject protagonist;
	public bool positionset;
	[HideInInspector]
	public float endtime = 0;

	private GameObject deathPanel;


	//Text text;

	// Use this for initialization
	void Start () {
		ghost = GameObject.Find ("Ghost");
		protagonist = GameObject.Find ("Protagonist");
		deathPanel = GameObject.Find ("DeathScreenEmpty");
		player = FindObjectOfType<PlayerController2> ();
		healthBar = GetComponent<Slider> ();
		levelLoader = FindObjectOfType<LevelLoader>(); 
	//	text = GetComponent<Text> ();
		if(!levelLoader.levelCompleted)
		playerHealth = maxPlayerHealth;
		levelManager = FindObjectOfType<LevelManager> ();
		ghost.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth <= 0 && !levelLoader.levelCompleted) {

			ghost.SetActive(true);
			player.anim.SetBool("hurt",false);
			player.GetComponent<PlayerController2>().enabled = false;
		

			if (!positionset) {
				ghost.transform.localPosition = new Vector3 (0,0,0);

				positionset = true;
			}
			player.anim.Play("NewDeath");

			ghost.transform.position = new Vector3 (ghost.transform.position.x,ghost.transform.position.y+Time.deltaTime,ghost.transform.position.z);
			//Instantiate(ghost, player.transform.position, player.transform.rotation);
			//Debug.Log("WHAT");
			endtime = endtime+Time.deltaTime;
			if (endtime > 5) {
			deathPanel.GetComponent<DeathScreen>().playerDead = true;		
			playerHealth = maxPlayerHealth;
			
			}
		}
		healthBar.value = playerHealth;
	//	text.text = "" + playerHealth;
	}
		public void HurtPlayer(int damageToGive){
		playerHealth -= damageToGive;
	}
	public void FullHealth() {
		playerHealth = maxPlayerHealth;
	}
}
