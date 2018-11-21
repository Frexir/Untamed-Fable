using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	private bool playerInZone;

	public string levelToLoad;

	public bool levelCompleted = true;



	// Use this for initialization
	void Start () {
		playerInZone = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInZone) {
			levelCompleted = true;
			Application.LoadLevel (levelToLoad);
		}

	
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Protagonist")
			playerInZone = true;
	}
	 void OnTriggerExit2D(Collider2D other){
		if (other.name == "Protagonist")
			playerInZone = false;
	} 
}
