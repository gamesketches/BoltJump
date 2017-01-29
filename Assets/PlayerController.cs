﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;
	bool grounded;
	bool started = false;
	bool jumping;
	float curGravity = 1;

	// Use this for initialization
	void Start () {
		jumping = false;
		grounded = false;
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		StartCoroutine(Introduction());
	}
	
	// Update is called once per frame
	void Update () {
		if(started) {
			Vector2 AccelerationVector = Vector2.zero;
			//Debug.Log(rb.velocity);
			if(Input.GetKeyDown(KeyCode.Space) && grounded) {
				rb.gravityScale = 1;
				rb.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
				jumping = true;
			}
				Vector2 accelerationVector;
				if(transform.rotation.z < 0 || transform.rotation.z > 270) {
					accelerationVector = new Vector2(transform.right.x, transform.right.y) * Input.GetAxis("Horizontal") * Time.deltaTime * 10;
					curGravity = grounded ? 10 : 1;
				}
				else {
					accelerationVector = new Vector2(transform.right.x * - 1, transform.right.y * -1) * Input.GetAxis("Horizontal") * Time.deltaTime * 10;
					curGravity = 1;
				}
				rb.gravityScale = curGravity;
				rb.AddForce(accelerationVector, ForceMode2D.Impulse);
			if(jumping) {
				transform.GetChild(1).RotateAround(Vector3.forward, 1 * Time.deltaTime);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.collider.tag == "Slope") {
			grounded = true;
			jumping = false;
			transform.GetChild(1).up = other.contacts[0].normal;
		}
	}
	void OnCollisionExit2D(Collision2D other) {
		if(other.collider.tag == "Slope") {
			grounded = false;
			rb.gravityScale = 1;
		}
	}

	IEnumerator Introduction() {
		float t = 0;
		Vector3 targetPosition = new Vector3(1, 0, -10);
		Vector3 startPosition = Camera.main.transform.localPosition;
		float startSize = Camera.main.orthographicSize;
		float targetSize = 20;
		while(t < 1) {
			Camera.main.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
			Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
			t += Time.deltaTime / 3;
			yield return null;
		}

		started = true;
		rb.gravityScale = 10;
	}
}