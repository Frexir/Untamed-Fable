using UnityEngine;
using System.Collections;

public class SetActive : MonoBehaviour {


		private bool playerInZone = false;
		public bool childSpawned = false;
	//	public GameObject child;
		// Use this for initialization
		void Start () {
		//child = GetComponentInChildren<GameObject> ();

			//kMoose = FindObjectOfType<KingMooseAI> ();
			//	bSlider = FindObjectOfType<Slider> ();
			//		text = FindObjectOfType<Text>();
		}
		
		// Update is called once per frame
		void Update () {
		foreach (Transform child in this.transform) {
			if (playerInZone == true && !childSpawned) {
				child.gameObject.SetActive (true);

				//	Instantiate(boss, transform.position, transform.rotation);
				childSpawned = true;
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

