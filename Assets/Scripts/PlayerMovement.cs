﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector]
	public static bool grounded;

	public float speed;
	public float jumpForce;

	/*private KeyCode left;
	private KeyCode right;
	private KeyCode up;*/

	private bool facingRight;
	private SpriteRenderer playerSpriteRenderer;
	private Rigidbody2D playerRigidbody;
	private Transform groundCheck;

	void Start () {
		/*Temporary
		left = KeyCode.A;
		right = KeyCode.D;
		up = KeyCode.W;*/

		facingRight = true;
		playerSpriteRenderer = GetComponent<SpriteRenderer> ();
		playerRigidbody = GetComponent<Rigidbody2D> ();
		groundCheck = transform.FindChild ("GroundCheck");
	}
	
	void Update () {
		//Needs optimization?
		if (facingRight) {
			playerSpriteRenderer.flipX = false;
			groundCheck.transform.position = new Vector2 (transform.position.x - 0.5f, groundCheck.position.y);
		} else {
			playerSpriteRenderer.flipX = true;
			groundCheck.transform.position = new Vector2 (transform.position.x + 0.5f, groundCheck.position.y);

		}
			
		/*if (Input.GetKey (right)) {
			facingRight = true;
			transform.Translate(new Vector2(0.1f * speed, 0));
		}

		if (Input.GetKey(left)){
			facingRight = false;
			transform.Translate(new Vector2(-0.1f * speed, 0));
		}*/

		if (SceneScript.instance.playingStarted) {
			transform.Translate (new Vector2 (0.1f * speed, 0));
		}

		CheckIfGrounded ();
		if (grounded && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			SceneScript.instance.playingStarted = true;
			Vector2 jumpVector = (Vector2.up * jumpForce);
			playerRigidbody.velocity = Vector2.zero;
			playerRigidbody.AddForce (jumpVector);
		}
	}

	void CheckIfGrounded(){
		grounded = Physics2D.Linecast (
			transform.position, 
			groundCheck.transform.position, 
			1 << LayerMask.NameToLayer("Ground")
		);
	}
}