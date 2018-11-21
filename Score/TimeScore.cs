using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeScore : MonoBehaviour {
	public LevelTimer levelTimer;
	public float timeScore;
	public float PPTimeScore;
	Text text;
	string lTimeClock = "lTimeClock";
	string lTimeScore = "levelTimeScore1";
	// Use this for initialization
	void Start () {
		levelTimer = FindObjectOfType<LevelTimer> ();
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		PPTimeScore = PlayerPrefs.GetFloat (lTimeClock);
		timeScore = (900 - Mathf.FloorToInt(PPTimeScore)) * 20;
		PlayerPrefs.SetFloat (lTimeScore, timeScore);
		text.text = "" + timeScore;
	}
}
