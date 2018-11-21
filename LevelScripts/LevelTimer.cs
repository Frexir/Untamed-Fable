using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {

	public LevelLoader levelLoader;

	private float startTime; 
	public static float finishTime;
	public float currentTime;
	public static float minutes;
	public static float seconds;
	public static float fraction;

	public string shownTime;
	string lTimeClock = "lTimeClock";

	Text text;

	void Start(){
		levelLoader = FindObjectOfType<LevelLoader>(); 
		startTime = Time.time;
		text = GetComponent<Text> ();

	}

	void Update (){
		if (!levelLoader.levelCompleted){
		currentTime = Time.time - startTime;
		text.text = "" + shownTime;
		minutes = Mathf.Floor(currentTime / 59.99f);
		seconds = Mathf.Abs(currentTime % 59.99f);
		fraction = Mathf.Abs((currentTime * 100) % 100);
		shownTime = string.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction); 
			}
			}
	void OnDisable () {
		PlayerPrefs.SetFloat(lTimeClock, currentTime);
		PlayerPrefs.Save();
	}
}
