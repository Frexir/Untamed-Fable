using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public PlayerHealth playerHealth;
	Text text;
	// Use this for initialization
	void Start () {
		playerHealth = FindObjectOfType<PlayerHealth> ();
		text = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		text.text = "" + playerHealth.playerHealth;
	}
}