using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

	private bool pointsadded;

	public int enemyHealth;
	public int eMultiplier;
	public int pointsToAdd;
	private PlayerController2 player;

	private EnemyAI_States enemy;
	public Animator anim;

	public GameObject eHurt;
	public GameObject eDeath;
	public ScoreManager ScoreManager;
	public ScoreMultiplier ScoreMultiplier;
	public ScoreMultiplierTimer ScoreMultiplierTimer;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<EnemyAI_States> ();
		anim = GetComponent<Animator> ();
		ScoreManager = FindObjectOfType<ScoreManager> ();
		ScoreMultiplier = FindObjectOfType<ScoreMultiplier> ();
		ScoreMultiplierTimer = FindObjectOfType<ScoreMultiplierTimer> ();
		player = FindObjectOfType<PlayerController2> ();
		eHurt = GameObject.Find ("EHurt");
		eDeath = GameObject.Find ("EDeath");
	}
	
	// Update is called once per frame
	void Update () {
	if (enemyHealth <= 0) {
			eDeath.GetComponent<AudioSource> ().Play ();
			anim.SetBool("Dead", true);
			anim.SetBool("Hurt", false);
			anim.Play("Death");
			enemy.enabled = false;
			if(!pointsadded) {
			ScoreManager.AddPoints (pointsToAdd);
				pointsadded = true;
			}
		}
	}
	public void giveDamage(int damageToGive){
		if (enemyHealth > 0) {
			eHurt.GetComponent<AudioSource> ().Play ();
			anim.SetBool ("Hurt", true);
			enemyHealth -= damageToGive;
			ScoreMultiplier.AddMultiplier (eMultiplier);
			ScoreMultiplierTimer.currentTime = 10;
			enemy.knockbackCount = enemy.knockbackLenght;
			enemy.stunTimeCount = enemy.stunTime;
			enemy.aiStates = STATES.Stunned;
			if (player.transform.position.x > transform.position.x)
				enemy.knockFromRight = true;
			else
				enemy.knockFromRight = false;
		}
	}
}
