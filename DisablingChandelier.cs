using UnityEngine;
using System.Collections;

public class DisablingChandelier : MonoBehaviour {

	// Use this for initialization
	public KingMooseAI kMoose;
	public GameObject cha;
	public GameObject thro;
	public GameObject mir;
	//	public GameObject child;
	// Use this for initialization
	void Start () {
		
		kMoose = FindObjectOfType<KingMooseAI> ();
		cha = GameObject.Find ("Chandelier");
		thro = GameObject.Find ("Throne");
		mir = GameObject.Find ("Mirror");
		//	bSlider = FindObjectOfType<Slider> ();
		//		text = FindObjectOfType<Text>();
	}
	
	// Update is called once per frame
	void Update () {
			if (kMoose.chandelier) {
			cha.gameObject.SetActive (true);
			thro.gameObject.SetActive (true);
			mir.gameObject.SetActive (true);
				
		} else {
			cha.gameObject.SetActive (false);
			thro.gameObject.SetActive (false);
			mir.gameObject.SetActive (false);
		}
		}
}
