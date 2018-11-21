using UnityEngine;
using System.Collections;

public class ScoreScreenMenu : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;
	public GameObject toDestroy;
	public GameObject toDestroy2;

	void Start () {
		toDestroy = GameObject.Find("PlayerUI");
		toDestroy2 = GameObject.Find("LevelExit");
	}

	public void LevelSelect(){
		GameObject.Destroy (toDestroy, 0);
		GameObject.Destroy (toDestroy2, 0);
		Application.LoadLevel (levelSelect);
	}

	public void HowToPlay(){
		GameObject.Destroy (toDestroy, 0);
		GameObject.Destroy (toDestroy2, 0);
		Application.LoadLevel (mainMenu);
	}

}