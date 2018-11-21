using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	//private string events = null;
	//private string triggeredEvent = null;
	public GameObject backgroundMusic;
	// Use this for initialization
	void Start () {
		backgroundMusic = GameObject.Find ("BackgroundMusic");
			backgroundMusic.GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
