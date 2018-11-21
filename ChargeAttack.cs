using UnityEngine;
using System.Collections;

public class ChargeAttack: MonoBehaviour {
	
	private EnemyAI_States enemy;
	public ScoreMultiplierTimer SMTimer;
	public int damageToGive;
	public int timePunish;
	public PlayerController2 player;
	public PlayerHealth playerHealth;
	
	//private PlayerController2 player;
	//	private bool timeoutActive = false;
	//	public float invTime;
	// Use this for initialization
	void Start () {
		playerHealth = FindObjectOfType<PlayerHealth> ();
		SMTimer = FindObjectOfType<ScoreMultiplierTimer>();
	}
	
	// Update is called once per frame
	
	
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		player = FindObjectOfType<PlayerController2> ();
		if (player.invincible == false && other.name == "Protagonist" && other.tag != "SwordBlock") {
			player.invincible = true;
			player.timeSpentInvincible = 0;
			playerHealth.HurtPlayer (damageToGive);
			SMTimer.GetHit();
			
			
			
			player.bossknockbackCount = player.bossknockbackLength;
			if (other.transform.position.x < transform.position.x)
				player.knockFromRight = true;
			else
				player.knockFromRight = false;
		}		
	}
}