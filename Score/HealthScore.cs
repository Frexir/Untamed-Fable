using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthScore : MonoBehaviour {

	public PlayerHealth playerHealth;
	public float healthScore;
	Text text;
	// Use this for initialization
	void Start () {
		playerHealth = FindObjectOfType<PlayerHealth> ();
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthScore = playerHealth.playerHealth * 20;
		text.text = "" + healthScore;
	}
}
