using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Line : MonoBehaviour 
{
	private LayerMask layerMirror = (1 << 8);
	private LayerMask layerIgnore = ~((1 << 8) | (1 << 9));
	
	private Vector2 mouseDirection;
	private LineRenderer line;

	List<Vector3> linePosList = new List<Vector3>();

	// Use this for initialization
	void Start () 
	{
		line = this.GetComponent<LineRenderer> ();
		linePosList.Add(transform.position);
		linePosList.Add(transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Teleport ();
		CastLine ();
	}

	private void CastLine()
	{
		line.SetVertexCount(linePosList.Count);

		linePosList[0] = transform.position;

		for(int i = 0; i < linePosList.Count; ++i)
		{
			line.SetPosition(i, linePosList[i]);
		}

		if(Input.GetMouseButton(0))
		{
			mouseDirection = Camera.main.ScreenToWorldPoint( Input.mousePosition) - transform.position;
			mouseDirection.Normalize();

			RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseDirection, 100.0f, layerMirror);
			if(hit.collider)
			{
				linePosList[1] = hit.point;
				hit.transform.GetComponent<Mirror>().SendLine(hit.point, hit.normal, mouseDirection, 2);

				           /*
				(linePosList.Count == 2)
				{
					linePosList.Add(hit.transform.GetComponent<Mirror>().SendLine(hit.point, hit.normal, mouseDirection, 0));
				}
				else
				linePosList[2] = hit.transform.GetComponent<Mirror>().SendLine(hit.point, hit.normal, mouseDirection, 0);
*/
			//	Debug.Log ("hitMirror");
			}
			else
			{
				hit = Physics2D.Raycast(transform.position, mouseDirection, 100.0f, layerIgnore);
				if(hit.collider)
				{
					linePosList.Clear();
					linePosList.Add(transform.position);
					linePosList.Add(hit.point);
			//		Debug.Log ("hitElse");
				}	
			}		
			//Debug.Log (mouseDirection);
		}
		else
		{
			linePosList.Clear();
			linePosList.Add(transform.position);
			linePosList.Add(transform.position);
		}
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
				transform.parent.position = new Vector2( linePosList[linePosList.Count - 1].x, linePosList[linePosList.Count - 1].y );
			}
		}
	}
}
