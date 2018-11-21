using UnityEngine;
using System.Collections;

public class BossKilled : MonoBehaviour {

	//public bool bossInZone;
	public string levelToLoad;
	public KingMooseAI kMoose;
	// Use this for initialization
	void Start () {
		kMoose = FindObjectOfType<KingMooseAI> ();
		//bossInZone = true;

	}
	
	// Update is called once per frame
	void Update () {
			if (kMoose.health.enemyHealth <= 1){		
				gameObject.SetActive (false);
			Application.LoadLevel (levelToLoad);
			}
		}
	}


/*void OnTriggerEnter2D(Collider2D other){
	if (other.name == "ey_boss")
		bossInZone = true;
}

void OnTriggerStay2D(Collider2D other){
	
	if (other.name == "ey_boss") {
			Debug.Log ("done");
			bossInZone = true;
		} 
		if (other.name == "Protagonist" && other.name != "ey_boss"){
			bossInZone = false;
		}
} */
