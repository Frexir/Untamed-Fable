using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public bool paused;
	public GameObject pausePanel;
	public string mainMenu;
	// Use this for initialization
	void Start () {
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (paused)
			PauseGame (true);
		else
			PauseGame (false);

		if (Input.GetButtonDown ("Pause")) {
			switchPause ();
			Debug.Log ("asfasd");
		}
	}
	void PauseGame(bool state) {
		if (paused) {
			pausePanel.SetActive(true);
			Time.timeScale = 0;
		} else if (!paused) {
			Time.timeScale = 1;
			pausePanel.SetActive(false);
		}
	}
	public void switchPause(){
		if (paused)
			paused = false;
		else
			paused = true;
	}
	public void MainMenu(){
		Application.LoadLevel (mainMenu);
	}
}
