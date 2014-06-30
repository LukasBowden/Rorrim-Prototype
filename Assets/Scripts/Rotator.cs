using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	float deltaRotation;
	float previousRotation;
	float currentRotation;

	bool clickable = false;

	// Use this for initialization
	void Start () 
	{
		//setup the size of the graphic and hitbox
		transform.localScale = new Vector3(transform.parent.transform.localScale.y/transform.parent.transform.localScale.x, 1, 1);//new Vector3(transform.parent.transform.localScale.y, transform.parent.transform.localScale.y, 0);
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0))
			clickable = true;
	}

	// Update is called once per frame
	void Update () 
	{
		if(clickable)
		{
			if (Input.GetMouseButtonDown (0) && Input.GetKey(KeyCode.LeftShift))
			{
				deltaRotation = 0f;
				previousRotation = angleBetweenPoints( transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
			}
			else if(Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
			{
				currentRotation = angleBetweenPoints( transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) );
				deltaRotation = Mathf.DeltaAngle( currentRotation, previousRotation );
				previousRotation = currentRotation;
				transform.parent.transform.Rotate( Vector3.back, deltaRotation );
			}
		}
		if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftShift))
		{
			clickable = false;
		}
	}

	float angleBetweenPoints( Vector2 position1, Vector2 position2 )
	{
		Vector2 fromLine = position2 - position1;
		Vector2 toLine = new Vector2( 1, 0 );
		
		float angle = Vector2.Angle( fromLine, toLine );
		Vector3 cross = Vector3.Cross( fromLine, toLine );

		if( cross.z > 0 )
			angle = 360f - angle;
		
		return angle;
	}
}
