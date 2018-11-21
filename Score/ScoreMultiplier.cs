using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreMultiplier : MonoBehaviour {

	Text text;
	public ScoreManager ScoreManager;

	public int currentMultiplier = 1;
	public int maxMultiplier = 10;



	void Start(){
		text = GetComponent<Text> ();

		ScoreManager = FindObjectOfType<ScoreManager> ();
	//	multiplier = 2;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "X" + currentMultiplier;

		}

	public void AddMultiplier (int eMultiplier){
		currentMultiplier = Mathf.Min (currentMultiplier + eMultiplier, maxMultiplier);
}
}