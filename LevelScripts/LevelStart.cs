using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour {

	public SmoothCamera2D smoothCamera;
	public PlayerController2 playerController;
	public bool startAnimDone;
	// Use this for initialization
	void Start () {
		smoothCamera = FindObjectOfType<SmoothCamera2D> ();
		playerController = FindObjectOfType<PlayerController2> ();

		playerController.enabled = false;
		smoothCamera.isFollowing = false;

		playerController.GetComponent<Animator> ().Play ("Level1Intro", 0);
		playerController.moveVelocity = 10;


	}

	// Update is called once per frame
	void Update () {
		if (playerController.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f ) {
		//	Debug.Log ("donez");
			playerController.GetComponent<Animator> ().StopPlayback();
			playerController.moveVelocity = playerController.moveSpeed*Input.GetAxisRaw ("Horizontal");
			playerController.enabled = true;
			smoothCamera.isFollowing = true;
		}
	
	}
}
