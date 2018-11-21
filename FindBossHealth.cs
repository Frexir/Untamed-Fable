using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FindBossHealth : MonoBehaviour {
	public KingMooseAI kMoose;
	public Slider bossHealth;
	// Use this for initialization
	void Start () {
		kMoose = FindObjectOfType<KingMooseAI> ();
		bossHealth = GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (kMoose.health.enemyHealth >= 1)
		bossHealth.value = kMoose.health.enemyHealth;
		if (kMoose.health.enemyHealth <= 1)
			gameObject.SetActive (false);
	}
}
