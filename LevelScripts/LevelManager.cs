using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {


	public GameObject currentCheckpoint;

	public float respawnDelay;
	
	private SmoothCamera2D cameraFollow;

//	private float gravityStore;
	private HurtPlayer hurtPlayer;
	private PlayerController2 player;
	public PlayerHealth playerHealth;
	private ScoreMultiplier scoreMultiplier;
	public string levelToLoad;
	public float restartDelay = 5f;
	private float restartTimer;
	public GameObject toDestroy;
	public GameObject toDestroy2;
	// Use this for initialization
	void Start () {
	
		player = FindObjectOfType<PlayerController2> ();
		cameraFollow = FindObjectOfType<SmoothCamera2D> ();
		playerHealth = FindObjectOfType<PlayerHealth> ();
		scoreMultiplier = FindObjectOfType<ScoreMultiplier> ();
		toDestroy = GameObject.Find("PlayerUI");
		toDestroy2 = GameObject.Find("LevelExit");
	}

	//gravityStore = player.GetComponent<Rigidbody2D> ().gravityScale;
	// Update is called once per frame
	void Update () {


	}
	public void RespawnPlayer(){
		if (currentCheckpoint)
			StartCoroutine ("RespawnPlayerCo");
		else {
			Application.LoadLevel (levelToLoad);

			//	restartTimer += Time.deltaTime;
			//	if (restartTimer >= restartDelay)
			GameObject.Destroy (toDestroy, 0);
			GameObject.Destroy (toDestroy2, 0);
			//		Application.LoadLevel (Application.loadedLevel);
		}
	}

	public IEnumerator RespawnPlayerCo(){
		//gravityStore = player.GetComponent<Rigidbody2D> ().gravityScale;
		playerHealth.endtime = 0;
		player.timeSpentInvincible = 0 ;
		player.invincible = false;
		player.knockbackCount = 0;
		player.enabled = false;
		scoreMultiplier.currentMultiplier = 1;
		player.GetComponent<Renderer> ().enabled = false;
		player.GetComponent<PolygonCollider2D> ().enabled = false;
		cameraFollow.isFollowing = false;
		//player.GetComponent<Rigidbody2D> ().gravityScale = gravityStore;
		//player.GetComponent<Rigidbody2D> ().gravityScale = 0f;
		//player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		yield return new WaitForSeconds (respawnDelay);
		//player.GetComponent<Rigidbody2D> ().gravityScale = gravityStore;
		cameraFollow.isFollowing = true;
		player.transform.position = currentCheckpoint.transform.position;
		player.enabled = true;


		playerHealth.FullHealth ();
		player.GetComponent<PolygonCollider2D> ().enabled = true;
		player.GetComponent<Renderer> ().enabled = true;

	}
}