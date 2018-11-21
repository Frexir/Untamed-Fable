using UnityEngine;
using System.Collections;

public class EnemyAIOBSOLETE : MonoBehaviour {

	public float moveSpeed;
	public bool moveRight;

	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	public LayerMask whatIsPatrolPoint;
	private bool atPatrolPoint;
	private bool hittingWall;
	private bool atEdge;
	public Transform edgeCheck;
	private Animator anim;
	public Transform patrolPointCheck;
	public Transform guardPost;
	public Transform target;
	public float guardRange;
	private GameObject enemy;
	private GameObject player;
	public float range;
	public float attackRange;
	public float chaseSpeed;
	public float maxAngle = 0.90f;
	private bool playerIsNear;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
		atEdge = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);



		EnemyProximity ();
		if (playerIsNear == true) {
			Vector3 dir = player.transform.position - enemy.transform.position;
			float distance = dir.sqrMagnitude;
			float dot = Vector3.Dot (transform.forward, dir.normalized);
			if (player.transform.position.x < enemy.transform.position.x) {
				moveRight = false;
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else {
				moveRight = true;
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}
			transform.position += dir.normalized * chaseSpeed * Time.deltaTime;
			if (anim.GetBool ("Attacking")) {
				anim.SetBool ("Attacking", false);
			}

			if (distance < attackRange && Mathf.Abs (1 - dot) < maxAngle) {
				anim.SetBool ("Attacking", true);
			}
		}
	}
	/*void BackToGuardPost () {
		Vector3 gDir = guardPost.transform.position - enemy.transform.position;
		float gDistance = gDir.sqrMagnitude;
		if (gDistance < guardRange) {
		if (guardPost.transform.position.x < enemy.transform.position.x) {
			moveRight = false;
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} else {
			moveRight = true;
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
		transform.position += gDir.normalized * chaseSpeed * Time.deltaTime;
		}
	}
*/
void Patrol () {
		Vector3 gDir = guardPost.transform.position - enemy.transform.position;
		float gDistance = gDir.sqrMagnitude;

		if (hittingWall || !atEdge)
			moveRight = !moveRight;
		if (moveRight) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		} else {
			transform.localScale = new Vector3 (1f, 1f, 1f);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		}
		if (gDistance > guardRange) {
			if (guardPost.transform.position.x < enemy.transform.position.x) {
				moveRight = false;
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else {
				moveRight = true;
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}
			if (gDistance > 0) {
				Debug.Log ("mita helevettia");
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, guardPost.transform.position, Time.deltaTime * 150);
			}

		}
	}
/*	void EAI () {
		Vector3 dir = player.transform.position - enemy.transform.position;

		Vector3 gDir = guardPost.transform.position - player.transform.position;
/	float gDistance = gDir.sqrMagnitude;

		float distance = dir.sqrMagnitude;
	float dot = Vector3.Dot (transform.forward, dir.normalized);


	if (anim.GetBool ("Attacking")) {
			anim.SetBool ("Attacking", false);
	}

		if (distance < attackRange && Mathf.Abs (1 - dot) < maxAngle) {
		anim.SetBool ("Attacking", true);
	}

		if (EnemyProximity = true) {
			Vector3 dir = player.transform.position - enemy.transform.position;
			float distance = dir.sqrMagnitude;
			float dot = Vector3.Dot (transform.forward, dir.normalized);
			if (player.transform.position.x < enemy.transform.position.x) {
				moveRight = false;
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else {
				moveRight = true;
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}
			transform.position += dir.normalized * chaseSpeed * Time.deltaTime;
		}
		if (gDistance > guardRange) {
			if (guardPost.transform.position.x < enemy.transform.position.x) {
				moveRight = false;
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else {
				moveRight = true;
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}
			if (gDistance > 0) {
				Debug.Log ("mita helevettia");
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, guardPost.transform.position, Time.deltaTime * 150);
			}
		} else {
			Patrol ();
		}
		*/


	void EnemyProximity () {
		Vector3 dir = player.transform.position - enemy.transform.position;
		float distance = dir.sqrMagnitude;
		float dot = Vector3.Dot (transform.forward, dir.normalized);
		if (distance < range && Mathf.Abs (1 - dot) < maxAngle)
			playerIsNear = true;
		else
			playerIsNear = false;
	}

	/*void Attacking () {
		Vector3 dir = player.transform.position - enemy.transform.position;
		float distance = dir.sqrMagnitude;
		float dot = Vector3.Dot (transform.forward, dir.normalized);
		if (anim.GetBool ("Attacking")) {
			anim.SetBool ("Attacking", false);
		}

		if (distance < attackRange && Mathf.Abs (1 - dot) < maxAngle) {
			anim.SetBool ("Attacking", true);
		}
	} */

}