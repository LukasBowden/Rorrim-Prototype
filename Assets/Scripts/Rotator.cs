using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	public float upperAngleLimit = 0;
	public float lowerAngleLimit = 0;

	float currentRotation;

	bool clickable = false;

	void Start () 
	{
	//	transform.position = new Vector3 (transform.parent.transform.position.x, transform.parent.transform.position.y, 2);
		transform.localScale = new Vector3(transform.parent.transform.localScale.y/transform.parent.transform.localScale.x, 1, 1);
		currentRotation = transform.parent.transform.eulerAngles.z;		
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0))
			clickable = true;
	}

	void Update () 
	{
		if(clickable)		
		{
			if (Input.GetMouseButtonDown (0) && Input.GetKey(KeyCode.LeftShift))
			{
				//Play a sound?
			}
			else if(Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
			{
				if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x > 0)
				{
					currentRotation += Input.GetAxis("Mouse Y") * 15;
				}
				if(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y > 0)
				{
					currentRotation -= Input.GetAxis("Mouse X") * 15;
				}
				if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x < 0)
				{
					currentRotation -= Input.GetAxis("Mouse Y") * 15;
				}
				if(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y < 0)
				{
					currentRotation += Input.GetAxis("Mouse X") * 15;
				}
				if(lowerAngleLimit != upperAngleLimit)
					currentRotation = Mathf.Clamp (currentRotation, lowerAngleLimit, upperAngleLimit);
			
				transform.parent.transform.eulerAngles = new Vector3(0, 0, currentRotation);
			}
			if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftShift))
			{
				clickable = false;
			}
		}
	}
}
