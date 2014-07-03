using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Line : MonoBehaviour 
{
	private LayerMask layerIgnore = ~((1 << 9) | (1 << 10) | (1 << 11) | (1 << 12));
	
	private Vector2 mouseDirection;
	private LineRenderer line;

	private bool canTeleport = true;
	private Vector3 teleportPos;
	private Vector3 pos;

	private List<Vector3> linePosList = new List<Vector3>();

	private int curLens = 1;

	public Color curColor;

	// Use this for initialization
	void Start () 
	{
		pos = transform.parent.transform.position;
		pos.z = 0;
		line = GetComponent<LineRenderer> ();
		linePosList.Add(transform.position);
		linePosList.Add(transform.position);
		linePosList.Add(transform.position);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(canTeleport)
		{
			Teleport ();
		}
		CastLine ();
		curLens += (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);
		setLens(curLens);
	}

	public void setLens(int lens)
	{	
		switch(lens)
		{
		case 1:
			curLens = 4;
			break;
		case 2:
			line.SetColors (Color.red, Color.red);
			curColor = Color.red;
			break;
		case 3:
			line.SetColors (Color.yellow, Color.yellow);
			curColor = Color.yellow;
			break;
		case 4:
			line.SetColors (Color.blue, Color.blue);
			curColor = Color.blue;
			break;
		case 5:
			curLens = 2;
			break;

		default :
			curLens = 2;
			break;
		}
	}

	private void CastLine()
	{
		line.SetVertexCount(linePosList.Count);

		linePosList[0] = transform.position;

		for(int i = 0; i < linePosList.Count; ++i)
		{
			line.SetPosition(i, linePosList[i]);
		}

		if(Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift))
		{
			mouseDirection = Camera.main.ScreenToWorldPoint( Input.mousePosition) - transform.position;
			mouseDirection.Normalize();

			RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseDirection, Mathf.Infinity, layerIgnore);
			
			if(hit.collider.tag != "Mirror")
			{
				enableTeleport();
				linePosList.Clear();
				linePosList.Add(transform.position);
				linePosList.Add(hit.point);
				setTeleportPos(hit.point.x + hit.normal.x/5.8f, hit.point.y + hit.normal.y/3);
				if(hit.collider.tag == "NoTeleport")
				{
					blockTeleport();
				}
				else if(hit.collider.tag == "Activator")
				{
					hit.collider.GetComponent<Activator>().AddColor(curColor);					
				}
			}
			else
			{		
				enableTeleport();
				linePosList[1] = new Vector3(hit.point.x, hit.point.y, 1);
				updateList(2, new Vector3 (hit.point.x + mouseDirection.x * 0.01f, hit.point.y + mouseDirection.y * 0.01f, 1));
				hit.transform.GetComponent<Mirror>().SendLine(hit.point, hit.normal, mouseDirection, 3);
			}
			pos.x = transform.parent.transform.position.x;
			pos.y = transform.parent.transform.position.y;
			pos.z = 1;
		}
		else
		{
			linePosList.Clear();
			linePosList.Add(pos);
			linePosList.Add(pos);
			linePosList.Add(pos);
		}
		pos = transform.parent.transform.position;
		pos.z = 0;
	}

	public void updateList(int index, Vector3 point)
	{
		if(linePosList.Count - 1 < index)
			linePosList.Add(point);
		else
			linePosList[index] = point;
	}

	public void clearElse(int index)
	{
		if(linePosList[index + 1] != null)
		{
			linePosList.RemoveRange(index + 1, linePosList.Count - (index + 1));
		}
	}

	private void Teleport ()
	{
		if(Input.GetMouseButton(0))
		{
			if(Input.GetMouseButtonDown(1))
			{
				transform.parent.position = teleportPos;
			}
		}
	}

	public void blockTeleport()
	{
		canTeleport = false;
	}
	public void enableTeleport()
	{
		canTeleport = true;
	}
	public void setTeleportPos(float TeleportPosX, float TeleportPosY)
	{
		teleportPos.x = TeleportPosX;
		teleportPos.y = TeleportPosY;
		teleportPos.z = 1;
	}
}
