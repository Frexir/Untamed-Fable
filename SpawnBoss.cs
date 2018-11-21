using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnBoss : MonoBehaviour {
	public bool playerInZone = false;
	public bool bossSpawned = false;
	//public GameObject boss;
	public KingMooseAI kMoose;
	//public Slider bSlider;
	public GameObject backgroundMusic;
	public GameObject bossMusic;
	//public Text text;
	public bool childSpawned = false;
	// Use this for initialization
	void Start () {
		backgroundMusic = GameObject.Find ("BackgroundMusic");
		bossMusic = GameObject.Find ("BossMusic");
	//	boss = GameObject.Find ("BossObject");
		//kMoose = FindObjectOfType<KingMooseAI> ();
	//	bSlider = FindObjectOfType<Slider> ();
	//		text = FindObjectOfType<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInZone == true && !childSpawned) {
			backgroundMusic.GetComponent<AudioSource> ().Stop ();
			bossMusic.GetComponent<AudioSource> ().Play ();
			foreach (Transform child in transform) {
					child.gameObject.SetActive (true);
					childSpawned = true;
					bossSpawned = true;

				}
			kMoose = FindObjectOfType<KingMooseAI> ();
			}

		if (bossSpawned) {
			if (kMoose.health.enemyHealth <= 1) {
				foreach (Transform child in transform) {
						child.gameObject.SetActive (false);
						
					}
				}
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
