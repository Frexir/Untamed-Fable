using UnityEngine; 
using System.Collections;
using UnityEngine.UI;

public class HighScores : MonoBehaviour { 

	public float highScore;
	public float score = 0;
	public float[] highScores = new float[5];
	string highScoreKey = "HighScore";
	Text text;
	void Start(){
		//Get the highScore from player prefs if it is there, 0 otherwise.
   
		text = GetComponent<Text> ();
	}

	void Update(){
		highScore = PlayerPrefs.GetFloat(highScoreKey,0); 
		text.text = "" + highScore;
	}
	
	/*void OnEnable(){
		
		//If our scoree is greter than highscore, set new higscore and save.
		for (int i = 0; i<highScores.Length; i++){
			
			//Get the highScore from 1 - 5
			highScoreKey = "HighScore"+(i+1).ToString();
			highScore = PlayerPrefs.GetFloat(highScoreKey,0);
			
			//if score is greater, store previous highScore
			//Set new highScore
			//set score to previous highScore, and try again
			//Once score is greater, it will always be for the
			//remaining list, so the top 5 will always be 
			//updated
			if(score>highScore){
				float temp = highScore;
				PlayerPrefs.SetFloat(highScoreKey,score);
					score = temp;
			}
		}
	}*/
	
}
