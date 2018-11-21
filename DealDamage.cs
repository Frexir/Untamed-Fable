using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour {

	public PlayerController2 player;
	public int damageToGive;
	private float cooldown;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (cooldown > 0)
		cooldown -= Time.deltaTime;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyHealthManager> ().giveDamage (damageToGive);
		}
		if (other.tag == "Boss"){
			other.GetComponent<BossHealthManager>().giveDamage(damageToGive);
			}
		}
	}

