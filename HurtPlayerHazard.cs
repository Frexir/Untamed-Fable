using UnityEngine;
using System.Collections;

public class HurtPlayerHazard : MonoBehaviour {
	public PlayerHealth playerHealth;
	public int damageToGive;
	[HideInInspector]
	public bool invincible = false;
	[HideInInspector]
	public float timeSpentInvincible;
	//private PlayerController2 player;
//	private bool timeoutActive = false;
//	public float invTime;
	// Use this for initialization
	void Start () {
	//	player = FindObjectOfType<PlayerController2> ();
		playerHealth = FindObjectOfType<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log ("" + invincible);
	}

	void Update () {
		if (invincible) {
			timeSpentInvincible += Time.deltaTime;

			if (timeSpentInvincible < 1.25f) {
				invincible = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		if (!invincible && other.name == "Protagonist") {
			invincible = true;
			timeSpentInvincible = 0;
			playerHealth.HurtPlayer (damageToGive);


			var player = other.GetComponent<PlayerController2> ();
			player.knockbackCount = player.knockbackLenght;
			if (other.transform.position.x > transform.position.x)
					player.knockFromRight = true;
			else
					player.knockFromRight = false;
		}
	}
		//}
	/*public void BeginTimeout(){
		Debug.Log ("toimiiko asdasd");
		StartCoroutine ("BeginTimeoutCo");
		}
		*/

	/*public IEnumerator BeginTimeoutCo (){
		timeoutActive = true;
		Debug.Log ("toimiiko asd");
		yield return new WaitForSeconds (invTime);
		invincible = false;
		Debug.Log ("toimiiko");
		timeoutActive = false;
	}
*/
}
