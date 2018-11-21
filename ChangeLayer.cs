using UnityEngine;
using System.Collections;

public class ChangeLayer : MonoBehaviour {

	// Use this for initialization
	public string layerName = "ToLayer";
	public int sortingOrder = 0;
	public SpriteRenderer sprite;
	public GameObject player;
	void Start () {
		player = GameObject.Find ("Protagonist");
		sprite = player.GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Protagonist") {
		sprite.sortingOrder = sortingOrder;
		sprite.sortingLayerName = layerName;
		}
	}
}
