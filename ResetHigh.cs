using UnityEngine;
using System.Collections;

public class ResetHigh : MonoBehaviour {
	public float highScore;
	string highScoreKey = "HighScore";
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ResetHighScore (){
		PlayerPrefs.SetFloat (highScoreKey, 0);
		PlayerPrefs.Save ();
	}

}
