using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;


	public PlayerController2 player;
	public bool isFollowing;
	public float xOffSet;
	public float yOffSet;

	void Start () {

		player = FindObjectOfType<PlayerController2> ();
		target = GameObject.Find ("Protagonist").transform;
		isFollowing = true;
	}

		// Update is called once per frame
		void FixedUpdate () 
		{
		if (isFollowing) {
			if (target) {
				Vector3 point = GetComponent<Camera> ().WorldToViewportPoint (target.position);
				Vector3 delta = target.position - GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
				Vector3 destination = transform.position + delta;

				//Y-axis Lock
				//destination.y = 0;

				transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);
			}
		} else
			return;
			//transform.position = new Vector4 (player.transform.position.x + xOffSet, player.transform.position.y + yOffSet, player.transform.position.z);

	}
}
