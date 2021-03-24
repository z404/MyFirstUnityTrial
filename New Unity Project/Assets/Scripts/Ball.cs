using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Ball : MonoBehaviour {
 
    // Balls default movement speed
    public float speed = 30;
 
    // The balls Rigidbody component
    private Rigidbody2D rigidBody;
 
    // Used to play sound effects
    private AudioSource audioSource;
 
	// Use this for initialization
	void Start () {
 
        // Get reference to the ball Rigidbody
        rigidBody = GetComponent<Rigidbody2D>();
 
        // When the ball is created move it to
        // the right (1,0) at the desired speed
        rigidBody.velocity = Vector2.right * speed;
 
	}
 
    // Called every time a ball collides with something
    // the object it hit is passed as a parameter
    void OnCollisionEnter2D(Collision2D col)
    {
     
        // If the LeftPaddle or RightPaddle hit the
        // ball simulate the ricochet
        if ((col.gameObject.name == "LeftPaddle") || (col.gameObject.name == "RightPaddle"))
        {
 
            HandlePaddleHit(col);
 
        }
 
        // WallBottom or WallTop
        if ((col.gameObject.name == "WallBottom") || (col.gameObject.name == "WallTop"))
        {
            // Play the sound effect
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.wallBloop);
        }
 
        // LeftGoal or RightGoal
        if ((col.gameObject.name == "WallLeft") || (col.gameObject.name == "WallRight"))
        {
            // Play the sound effect
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.goalBloop);
 
            // TODO Update Score UI
 
            // Move the ball to the center of the screen
            transform.position = new Vector2(0, 0);
 
        }
 
    }
 
    void HandlePaddleHit(Collision2D col)
    {
 
        // Find y for the ball vector based
        // on where the ball hit the paddle
        // Above the center y angles up
        // Below the center y angles down
        float y = BallHitPaddleWhere(transform.position,
            col.transform.position,
            col.collider.bounds.size.y);
 
        // Vector ball moves to
        Vector2 dir = new Vector2();
 
        // Go left or right on the x axis
        // depending on which panel was hit
        if(col.gameObject.name == "LeftPaddle")
        {
            dir = new Vector2(1, y).normalized;
        }
 
        if (col.gameObject.name == "RightPaddle")
        {
            dir = new Vector2(-1, y).normalized;
        }
 
        // Change the velocity / direction of ball
        // You assign a vector to velocity
        rigidBody.velocity = dir * speed;
 
        // Play sound effect
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitPaddleBloop);
 
    }
 
    // Find y for the ball vector based
    // on where the ball hit the paddle
    float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
    {
        return (ball.y - paddle.y) / paddleHeight;
    }
 
}