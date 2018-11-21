using UnityEngine;
using System.Collections;

public class EnemyDealDamage : MonoBehaviour {

	public EnemyAI_States enemy;
	public int damageToGive;
	public PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
		playerHealth = FindObjectOfType<PlayerHealth> ();
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Protagonist") {
			playerHealth.HurtPlayer (damageToGive);
		}
	}
}