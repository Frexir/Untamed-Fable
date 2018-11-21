using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour {

	public float jumpTime;
	public float jumpMaxTime;
	public float jumpSpeed;
	public bool jump;
	public bool falling;

	public float moveSpeedMax;
	public float moveSpeed;
	public float moveSpeedStore;
	public bool run;
	//[HideInInspector]
	public float moveVelocity;

	public float relativeForce;
	private float facingRight;

	public Transform groundCheck2;
	public float groundCheckRadius2;
	public LayerMask whatIsGround;
	private bool grounded;
	//private bool doubleJumped;
	[HideInInspector]
	public Animator anim;

	public float bossknockback;
	public float bossknockbackCount;
	public float bossknockbackLength;
	public float knockback;
	public float knockbackCount;
	public float knockbackLenght;
	public bool knockFromRight;
	
	public float blockTime;
	public float blockTimeS;

	public GameObject pSprint;
	public GameObject jumpSound;
	public GameObject PSlash1;
	public GameObject PSlash2;
	public GameObject PSlash3;
	//public float audioVolume;
	[HideInInspector]
	public bool invincible = false;
	public float timeSpentInvincible;

	public bool isComboPlaying;
	private int tap;
	private float timer;
	private bool attack;
	private float lastPress;
	public float aTime;

	public Material hurtMat;
	public Material mainMat; 
	private Rigidbody2D rgb;

		// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rgb = GetComponent<Rigidbody2D> ();
		jumpSound = GameObject.Find ("PJump");
		PSlash1 = GameObject.Find ("PSlash1");
		PSlash2 = GameObject.Find ("PSlash2");
		PSlash3 = GameObject.Find ("PSlash3");
		pSprint = GameObject.Find ("PSprint");

	}
	void FixedUpdate () {
		if ((jump ==true) && (jumpTime<jumpMaxTime) && (falling == false)){
			jumpTime = jumpTime+0.2f;
			rgb.velocity = new Vector2 (rgb.velocity.x,(jumpTime+jumpSpeed));
		} else {
			jumpTime = 0;
			jump = false;
		}
		grounded = Physics2D.OverlapCircle (groundCheck2.position, groundCheckRadius2, whatIsGround);

	}


	// Update is called once per frame
	void Update () {
	
		if (run) {
			moveSpeed = moveSpeedMax;
		} else {
			moveSpeed = moveSpeedStore;
		}
		moveVelocity = moveSpeed * Input.GetAxisRaw ("Horizontal");
	//	if(Input.GetKey("escape")) {
	//		Quit();
	//	}
		//if (grounded)
		//	doubleJumped = false;
		rgb.AddRelativeForce (new Vector2(relativeForce*facingRight, 0), ForceMode2D.Force);
		if (isComboPlaying)
			moveVelocity = 0;

		if (Input.GetButtonDown ("Jump") && grounded && !anim.GetBool ("Block") && !Input.GetButton ("Vertical")) {
			jump = true;
			jumpSound.GetComponent<AudioSource> ().Play();

            //Jump ();
        } 

		if (Input.GetButtonUp("Jump")) {
			jump = false;
			//Jump ();
		}
	/*	if (Input.GetButtonDown("Jump") && !doubleJumped && !grounded && !Input.GetButton ("Vertical")) {
			Jump ();
			doubleJumped = true;
		}*/


		if (Input.GetButtonDown ("Run")) {
			run = true;
			pSprint.GetComponent<AudioSource> ().Play ();
			moveSpeed = moveSpeedMax;
		}
		if (Input.GetButtonUp ("Run")) {
			pSprint.GetComponent<AudioSource> ().Stop ();
			moveSpeed = moveSpeedStore;
			run = false;
		}


	/* 	if (moveSpeed < moveSpeedMax && Input.GetButton("Horizontal") && !anim.GetBool("Block")) {

				moveSpeed += Time.deltaTime*3;

		}
		if (moveSpeed >= moveSpeedMax)
			moveSpeed = moveSpeedMax; 
		if (Input.GetButtonUp ("Horizontal"))
			moveSpeed = moveSpeedStore;*/

		if (knockbackCount <= 0.2f && bossknockbackCount <= 0.2f) {
			rgb.velocity = new Vector2 (moveVelocity, rgb.velocity.y);
			anim.SetBool ("hurt", false);
		}
		
			else if (bossknockbackCount > 0.2f) {
				moveSpeed = moveSpeedStore;
				anim.SetBool ("hurt", true);
				rgb.velocity = new Vector2 (0, 0);
				if (knockFromRight) 
					rgb.velocity = new Vector2 (bossknockback, bossknockback * 2);
				if (!knockFromRight)
					rgb.velocity = new Vector2 (-bossknockback, bossknockback * 2);
			
				bossknockbackCount -= Time.deltaTime;
		
		} else if (knockbackCount > 0.2f) {
				moveSpeed = moveSpeedStore;
				anim.SetBool ("hurt", true);
				rgb.velocity = new Vector2 (0, 0);
				if (knockFromRight) 
					rgb.velocity = new Vector2 (-knockback, knockback * 2);
				if (!knockFromRight)
					rgb.velocity = new Vector2 (knockback, knockback * 2);
			knockbackCount -= Time.deltaTime;
			} 

			if (rgb.velocity.y < 0 && !grounded) {
				falling = true;
				anim.SetBool ("falling", true);
		
			} else {
				anim.SetBool ("falling", false);
				falling = false;
			}
		


		//if (anim.GetBool ("AttackLL"))
		//	anim.SetBool ("AttackLL", false);
			timer = Time.timeSinceLevelLoad;
	
			if (!isComboPlaying) {
			if (Input.GetButtonDown ("Fire1") && grounded) {
				lastPress = Time.timeSinceLevelLoad;
				if (tap > 2) {
					tap = 1;
				}
				else {
					tap++;
				}
				Combo ();
			}
			//	anim.SetBool ("AttackLL", true);
		
			if (timer - lastPress > 1) {
				tap = 0;
				if (timer - lastPress < aTime) {
					attack = false;
				}  else {
					attack = true;

				}
			}
		}








		if (anim.GetBool ("Block")) 
			blockTime = blockTime -= Time.deltaTime;
			
		if (Input.GetButtonDown ("Block") && grounded && !Input.GetButton("Jump") && blockTime >= 0) {
				anim.SetBool ("Block", true);

			moveSpeed = 0;
	//		moveVelocity = moveSpeed * Input.GetAxisRaw ("Horizontal");
			}
		if (Input.GetButtonUp ("Block") || blockTime < 0&& !run) {
				anim.SetBool ("Block", false);
			blockTime = blockTimeS;
			moveSpeed = moveSpeedStore;
	//		moveVelocity = moveSpeed * Input.GetAxisRaw ("Horizontal");
			}


		anim.SetFloat ("moveSpeed", Mathf.Abs (rgb.velocity.x));
			anim.SetBool ("grounded", grounded);
			
			if (GetComponent<Rigidbody2D> ().velocity.x > 0) {
			transform.localScale = new Vector3 (1f, 1f, 1f);
			facingRight = 1f;
		} else if (GetComponent<Rigidbody2D> ().velocity.x < 0) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);
			facingRight = -1f;
		}

		if (invincible) {
			timeSpentInvincible += Time.deltaTime;
			StartCoroutine ("InvincibilityBlink");
			if (timeSpentInvincible > 1.55f) {
				invincible = false;
			}
		}
		}
	//	if (Input.GetButton ("Vertical") && Input.GetButton("Jump") && grounded) {
	//		GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x,1);
		//	Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Ground"), LayerMask.NameToLayer ("Player"), true);
			//GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -10);
		//	GetComponent<Rigidbody2D>().AddRelativeForce (new Vector2(0, -10), ForceMode2D.Force);
			//GetComponent<PolygonCollider2D> ().enabled = false;
			//GetComponent<PolygonCollider2D> ().enabled = true;
		//} else {
		//	Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Ground"), LayerMask.NameToLayer ("Player"), false);
		//}
	
/*		public void Jump(){
			GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x,JumpHeight);
		jumpSound.Play();
}*/
	IEnumerator InvincibilityBlink() {
	//	Material mainMat = GetComponent<SpriteRenderer> ().material;
	//	Material hurtMat = Material.GetTexture("HurtFlash");
		//Color mainCol = GetComponent<SpriteRenderer> ().material.color;

		GetComponent<SpriteRenderer> ().material = hurtMat;
		//GetComponent<SpriteRenderer> ().material.color = Color.white;
			yield return new WaitForSeconds(0.2f);
		GetComponent<SpriteRenderer> ().material = mainMat;
	//	GetComponent<SpriteRenderer> ().material.color = mainCol;
	}

public void Combo(){
	if (attack) {
		switch(tap) {
		case 0:
			//Reset to idle
			anim.Play("NewIdle");
				break;
			case 1:
			//combo start
		//		isComboPlaying = true;
				anim.Play ("NewAttack1");
				PSlash1.GetComponent<AudioSource> ().Play();
				break;

		case 2:
			//combo 2
			anim.Play("NewAttack2");
				PSlash2.GetComponent<AudioSource> ().Play();
			//	isComboPlaying = true;
			break;

		case 3:
			//combo 3
			anim.Play("NewAttack3");
				PSlash3.GetComponent<AudioSource> ().Play();
			//	isComboPlaying = true;
			break;
		}
	}
}

public void Quit(){
	Application.Quit();
}
}

/*		if(knockFromRight)
 * 				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (knockback, knockback * 6f));
				GetComponent<Rigidbody2D>().velocity = new Vector2 (-knockback, knockback);
		if(!knockFromRight)
				GetComponent<Rigidbody2D>().velocity = new Vector2 (knockback, knockback);
								GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-knockback, knockback * 6f));
		knockbackCount -= Time.deltaTime;

 */ 