using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTime : MonoBehaviour {
	public LevelTimer levelTimer;
	Text text;
	// Use this for initialization
	void Start () {
		levelTimer = FindObjectOfType<LevelTimer> ();
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "" + levelTimer.shownTime;

	}
}
