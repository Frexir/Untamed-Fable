using UnityEngine;
using System.Collections;

public enum STATES {Patrol, Flee, Chase, Stationary, Stunned, Prepared, Elevation};

public class EnemyAI_States : MonoBehaviour {

	public STATES aiStates;
	public Transform tTarget;
	public Vector3 vTarget;
	public Transform[] Waypoints;
	public float moveSpeed;
	private float moveSpeedS;
	public int curWaypoint;
	public bool doPatrol = true;
	public Vector3 target;
	private bool attacking;
	public Vector3 moveDirection;
	public Vector3 velocity;
	public Animator anim;

	[HideInInspector]
	public Animation attAnim;

	public GameObject eSlash;
	private GameObject enemy;
	private GameObject player;
	private PlayerController2 playercon;
	public float range;
	public float attackRange;
	public float maxAngle = 0.90f;
	public float returnToPatrol;
	private bool playerIsNear;
	private float moveVelocity;
	private int moveDirectionInt;
	public bool moveBackwards;

	private float playerDirectionFloat;

	public float knockbackx;
	public float knockbacky;
	public float knockbackCount;
	public float knockbackLenght;
	public bool knockFromRight;

	public float stunTime;
	public float stunTimeCount;

	private bool atEdge;
	public Transform edgeCheck;
	public float edgeCheckRadius;
	public LayerMask whatIsWall;
	public Transform groundCheckEnemy1;

	public bool grounded;

	public float attackDelay;
	private float attackDelayS;

	public bool attackDone;
	public bool chargeCheck;

	private bool playerInCollider;

	public bool randomMovement;
	private float rx;
	private float timeToMove = 10;
	public float randomRange;

	private bool closeEnough = false;


	// Use this for initialization
	void Start () {
		eSlash = GameObject.Find ("ESlash");
		anim = GetComponent<Animator> ();
		attAnim = GetComponent<Animation> ();
		player = GameObject.Find("Protagonist");
		attackDelayS = attackDelay;
		moveSpeedS = moveSpeed;
		playercon = FindObjectOfType<PlayerController2> ();
		transform.localScale = new Vector3 (1f, 1f, 1f);
		target = transform.position;
	}

	// Update is called once per frame
	void Update () {
		PlayerManouver();
		switch(aiStates) {
		case STATES.Chase:
			Chase ();
			if (playerIsNear == false)
				aiStates = STATES.Stationary;
			break;
		case STATES.Flee:
			Flee ();
			break;
		case STATES.Patrol:
			Patrol ();
			if (playerIsNear == true)
				aiStates = STATES.Chase;
			break;
		case STATES.Stationary:
			Stationary();
			if (playerIsNear == true)
				aiStates = STATES.Chase;
			break;
		case STATES.Stunned:
			Stunned ();
			break;
		case STATES.Prepared:
			Prepared ();
			break;
		case STATES.Elevation:
			Elevation ();
			break;
		}
	
		grounded = Physics2D.OverlapCircle (groundCheckEnemy1.position, edgeCheckRadius, whatIsWall);
		if (grounded)
			anim.SetBool ("grounded", true);
		else
			anim.SetBool ("grounded", false);
		atEdge = Physics2D.OverlapCircle (edgeCheck.position, edgeCheckRadius, whatIsWall);
		moveVelocity = moveSpeed * moveDirectionInt;

		anim.SetFloat ("moveSpeed", Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x));
		EnemyProximity ();
		//if(!atEdge)
		Vector3 dir = player.transform.position - transform.position;
		playerDirectionFloat = player.transform.position.x - transform.position.x;
		float distance = dir.sqrMagnitude;
		/*if (distance >= 50) {
			moveBackwards = false;
			closeEnough = false;
		}
		if (distance < 10 && distance >= 0 && stunTimeCount<0.05f) {
			moveBackwards = true;
			anim.SetBool ("moveBackwards", true);
			moveSpeed = 11;
		}
		if (distance < 30 && distance >= 10 && stunTimeCount<0.05f) {
			moveSpeed = 0;
			Debug.Log ("mov0");
			moveBackwards = false;
			anim.SetBool ("moveBackwards", false);
			closeEnough = true;

		}*/
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
			if (knockbackCount <= 0.05f) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
			if (anim.GetBool("Hurt") && grounded)
				anim.SetBool("Hurt",false);
			} 
			else {
				if (knockFromRight)
				GetComponent<Rigidbody2D> ().AddForce(new Vector2(-knockbackx,knockbacky), ForceMode2D.Impulse);
					//GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockbackx, knockbacky);
				if (!knockFromRight)
				GetComponent<Rigidbody2D> ().AddForce(new Vector2(knockbackx,knockbacky), ForceMode2D.Impulse);
					//GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockbackx, knockbacky);
				knockbackCount -= Time.deltaTime;
			}
			//if (chargeCheck == true && distance >= attackRange-100) {
		//		GetComponent<Rigidbody2D>().AddRelativeForce (new Vector2(5000*moveDirectionInt, 0), ForceMode2D.Force);
			//}
			//Vector3 norTar = (target - transform.position).normalized;
			//float angle = Mathf.Atan2 (norTar.y, norTar.x) * Mathf.Rad2Deg;

			/*
		Vector3 dir = ((target.x , target.y , target.z) - transform.position);
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		*/
		if (playercon.moveSpeed > 15 && distance < range+500 && playerDirectionFloat<0) {
			aiStates = STATES.Prepared;				
			transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}

	void Patrol () {
		Vector3 dir = player.transform.position - transform.position;
	//	float distance = dir.sqrMagnitude;
		timeToMove -= Time.deltaTime;
		if (curWaypoint < Waypoints.Length) {
			target = Waypoints [curWaypoint].position;
			moveDirection = target - transform.position;
			velocity = GetComponent<Rigidbody2D> ().velocity;
		//	
		//	Debug.Log ("" + target);
		//	Debug.Log ("" + timeToMove);
			if (moveDirection.magnitude < 11.5f) {
				curWaypoint++;
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
			}
		} else {
			moveSpeed = moveSpeedS;
			moveDirection = target - transform.position;
			if (moveDirection.magnitude < 2 && !randomMovement)
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
			if (doPatrol)
				curWaypoint = 0;
			if (moveDirection.magnitude < 2 && randomMovement)
				moveSpeed = 0;
			if (randomMovement && timeToMove <= 0) {
				rx = Random.Range (-randomRange, randomRange);
				target = transform.position + new Vector3 (rx, 0, 0);
			//	GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
				timeToMove =11 +Random.Range (-randomRange, randomRange);
			}
		}
	}

	void Chase () {
		Vector3 dir = player.transform.position - transform.position;
		float distance = dir.sqrMagnitude;
		if (transform.position != tTarget.position) {
			vTarget = tTarget.position;
			moveDirection = vTarget - transform.position;
			velocity = GetComponent<Rigidbody2D> ().velocity;
			if (moveDirection.magnitude < 0.1f) {
				curWaypoint++;
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
				if (!closeEnough)
				moveSpeed = moveSpeedS;
			}
		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		}
		if (playerIsNear == true) {
		//	float dot = Vector3.Dot (transform.forward, dir.normalized);
			float angle = Vector3.Angle (dir, transform.up);
			if ((angle > 135  || angle < 45) && !anim.GetBool ("Attacking") && distance>3f )
				aiStates = STATES.Elevation;
			if (anim.GetBool ("Attacking")) {
				anim.SetBool ("Attacking", false);
			}
			if (distance > attackRange) {
				attackDelay = attackDelayS;
			}
			if (distance < attackRange && stunTimeCount < 0.1f) {
				attackDelay = attackDelay - Time.deltaTime;
			
				if (attackDelay <= 0) {
					eSlash.GetComponent<AudioSource> ().Play ();
					anim.SetBool ("Attacking", true);
					moveSpeed = 0;

//					GetComponent<Rigidbody2D>().AddRelativeForce (new Vector2(100000*moveDirectionInt, 0), ForceMode2D.Force);
					if (attackDone == true) {
						attackDelay = attackDelayS;
					}
				}
			}
		}
	}

	void Flee (){
		if (transform.position != tTarget.position) {
			vTarget = Waypoints [curWaypoint].position;
			moveDirection = vTarget + transform.position;
			velocity = GetComponent<Rigidbody2D> ().velocity;
			if (moveDirection.magnitude < 2) {
				curWaypoint++;
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
			}
		}
	}

	void Stationary (){
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		if (playerIsNear == true) {
			Vector3 dir = player.transform.position - transform.position;
			float distance = dir.sqrMagnitude;
			float dot = Vector3.Dot (transform.forward, dir.normalized);
			if (anim.GetBool ("Attacking")) {
				anim.SetBool ("Attacking", false);
			}
			if (distance > attackRange) {
				attackDelay = attackDelayS;
			}
			if (distance < attackRange && stunTimeCount < 0.1f) {
				attackDelay = attackDelay - Time.deltaTime;
				if (attackDelay <= 0) {
					anim.SetBool ("Attacking", true);
					//GetComponent<Rigidbody2D>().AddRelativeForce (new Vector2(100000*moveDirectionInt, 0), ForceMode2D.Force);
					if (attackDone == true) {
						attackDelay = attackDelayS;
					}
				}
			}
		}
	}

	void Stunned (){
		moveSpeed = 0;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		if (anim.GetBool ("Attacking")) 
			anim.SetBool ("Attacking", false);
		if (stunTimeCount > (float)0.1)
			stunTimeCount -= Time.deltaTime;
		if (stunTimeCount < (float)0.1)
			aiStates = STATES.Stationary;
	}

	void Prepared(){
		moveSpeed = 0;
		if (playercon.moveSpeed < 15)
			aiStates = STATES.Chase;
		Vector3 dir = player.transform.position - transform.position;
		float distance = dir.sqrMagnitude;
		float dot = Vector3.Dot (transform.forward, dir.normalized);

		//if (anim.GetBool ("FastAttack")) {
		//	anim.SetBool ("FastAttack", false);
		//}
		if (distance > attackRange) {
			attackDelay = attackDelayS;
		}
	//	if (distance < attackRange+800 && stunTimeCount < 0.1f) {
		if (distance < 100 && stunTimeCount < 0.1f) {
			attackDelay = 0;
			if (attackDelay <= 0) {
				anim.Play ("FastAttack");
				//					GetComponent<Rigidbody2D>().AddRelativeForce (new Vector2(100000*moveDirectionInt, 0), ForceMode2D.Force);
				if (attackDone == true) {
				//	if (anim.GetBool ("FastAttack")) {
				//		anim.SetBool ("FastAttack", false);
				//	}
					attackDelay = attackDelayS;
					moveSpeed = moveSpeedS;
					aiStates = STATES.Chase;
				}
			}
		}
	}

	void Elevation (){
		if (playerIsNear) {
		vTarget = tTarget.position;
		moveDirection = vTarget - transform.position;
		velocity = GetComponent<Rigidbody2D> ().velocity;
		Vector3 dir = player.transform.position - transform.position;

		float angle = (Vector3.Angle (dir, transform.up));
		float distance = dir.sqrMagnitude;
			if (distance < attackRange && stunTimeCount < 0.1f) 
				attackDelay = attackDelay - Time.deltaTime;
		//	float dot = Vector3.Dot (transform.forward, dir );
			//Debug.Log ("" + dot);
			if (angle > 45  && angle < 135){
				aiStates = STATES.Chase;
			    }
			if (angle > 135 || angle < 45){
				moveSpeed = 0;
			}
		}
	}

	void EnemyProximity () {
		Vector3 dir = player.transform.position - transform.position;
		float distance = dir.sqrMagnitude;
		float dot = Vector3.Dot (transform.forward, dir.normalized);
		if (distance < range ) {
			playerIsNear = true;
		}
		else
			playerIsNear = false;
		if (distance > returnToPatrol && stunTimeCount < (float)0.1)
			aiStates = STATES.Patrol;
//		else
		//	moveSpeed = moveSpeedS;
	}

	void PlayerManouver(){
		Vector3 dir = player.transform.position - transform.position;
		float distance = dir.sqrMagnitude;
		if (distance >= 25) {
			moveBackwards = false;
			closeEnough = false;
		}
		else if (distance < 20 && distance >= 10 && stunTimeCount<0.1f) {
			moveSpeed = 0;
			moveBackwards = false;
			anim.SetBool ("moveBackwards", false);
			closeEnough = true;	
		}
		else if (distance < 10 && distance >= 0 && stunTimeCount<0.1f && attackDelay<0.25f) {
			moveBackwards = true;
			anim.SetBool ("moveBackwards", true);
			moveSpeed = 11;
		}
	}
	//void Angle (){
	//	float angledot = Vector3.Dot (
	

	/*void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			playerInCollider = true;
			moveBackwards = true;
			anim.SetBool ("moveBackwards", true);
		//	moveSpeed = 0;
			Debug.Log ("Asd");
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			playerInCollider = false;
			moveBackwards = false;
			anim.SetBool ("moveBackwards", false);
		//	moveSpeed = moveSpeedS;
			Debug.Log ("DAsd");
		}
	}*/

}
