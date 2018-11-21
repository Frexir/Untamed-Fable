using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreMultiplierTimer : MonoBehaviour {

	public ScoreMultiplier ScoreMultiplier;
	public HurtPlayer HPlayer;
	public float currentTime = 10;
	private float nextTime;
	public int timePunish;

	private static string shownTime;

	Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		ScoreMultiplier = FindObjectOfType<ScoreMultiplier> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ScoreMultiplier.currentMultiplier <= 1) {
			currentTime = 10;
		}
		if (ScoreMultiplier.currentMultiplier > 1) {
			currentTime = currentTime - Time.deltaTime;
			if (currentTime < 0 && currentTime > -1) {
				ScoreMultiplier.currentMultiplier = ScoreMultiplier.currentMultiplier - 1;
				currentTime = 10;
			}
			if (currentTime < -1) {
				nextTime = currentTime;
				ScoreMultiplier.currentMultiplier = ScoreMultiplier.currentMultiplier - 1;
				currentTime = 10 + nextTime;
			}
		}
		text.text = "" + shownTime;
		shownTime = string.Format ("{0:00}", currentTime); 
}
	public void GetHit() {
		currentTime = currentTime - timePunish;
	}
}
