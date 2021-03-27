using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AIPaddle : MonoBehaviour {
 
	// Reference to the ball
	public Ball theBall;
 
	// Default ball speed
	public float speed = 30;
 
	// lerp is used to smooth out movement over time
	// 
	public float lerpTweak = 2f;
 
	// Reference to the Rackets Rigidbody component
	private Rigidbody2D rigidBody;
 
	// Use this for initialization
	void Start () {
 
		// Get reference to the attached Rigidbody
		// component
		rigidBody = GetComponent<Rigidbody2D> ();
	}
 
	void FixedUpdate () {
 
		// Check if the ball y position is > racket y position
		if (theBall.transform.position.y > transform.position.y)
		{
			Vector2 dir = new Vector2(0, 1).normalized;
 
			// Lerp receives 2 vectors and smoothes the movement over time
			rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, dir * speed, lerpTweak * Time.deltaTime);
		}
		else if (theBall.transform.position.y < transform.position.y)
		{
			Vector2 dir = new Vector2(0, -1).normalized;
			rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, dir * speed, lerpTweak * Time.deltaTime);
		}
		else
		{
			Vector2 dir = new Vector2(0, 0).normalized;
			rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, dir * speed, lerpTweak * Time.deltaTime);
		}
	}
 
}