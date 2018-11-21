using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {
	public LevelLoader lvlLoad;
	// Use this for initialization
	void Start () {
		lvlLoad = FindObjectOfType<LevelLoader> ();
		if (lvlLoad.levelCompleted == true)
		DontDestroyOnLoad (this.gameObject);
	}

}
