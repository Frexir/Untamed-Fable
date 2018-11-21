using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScreenOverall : MonoBehaviour {

//	public LevelScore levelScore;

//	public HealthScore healthScore;

//	public TimeScore timeScore;

	public float score;
	public float highScore = 0;
	string highScoreKey = "HighScore";
	string SavedLevelScore = "levelScore1";
	public float overallScoreNum;
	public float addTimeScore;
	string lTimeScore = "levelTimeScore1";
	Text text;


	// Use this for initialization
	void Start () {
	
		


	//	healthScore = FindObjectOfType<HealthScore> ();

	//	timeScore = FindObjectOfType<TimeScore> ();

	//	levelScore = FindObjectOfType<LevelScore> ();

		text = GetComponent<Text> ();

	}


	// Update is called once per frame

	void Update () {
		overallScoreNum = PlayerPrefs.GetFloat (SavedLevelScore, 0);
		highScore = PlayerPrefs.GetFloat(highScoreKey,0);
		addTimeScore = PlayerPrefs.GetFloat (lTimeScore,0);
		score = overallScoreNum+addTimeScore;
	//	overallScoreNum = healthScore.healthScore + timeScore.timeScore + levelScore.levelScore;

		text.text = "" + overallScoreNum;
		if(score>highScore){
			PlayerPrefs.SetFloat(highScoreKey, score);
			PlayerPrefs.Save();
		}

	}
	/*void OnDisable(){
		
		//If our scoree is greter than highscore, set new higscore and save.
		if(score>highScore){
			PlayerPrefs.SetFloat(highScoreKey, score);
			PlayerPrefs.Save();
		}
	}*/

}

