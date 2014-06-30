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
		transform.localScale = new Vector3(transform.parent.transform.localScale.y/transform.parent.transform.localScale.x, 1, 1);//new Vector3(transform.parent.transform.localScale.y, transform.parent.transform.localScale.y, 0);
	}

	void OnMouseOver()
	{
		clickable = true;
		Debug.Log("Rotator Mouse Over");
	}

	void OnMouseDrag()
	{
		if(!clickable) return;
		if (Input.GetMouseButtonDown (0))
		{
			deltaRotation = 0f;
			previousRotation = angleBetweenPoints( transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
		else if(Input.GetMouseButton(0))
		{
			currentRotation = angleBetweenPoints( transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) );
			deltaRotation = Mathf.DeltaAngle( currentRotation, previousRotation );
			previousRotation = currentRotation;
			transform.parent.transform.Rotate( Vector3.back, deltaRotation );
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}

	float angleBetweenPoints( Vector2 position1, Vector2 position2 )
	{
		var fromLine = position2 - position1;
		var toLine = new Vector2( 1, 0 );
		
		var angle = Vector2.Angle( fromLine, toLine );
		var cross = Vector3.Cross( fromLine, toLine );
		
		// did we wrap around?
		if( cross.z > 0 )
			angle = 360f - angle;
		
		return angle;
	}
}
