using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

	public float speed;
	public float jumpForce;


	private float move;
	private Vector2 playerVelocity;

	private bool grounded = false;
	private bool leftWall = false;
	private bool rightWall = false;
	private float boundingWidth = 0.34f;
	private float boundingHeight = 0.64f;
	private LayerMask layerGround = ~((1 << 9) | (1 << 10) | (1 << 11) | (1 << 12));
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
		GroundCheck();
		WallCheck();
		PlayerMovement ();
	}

	private void PlayerMovement()
	{
		playerVelocity.x = move * speed;
		if(grounded)
			playerVelocity.y = 0;
		else
			playerVelocity.y = rigidbody2D.velocity.y;

		if(grounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)))
			playerVelocity.y = jumpForce;

		rigidbody2D.velocity = playerVelocity;
	}

	private void GroundCheck()
	{
		if(Physics2D.Raycast(transform.position, -Vector2.up, boundingHeight/2 + 0.02f, layerGround) 
		   || (Physics2D.Raycast(new Vector2(transform.position.x-boundingWidth/2, transform.position.y), -Vector2.up, boundingHeight/2 + 0.02f, layerGround))
		   || (Physics2D.Raycast(new Vector2(transform.position.x+boundingWidth/2, transform.position.y), -Vector2.up, boundingHeight/2 + 0.02f, layerGround)))
			grounded = true;
		else
			grounded = false;
	}

	private void WallCheck()
	{
		if(Physics2D.Raycast(transform.position, -Vector2.right, boundingWidth/2 + 0.02f, layerGround)
		   || (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-0.31f), -Vector2.right, boundingWidth/2 + 0.02f, layerGround))
		   || (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y+0.31f), -Vector2.right, boundingWidth/2 + 0.02f, layerGround)))
		{
			leftWall = true;
			rightWall = false;
		}
		else if(Physics2D.Raycast(transform.position, Vector2.right, boundingWidth/2 + 0.02f, layerGround)
		        || (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-0.31f), Vector2.right, boundingWidth/2 + 0.02f, layerGround))
		        || (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y+0.31f), Vector2.right, boundingWidth/2 + 0.02f, layerGround)))
		{
			leftWall = false;
			rightWall = true;
		}
		else
		{
			leftWall = false;
			rightWall = false;
		}
		
		if(leftWall && move == -1)
		{
			move = 0;
		}
		if(rightWall && move == 1)
		{
			move = 0;
		}
	}

}
