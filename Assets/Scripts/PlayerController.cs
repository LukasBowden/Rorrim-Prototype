using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

	public float speed;
	public float jumpForce;


	private float move;
	private Vector2 playerVelocity;

	// Use this for initialization
	void Start () 
	{

	}

	void FixedUpdate()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		move = Input.GetAxisRaw("Horizontal");
		PlayerMovement ();
	}

	private void PlayerMovement()
	{
		playerVelocity.x = move * speed;
		playerVelocity.y = rigidbody2D.velocity.y;

		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
			playerVelocity.y = jumpForce;

		rigidbody2D.velocity = playerVelocity;
	}


}
