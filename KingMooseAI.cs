using UnityEngine;
using System.Collections;
public enum MOOSESTATES {MoosePrepared, MooseSwing, MooseCharge, MooseStunned};

public class KingMooseAI : MonoBehaviour {

	Vector3 originalCameraPosition;
	public SmoothCamera2D camShake;
	public MOOSESTATES mooseStates;
	private float shakeAmt;
	public bool shaken;
	public BossHealthManager health;
	public bool bossKilled = false;

	public Transform tTarget;
	public Vector3 vTarget;
	public Transform[] Waypoints;
	public float moveSpeed;
	private float moveSpeedS;
	public int curWaypoint;
	private int curWaySav;
	//public bool doPatrol = true;
	public Vector3 target;
	private bool attacking;
	public Vector3 moveDirection;
	public Vector3 velocity;
	public Animator anim;

	public bool chandelier =true;

	private GameObject moose;
	private GameObject player;

	private bool playerIsNear;
	private float moveVelocity;
	public int moveDirectionInt;
	public bool moveBackwards;

	private float playerDirectionFloat;

	public float stunTime;
	public float stunTimeCount;

	public float swingDelay;
	private float swingDelayS;
	public float preparedDelay;
	private float preparedDelayS;

	private float randomAttack;

	private bool chargeCheck;
	public bool readyToCharge;
	public bool attackDone;
	//public bool chargeCheck;

	private bool playerInCollider;

	public GameObject stars;
	public GameObject mSlam;
	public GameObject mCharge;
	public GameObject bSigh;
	public GameObject mStop;
	// Use this for initialization
	void Awake () {

		camShake = FindObjectOfType<SmoothCamera2D> ();
		health = GetComponent<BossHealthManager> ();
		anim = GetComponent<Animator> ();
		player = GameObject.Find("Protagonist");
		preparedDelayS = preparedDelay;
		swingDelayS = swingDelay;
		moveSpeedS = moveSpeed;
	//	transform.localScale = new Vector3 (1f, 1f, 1f);
		target = transform.position;
		stars = GameObject.Find ("Stars");
		stars.SetActive(false);

		mSlam = GameObject.Find ("MSlam");
		mCharge = GameObject.Find ("MCharge");
		bSigh = GameObject.Find ("BSigh");
		mStop = GameObject.Find ("MStop");
	}
	
	// Update is called once per frame
	void Update () {
		switch(mooseStates) {
		case MOOSESTATES.MooseSwing:
			MooseSwing ();
			break;
		case MOOSESTATES.MooseCharge:
			MooseCharge ();
			break;
		case MOOSESTATES.MooseStunned:
			MooseStunned ();
			break;
		case MOOSESTATES.MoosePrepared:
			MoosePrepared ();
			break;
		}
		if (moveBackwards == true) {
			if (moveDirection.x > 0) {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
				moveDirectionInt = -1;
			}
			if (moveDirection.x < 0) {
				transform.localScale = new Vector3 (1f, 1f, 1f);
				moveDirectionInt = 1;
			}
		} else {
			if (moveDirection.x > 0) {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
				moveDirectionInt = 1;
			}
			if (moveDirection.x < 0) {
				transform.localScale = new Vector3 (1f, 1f, 1f);
				moveDirectionInt = -1;
			}
		}
		moveVelocity = moveSpeed * moveDirectionInt;

		//anim.SetFloat ("moveSpeed", Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x));
		Vector3 dir = player.transform.position - transform.position;
		//playerDirectionFloat = player.transform.position.x - transform.position.x;
		float distance = dir.sqrMagnitude;
	/*	if (health.enemyHealth <= 0) {
			bossKilled = true;
			Debug.Log("killed");
		}*/
		if (shaken) {
			InvokeRepeating ("CamShake", 0, 0.01f);
			Invoke ("StopShaking", 0.3f);
		}
	}

	public void MooseSwing() {	
		preparedDelay = preparedDelayS;
		swingDelay = swingDelayS;
		moveVelocity = moveSpeed * moveDirectionInt;

		anim.Play ("MooseAttack");
		mSlam.GetComponent<AudioSource> ().Play ();
		//Attack Animation
		moveSpeed = 15;
		if (curWaypoint < Waypoints.Length) {
			target = Waypoints [curWaypoint].position;
			moveDirection = target - transform.position;
			velocity = GetComponent<Rigidbody2D> ().velocity;
			if (moveDirectionInt == -1) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
				if (moveDirection.magnitude < 2) {
					curWaySav = curWaypoint;
					curWaypoint++;
					//anim.SetBool ("MooseAttack",false);
					mooseStates = MOOSESTATES.MoosePrepared;
				}
			}
			if (moveDirectionInt == 1) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
				if (moveDirection.magnitude < 2) {
					curWaySav = curWaypoint;
					curWaypoint--;
				//	anim.SetBool ("MooseAttack", false);
					mooseStates = MOOSESTATES.MoosePrepared;
				}
			}




			/*

			if (moveDirection.magnitude < 1) {
				curWaypoint++;
				mooseStates = MOOSESTATES.MoosePrepared;
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
				}
		} else {
			moveSpeed = moveSpeedS;
			moveDirection = target - transform.position;
			if (moveDirection.magnitude < 2)
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);			
			//curWaypoint = 0;
	}*/
		}
	}
	public void MooseCharge(){
		preparedDelay = preparedDelayS;
		swingDelay = swingDelayS;
		moveVelocity = moveSpeed * moveDirectionInt;

		target = Waypoints [curWaypoint].position;
		moveDirection = target - transform.position;
		velocity = GetComponent<Rigidbody2D> ().velocity;

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
		if (readyToCharge || chargeCheck) {
			chargeCheck = true;
			anim.SetBool ("Prepared", false);
			anim.SetBool ("Charge", true);
			moveSpeed = 28;
			mCharge.GetComponent<AudioSource> ().Play ();
			//Charge Animation
			if (moveDirectionInt == 1) {

				curWaypoint = 0;
				curWaySav = curWaypoint;
				if (moveDirection.magnitude < 2) {
					stunTimeCount = stunTime;
					anim.SetBool ("Charge", false);
					mCharge.GetComponent<AudioSource> ().Stop ();
					mStop.GetComponent<AudioSource> ().Play ();
					mooseStates = MOOSESTATES.MooseStunned;
				}
			}
			if (moveDirectionInt == -1) {

				curWaypoint = 4;
				curWaySav = curWaypoint;
				if (moveDirection.magnitude < 2) {
					transform.localScale = new Vector3 (1f, 1f, 1f);
					stunTimeCount = stunTime;
					anim.SetBool ("Charge", false);	
					mCharge.GetComponent<AudioSource> ().Stop ();
					mStop.GetComponent<AudioSource> ().Play ();
					mooseStates = MOOSESTATES.MooseStunned;
				}
			}

		}





		/*
		if (curWaypoint < Waypoints.Length) {
			target = Waypoints [curWaypoint].position;
			moveDirection = target - transform.position;
			velocity = GetComponent<Rigidbody2D> ().velocity;
			if (moveDirection.magnitude < 1) {
				curWaypoint++;
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
			}
		} else {
			moveSpeed = moveSpeedS;
			moveDirection = target - transform.position;
			if (moveDirection.magnitude < 2)
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
	}*/

	}

	public void MooseStunned(){
		stars.SetActive(true);
		chargeCheck = false;
		moveVelocity = moveSpeed * moveDirectionInt;
		moveSpeed = 0;
		anim.SetBool ("Stunned", true);
	//Stunned animation
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		if (stunTimeCount > (float)0.1) {
			stunTimeCount -= Time.deltaTime;
		}
		if (stunTimeCount < (float)0.1) {
			anim.SetBool ("Stunned", false);
			stars.SetActive(false);
			mooseStates = MOOSESTATES.MoosePrepared;
		}
		
	}
	public void MoosePrepared(){
		anim.SetBool ("Idle",true);
		bSigh.GetComponent<AudioSource> ().Play ();
		moveVelocity = moveSpeed * moveDirectionInt;
		moveSpeed = 0;
		if (curWaypoint <= 4 && curWaySav == 4 )
			curWaypoint = 3;
		if (curWaypoint >= 0 && curWaySav == 0)
			curWaypoint = 1;
		target = Waypoints [curWaypoint].position;
		moveDirection = target - transform.position;
	//Standing Animation

		if (preparedDelay > (float)0.1)
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
			preparedDelay -= Time.deltaTime;
		if (preparedDelay < (float)0.1) {
			//Prepared Animation
			//anim.SetBool ("Idle", false);
			if (swingDelay > (float)0.1)
				swingDelay -= Time.deltaTime;
			if (swingDelay < (float)0.1) {
				randomAttack = Random.Range (0, 9);
				if (randomAttack <= 6) {
					anim.SetBool ("Idle",false);
					mooseStates = MOOSESTATES.MooseSwing;
				} if(randomAttack > 6 || (curWaypoint == 2 && (curWaySav == 1 || curWaySav == 3))) {
					anim.SetBool ("Idle",false);
					anim.SetBool("Prepared", true);
					mooseStates = MOOSESTATES.MooseCharge;
						}
					}
				}
				}
			
 void CamShake() {
		float quakeAmtx = Random.Range (-0.1f,0.1f);
		float quakeAmty = Random.Range (-0.1f,0.1f);
		Vector3 Cam = camShake.transform.position;
		Cam.y += quakeAmty;
		Cam.x += quakeAmtx;
		camShake.transform.position = Cam;
			}
	void StopShaking() {
		CancelInvoke ("CamShake");
	//	camShake.transform.position = originalCameraPosition;
	}

		

}

