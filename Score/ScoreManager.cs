using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public float score;
	public ScoreMultiplier SMultiplier;
	public LevelLoader levelLoader;
	string SavedLevelScore = "levelScore1";
	/*private float currentTime;
	private float nextTime;


	public int currentMultiplier = 1;
	public int maxMultiplier = 10;
*/

	Text text;

	void Start(){
		levelLoader = FindObjectOfType<LevelLoader> ();
		text = GetComponent<Text> ();
		SMultiplier = FindObjectOfType<ScoreMultiplier>();

		if (!levelLoader.levelCompleted)
		score = 0;
	}

	void Update (){
	if (score < 0)
			score = 0;

		text.text = "" + score;

	}

	/*public void AddMultiplier (int eMultiplier){
		currentMultiplier = Mathf.Min (currentMultiplier + eMultiplier, maxMultiplier);
	}*/

	public void AddPoints (int pointsToAdd){
		


		score += pointsToAdd * SMultiplier.currentMultiplier;

	}
	public void Reset()
	{
		score = 0;
	}
	void OnDisable()
	{
		PlayerPrefs.SetFloat (SavedLevelScore, score);
	}
}
