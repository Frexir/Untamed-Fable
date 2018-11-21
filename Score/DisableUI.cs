using UnityEngine;
using System.Collections;

public class DisableUI : MonoBehaviour {

	public Canvas playerUI;
	// Use this for initialization
	void Start () {
		playerUI = GameObject.Find ("PlayerUI").GetComponent<Canvas> ();
		playerUI.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
