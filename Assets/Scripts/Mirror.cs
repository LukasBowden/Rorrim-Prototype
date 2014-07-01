using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour 
{
	private LayerMask layerMirror = (1 << 8);
	private LayerMask layerIgnore = ~((1 << 9) | (1 << 10) | (1 << 11));
	private Vector3 dir;
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
		if(hit.collider.tag == "Mirror")
		{
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().enableTeleport();
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().updateList(index, hit.point);			
			if(index < 4320)
			{
				hit.transform.GetComponent<Mirror>().SendLine(hit.point, hit.normal, dir, index + 1);
			}
		}
		else if(hit.collider.tag == "NoTeleport")
		{
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().blockTeleport();
		}
		else
		{
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().enableTeleport();
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().updateList(index, hit.point);
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().clearElse(index);
		}
	}
}
