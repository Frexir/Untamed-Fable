using UnityEngine;
using System.Collections;

public class PlayerBlock : MonoBehaviour {

	private EnemyAI_States enemy;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter2D(Collider2D other){
		enemy = other.GetComponentInParent<EnemyAI_States> ();
		if (other.tag == "EnemyMeleeAttack") {
			enemy.knockbackCount = enemy.knockbackLenght;
			enemy.stunTimeCount = enemy.stunTime;
			enemy.aiStates = STATES.Stunned;
			if (other.transform.position.x < transform.position.x)
				enemy.knockFromRight = true;
			else
				enemy.knockFromRight = false;
		}
	}
}
