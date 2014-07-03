using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {

	public string axis;

	public float maxX = 0;
	public float minX = 0;
	public float maxY = 0;
	public float minY = 0;

	private Vector3 pos;

	bool clickable = false;

	private int lockAxis = 0;

	// Use this for initialization
	void Start ()
	{
		pos = transform.parent.position;
		maxX += pos.x;
		minX += pos.x;

		maxY += pos.y;
		minY += pos.y;
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
				//Play a sound?
			}
			else if(Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
			{
				if(axis == "X")
				{
					pos.x += Input.GetAxis("Mouse X") * 0.25f;
					if(pos.x > maxX)
					{
						pos.x = maxX;
					}
					if(pos.x < minX)
					{
						pos.x = minX;
					}
				}
				if(axis == "Y")
				{
					pos.y += Input.GetAxis("Mouse Y") * 0.25f;
					if(pos.y > maxY)
					{
						pos.y = maxY;
					}
					if(pos.y < minY)
					{
						pos.y = minY;
					}
				}
				if(axis == "XY")
				{
					if(Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Mouse Y")) && lockAxis == 0)
						lockAxis = 1;
					if(lockAxis == 1)
						pos.x += Input.GetAxis("Mouse X") * 0.25f;
					if(pos.x > maxX)
					{
						pos.x = maxX;
					}
					if(pos.x < minX)
					{
						pos.x = minX;
					}
					if(Mathf.Abs(Input.GetAxis("Mouse X")) < Mathf.Abs(Input.GetAxis("Mouse Y")) && lockAxis == 0)
						lockAxis = 2;
					if(lockAxis == 2)
						pos.y += Input.GetAxis("Mouse Y") * 0.25f;
					if(pos.y > maxY)
					{
						pos.y = maxY;
					}
					if(pos.y < minY)
					{
						pos.y = minY;
					}
				}
				transform.parent.position = pos;
			}
		}
		if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftShift))
		{
			lockAxis = 0;
			clickable = false;
		}
	}
}
