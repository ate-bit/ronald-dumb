using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float maxSpeed = 5f;
	public float jumpForce = 10f;

	private bool facingRight = true;
	private bool grounded = true;

	private Rigidbody2D rb2D;

	void Awake()
	{
		rb2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		float move = Input.GetAxis ("Horizontal");
		rb2D.velocity = new Vector2 (move * maxSpeed, rb2D.velocity.y);

		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}

		if (grounded) {
			if (Input.GetKey (KeyCode.Space)) {
				grounded = false;
				rb2D.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground")) {
			grounded = true;
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}	
}
