using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// Needed to manipulate the UI
using UnityEngine.UI;
 
public class Ball : MonoBehaviour {
 
	// Ball default speed
	public float speed = 30;
 
	// Reference to the balls Rigidbody component
	private Rigidbody2D rigidBody;
 
	// Reference the AudioSource for the Ball
	private AudioSource audioSource;
 
	// Use this for initialization
	void Start () {
 
		// Get reference to the attached Rigidbody
		// component
		rigidBody = GetComponent<Rigidbody2D> ();
 
		// Calculate velocity for the ball
		rigidBody.velocity = Vector2.right * speed;
	}
 
	// Called when the ball collides with anything
	void OnCollisionEnter2D(Collision2D col){
 
		// col provides info on the object the ball hit
		// If it is the left paddle do this
		if ((col.gameObject.name == "LeftPaddle") || (col.gameObject.name == "RightPaddle")) {
			handlePaddleHit (col);
		}
 
		if ((col.gameObject.name == "WallBottom") || (col.gameObject.name == "WallTop")) {
 
			// Call for SoundManager to play the wall sound
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.wallBloop);
		}
 
		if ((col.gameObject.name == "WallLeft") || (col.gameObject.name == "WallRight")) {
 
			// Call for SoundManager to play the goal sound
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.goalBloop);
 
			if (col.gameObject.name == "WallLeft") {
 
				increaseTextUIScore ("RightScoreUI");
					
			} else if (col.gameObject.name == "WallRight") {
 
				increaseTextUIScore ("LeftScoreUI");
 
			}
 
			// Change the balls position on the game board to its starting
			// position
			transform.position = new Vector2(-1.133788f, 0.1743597f);
		}
	}
 
	// Calculate where the ball hits the paddle by dividing
	// the ball's y coordinate by the paddles height
	// If the ball hits above the midpoint ricochet up
	// and vice versa
	float ballHitPaddleWhere(Vector2 ball, Vector2 paddle,
		float paddleHeight){
		return (ball.y - paddle.y) / paddleHeight;
	}
 
	void handlePaddleHit(Collision2D col){
 
		// Pass the balls position, the paddles position,
		// the height of the paddle
		float y = ballHitPaddleWhere (transform.position,
			col.transform.position,
			col.collider.bounds.size.y);
 
		// Calculate direction of ball
		// A vector is a line pointing from an origin
		// to a point x, y
		// Magnitude is the length of the line
		Vector2 dir = new Vector2();
 
		// If (0,1) is straight up and down is (0, -1), 
		// normalized would change our vector into a value 
		// between 0 and 1
		if (col.gameObject.name == "LeftPaddle") {
			dir = new Vector2 (1, y).normalized;
			Vector2 dir2 = dir = new Vector2 (1, y);
			Debug.Log ("Dir : " + dir + "Dir2 : " + dir2);
		}
 
		if (col.gameObject.name == "RightPaddle") {
			dir = new Vector2 (-1, y).normalized;
		}
 
		// Change the velocity / direction of the ball
		// You assign a vector to velocity here
		rigidBody.velocity = dir * speed;
 
		// Call for SoundManager to play paddle sound
		SoundManager.Instance.PlayOneShot (SoundManager.Instance.hitPaddleBloop);
	}
 
	// Increases the score the the text UI name passed
	void increaseTextUIScore(string textUIName){
 
		// Find the matching text UI component
		var textUIComp = GameObject.Find(textUIName)
			.GetComponent<Text>();
 
		// Get the string stored in it and convert to an int
		int score = int.Parse(textUIComp.text);
 
		// Increment the score
		score++;
 
		// Convert the score to a string and update the UI
		textUIComp.text = score.ToString();
	}
 
 
}