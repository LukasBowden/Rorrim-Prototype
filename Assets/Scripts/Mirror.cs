using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour 
{
	private LayerMask layerMirror = (1 << 8);
	private LayerMask layerMirrorIgnore = ~(1 << 8);
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
		hit = Physics2D.Raycast(hitPoint + dir * 0.01f, dir, Mathf.Infinity, layerMirror);
		if(hit.collider)
		{
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().updateList(index, hit.point);
			
			if(index < 4320)
			{
				hit.transform.GetComponent<Mirror>().SendLine(hit.point, hit.normal, dir, index + 1);
			}
		}
		else
		{
			hit = Physics2D.Raycast(hitPoint + dir * 0.01f, dir, Mathf.Infinity, layerMirrorIgnore);
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().updateList(index, hit.point);
			GameObject.FindGameObjectWithTag("Line").GetComponent<Line>().clearElse(index);
		}
	}
}
