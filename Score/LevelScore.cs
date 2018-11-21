using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour {
	public ScoreManager scoreManager;
	public float levelScore;
	Text text;

	// Use this for initialization
	void Start () {
		scoreManager = FindObjectOfType<ScoreManager> ();
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		levelScore = scoreManager.score;
		text.text = "" + levelScore;
	
	}
}
