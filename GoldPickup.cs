using UnityEngine;
using System.Collections;

public class GoldPickup : MonoBehaviour {

	public int pointsToAdd;
	public ScoreManager ScoreManager;

	void Start () {
		ScoreManager = FindObjectOfType<ScoreManager>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name == "Protagonist"){
			ScoreManager.AddPoints (pointsToAdd);

		Destroy (gameObject);
		}

		else return;

	}
	

}
