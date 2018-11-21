using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	public string startLevel;
	public string howToPlay;
	public string credits;
	public string settings;

	void Start () {
	}

	public void NewGame(){
		Application.LoadLevel (startLevel);
	}
	public void Credits(){
		Application.LoadLevel (credits);
	}
	public void Settings(){
		Application.LoadLevel (settings);
	}
	public void HowToPlay(){
		Application.LoadLevel (howToPlay);
	}
	public void ExitGame(){
		Application.Quit ();
	}
}
