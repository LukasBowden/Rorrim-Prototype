using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour 
{
	private LayerMask layerIgnore = ~((1 << 9) | (1 << 10) | (1 << 11) | (1 << 12));
	private Vector3 dir;
	private Vector3 hitPoint;	
	private RaycastHit2D hit;
//	private normRot;


	// Use this for initialization
	void Start () 
	{
//		normRot = 
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void SendLine(Vector3 hitPoint, Vector3 hitNormal, Vector3 incidentLine, int index)
	{
		/*
			R = reflected			(?dir?)
			N = normal of mirror	(hitNormal)
			I = incident ray		(incidentLine)
			V = 2 * N * (-N dot I)
				R = I + 2 * N * (-N dot I)
				  = I - 2 * N * (N dot I)			
		 */
		dir = incidentLine - ((2 * hitNormal) * (Vector3.Dot(hitNormal, incidentLine)));
		hit = Physics2D.Raycast(hitPoint + dir * 0.01f, dir, Mathf.Infinity, layerIgnore);

		hitPoint.x = hit.point.x;
		hitPoint.y = hit.point.y;
		hitPoint.z = 1;

		if(hit.collider.tag == "Mirror")
		{
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().enableTeleport();
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().updateList(index, hitPoint);			
			if(index < 500)
			{			
				hit.transform.GetComponent<Mirror>().SendLine((hitPoint), hit.normal, dir, index + 1);
				GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().setTeleportPos(hitPoint.x + hit.normal.x/6, hitPoint.y + hit.normal.y/3);				
			}
		}
		else if(hit.collider.tag == "NoTeleport")
		{
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().blockTeleport();
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().setTeleportPos(hitPoint.x + hit.normal.x/6, hitPoint.y + hit.normal.y/3);
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().updateList(index, hitPoint);
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().clearElse(index);
		}
		else if(hit.collider.tag == "Activator")
		{
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().blockTeleport();
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().setTeleportPos(hitPoint.x + hit.normal.x/6, hitPoint.y + hit.normal.y/3);
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().updateList(index, hitPoint);
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().clearElse(index);

			hit.collider.GetComponent<Activator>().AddColor(GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().curColor);
		}
		else
		{
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().enableTeleport();
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().setTeleportPos(hitPoint.x + hit.normal.x/6, hitPoint.y + hit.normal.y/3);
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().updateList(index, hitPoint);
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().clearElse(index);
		}
	}
}
