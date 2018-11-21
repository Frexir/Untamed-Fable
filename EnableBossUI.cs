using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnableBossUI : MonoBehaviour {

	private bool playerInZone = false;
	public bool childSpawned = false;
	public Slider bSlider;
	public Text text;
	public GameObject gObj;
	public GameObject gObj2;
	//public SpawnBoss sBoss;
	// Use this for initialization
	void Start () {
		
		//sBoss = FindObjectOfType<SpawnBoss> ();
			bSlider = FindObjectOfType<Slider> ();
				text = FindObjectOfType<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInZone) {
			bSlider.enabled = true;
			text.enabled = true;
			gObj.SetActive(true);
			gObj2.SetActive(true);
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
